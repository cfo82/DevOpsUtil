namespace DevOpsUtil.Github.PullRequests.Wpf.ViewModels;

using System.Collections.Immutable;
using System.Windows.Input;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Github.Core.Contracts;
using DevOpsUtil.Github.PullRequests.Contracts;
using Prism.Commands;
using Prism.Mvvm;

public class PullRequestViewModel : BindableBase
{
    private readonly IBrowserService _browserService;
    private readonly IPullRequest _pullRequest;

    public PullRequestViewModel(
        IBrowserService browserService,
        IGithubSettingsService githubSettingsService,
        IPullRequest pullRequest,
        PullRequestsTabSettings settings)
    {
        var githubSettings = githubSettingsService.Get(settings.GithubSettingsKey);

        _browserService = browserService;
        _pullRequest = pullRequest;

        var lastReviewer = _pullRequest.Reviewers.LastOrDefault();
        Reviewers = _pullRequest.Reviewers
            .Select(reviewer => new PullRequestReviewerViewModel(
                reviewer,
                reviewer != lastReviewer))
            .ToImmutableArray();

        ApprovedByMe =
            !_pullRequest.Reviewers.Any(item => string.Equals(item.UserName, githubSettings.UserName))
            || _pullRequest.Reviewers.Any(item => string.Equals(item.UserName, githubSettings.UserName) && item.HasApproved);

        OpenUrlCommand = new DelegateCommand<string>(OpenUrl);
    }

    public bool ApprovedByMe { get; }

    public bool NotApprovedByMe => !ApprovedByMe;

    public string Id => _pullRequest.Id.ToString();

    public bool HasUri => _pullRequest.WebUrl != null;

    public string Uri => _pullRequest.WebUrl ?? string.Empty;

    public string Title => _pullRequest.Title;

    public string Repository => _pullRequest.Repository.Name;

    public ICommand OpenUrlCommand { get; }

    public IEnumerable<PullRequestReviewerViewModel> Reviewers { get; set; }

    private void OpenUrl(string url)
    {
        _browserService.OpenUrl(new Uri(url));
    }
}