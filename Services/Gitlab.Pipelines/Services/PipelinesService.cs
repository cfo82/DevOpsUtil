namespace DevOpsUtil.Gitlab.Pipelines.Services;

using System.Collections.Immutable;
using System.Net.Http.Headers;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Gitlab.Core.Contracts;
using DevOpsUtil.Gitlab.Pipelines.Contracts;
using DevOpsUtil.Gitlab.Pipelines.GraphQL;
using Microsoft.Extensions.DependencyInjection;

public class PipelinesService : IPipelinesService, IRefreshable, IErrorStateProvider
{
    private readonly IPipelinesServiceSettings _settings;
    private readonly GitlabSettings _gitlabSettings;
    private readonly List<IProject> _projects;
    private readonly IServiceProvider _services;
    private readonly string _groupPath;
    private bool _isRefreshing;

    public PipelinesService(
        IRefreshService refreshService,
        IErrorStateService errorStateService,
        IGitlabSettingsService gitlabSettingsService,
        IPipelinesServiceSettings settings)
    {
        _gitlabSettings = gitlabSettingsService.Get(settings.GitlabSettingsKey);
        _settings = settings;
        _projects = new List<IProject>();
        _groupPath = _gitlabSettings.GroupPath;
        _isRefreshing = false;

        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddGitlabClient()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(_gitlabSettings.BaseAddress + "/api/graphql");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _gitlabSettings.AccessToken);
            });

        _services = serviceCollection.BuildServiceProvider();

        refreshService.RegisterRefreshable(this);
        errorStateService.SubscribeErrorProvider(this);
    }

    public event EventHandler? ProjectsChanged;

    public event EventHandler? ErrorStateChanged;

    public ImmutableArray<IProject> Projects => _projects.ToImmutableArray();

    public bool HasError => _projects.Count > 0 && _projects.Any(project => project.HasFailed);

    public async Task RefreshAsync(bool forceRefresh)
    {
        try
        {
            if (_isRefreshing)
            {
                return;
            }

            _isRefreshing = true;

            var client = _services.GetRequiredService<IGitlabClient>();

            var results = await Task.WhenAll(
                client.QueryPipelines.ExecuteAsync(_groupPath, "master"),
                client.QueryPipelines.ExecuteAsync(_groupPath, "develop"));

            _projects.Clear();

            foreach (var masterProject in results[0].Data?.Group?.Projects.Nodes ??
                                    new List<IQueryPipelines_Group_Projects_Nodes>())
            {
                if (masterProject == null)
                {
                    continue;
                }

                if (_gitlabSettings.IgnoreArchived && (masterProject.Archived ?? false))
                {
                    continue;
                }

                var developProject = results[1].Data?.Group?.Projects.Nodes
                    ?.FirstOrDefault(p => p != null && p.Id == masterProject.Id);
                if (developProject == null)
                {
                    continue;
                }

                _projects.Add(new Project(_settings, masterProject, developProject));
            }

            ErrorStateChanged?.Invoke(this, EventArgs.Empty);
            ProjectsChanged?.Invoke(this, EventArgs.Empty);
        }
        finally
        {
            _isRefreshing = false;
        }
    }
}