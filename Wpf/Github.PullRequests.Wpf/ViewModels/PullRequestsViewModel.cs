namespace DevOpsUtil.Github.PullRequests.Wpf.ViewModels;

using System.Collections.ObjectModel;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Github.Core.Contracts;
using DevOpsUtil.Github.PullRequests.Contracts;

public class PullRequestsViewModel
{
    private readonly IBrowserService _browserService;
    private readonly IGithubSettingsService _githubSettingsService;
    private readonly IPullRequestService _pullRequestService;
    private readonly PullRequestsTabSettings _settings;

    public PullRequestsViewModel(
        IBrowserService browserService,
        IGithubSettingsService githubSettingsService,
        IPullRequestService pullRequestService,
        string title,
        PullRequestsTabSettings settings)
    {
        _browserService = browserService;
        _githubSettingsService = githubSettingsService;
        _pullRequestService = pullRequestService;
        _settings = settings;
        Header = title;
        PullRequests = new ObservableCollection<PullRequestViewModel>();

        _pullRequestService.OnPullRequestsChanged += PullRequestsChanged;
    }

    public string Header { get; }

    public ObservableCollection<PullRequestViewModel> PullRequests { get; }

    private void PullRequestsChanged(object? sender, EventArgs e)
    {
        PullRequests.Clear();
        foreach (var pullRequest in _pullRequestService.PullRequests)
        {
            PullRequests.Add(new PullRequestViewModel(
                _browserService,
                _githubSettingsService,
                pullRequest,
                _settings));
        }
    }
}