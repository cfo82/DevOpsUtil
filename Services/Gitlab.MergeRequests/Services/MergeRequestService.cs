namespace DevOpsUtil.Gitlab.MergeRequests.Services;

using System.Collections.Immutable;
using System.Net.Http.Headers;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Gitlab.Core.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.GraphQL;
using Microsoft.Extensions.DependencyInjection;

public class MergeRequestService : IMergeRequestService, IRefreshable
{
    private readonly List<IMergeRequest> _mergeRequests;
    private readonly ServiceProvider _services;
    private readonly string _groupPath;

    public MergeRequestService(
        IRefreshService refreshService,
        IGitlabSettingsService gitlabSettingsService,
        IMergeRequestServiceSettings settings)
    {
        var gitlabSettings = gitlabSettingsService.Get(settings.GitlabSettingsKey);
        _mergeRequests = new List<IMergeRequest>();
        _groupPath = gitlabSettings.GroupPath;

        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddGitlabClient()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(gitlabSettings.BaseAddress + "/api/graphql");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", gitlabSettings.AccessToken);
            });

        _services = serviceCollection.BuildServiceProvider();

        refreshService.RegisterRefreshable(this);
    }

    public event EventHandler<EventArgs>? OnMergeRequestsChanged;

    public ImmutableArray<IMergeRequest> MergeRequests => _mergeRequests.ToImmutableArray();

    public async Task RefreshAsync(bool forceRefresh)
    {
        var client = _services.GetRequiredService<IGitlabClient>();

        var mergeRequests = new List<MergeRequest>();
        var gitlabMergeRequests = await client.QueryMergeRequests.ExecuteAsync(_groupPath);
        foreach (var gitlabProject in gitlabMergeRequests.Data?.Group?.Projects.Nodes ??
                                      new List<IQueryMergeRequests_Group_Projects_Nodes>())
        {
            if (gitlabProject == null)
            {
                continue;
            }

            var project = new Project(gitlabProject);

            foreach (var mergeRequest in gitlabProject.MergeRequests?.Nodes ??
                                         new List<IQueryMergeRequests_Group_Projects_Nodes_MergeRequests_Nodes>())
            {
                if (mergeRequest == null)
                {
                    continue;
                }

                mergeRequests.Add(new MergeRequest(project, mergeRequest));
            }
        }

        mergeRequests = mergeRequests
            .OrderBy(mr => mr.Id)
            .ToList();

        _mergeRequests.Clear();
        _mergeRequests.AddRange(mergeRequests);

        OnMergeRequestsChanged?.Invoke(this, EventArgs.Empty);
    }
}