namespace DevOpsUtil.Github.PullRequests.Services;

using System.Collections.Immutable;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Github.Core.Contracts;
using DevOpsUtil.Github.PullRequests.Contracts;
using Octokit;
using Octokit.Internal;

public class PullRequestService : IPullRequestService, IRefreshable
{
    private readonly List<IPullRequest> _pullRequests;
    private readonly GitHubClient _client;

    public PullRequestService(
        IRefreshService refreshService,
        IGithubSettingsService githubSettingsService,
        IPullRequestServiceSettings settings)
    {
        var githubSettings = githubSettingsService.Get(settings.GithubSettingsKey);
        _pullRequests = new List<IPullRequest>();

        InMemoryCredentialStore credentials = new InMemoryCredentialStore(new Credentials(githubSettings.AccessToken));
        _client = new GitHubClient(new ProductHeaderValue(githubSettings.UserName), credentials);

        refreshService.RegisterRefreshable(this);
    }

    public event EventHandler<EventArgs>? OnPullRequestsChanged;

    public ImmutableArray<IPullRequest> PullRequests => _pullRequests.ToImmutableArray();

    public async Task RefreshAsync(bool forceRefresh)
    {
        var request = new PullRequestRequest();

        var pullRequests = new List<PullRequest>();
        var githubRepositories = await _client.Repository.GetAllForCurrent();
        foreach (var githubRepository in githubRepositories)
        {
            var repository = new Repository(githubRepository);

            var githubPullRequests = await _client.PullRequest.GetAllForRepository(githubRepository.Id);
            foreach (var githubPullRequest in githubPullRequests)
            {
                var githubReviews = await _client.PullRequest.Review.GetAll(repository.Id, githubPullRequest.Number);
                pullRequests.Add(new PullRequest(repository, githubPullRequest, githubReviews));
            }
        }

        pullRequests = pullRequests
            .OrderBy(pr => pr.Id)
            .ToList();

        _pullRequests.Clear();
        _pullRequests.AddRange(pullRequests);

        OnPullRequestsChanged?.Invoke(this, EventArgs.Empty);
    }
}