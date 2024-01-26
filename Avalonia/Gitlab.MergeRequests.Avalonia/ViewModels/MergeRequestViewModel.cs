namespace DevOpsUtil.Gitlab.MergeRequests.Avalonia.ViewModels;

using System.Collections.Immutable;
using System.Windows.Input;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Gitlab.Core.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.Contracts;
using Prism.Commands;
using Prism.Mvvm;

public class MergeRequestViewModel : BindableBase
{
    private readonly IBrowserService _browserService;
    private readonly IMergeRequest _mergeRequest;

    public MergeRequestViewModel(
        IBrowserService browserService,
        IGitlabSettingsService gitlabSettingsService,
        IMergeRequest mergeRequest,
        MergeRequestsTabSettings settings)
    {
        var gitlabSettings = gitlabSettingsService.Get(settings.GitlabSettings);

        _browserService = browserService;
        _mergeRequest = mergeRequest;

        var lastReviewer = _mergeRequest.Reviewers.LastOrDefault();
        Reviewers = _mergeRequest.Reviewers
            .Select(reviewer => new MergeRequestReviewerViewModel(
                reviewer,
                reviewer != lastReviewer))
            .ToImmutableArray();

        ApprovedByMe =
            !_mergeRequest.Reviewers.Any(item => string.Equals(item.UserName, gitlabSettings.UserName))
            || _mergeRequest.Reviewers.Any(item => string.Equals(item.UserName, gitlabSettings.UserName) && item.HasApproved);

        OpenUrlCommand = new DelegateCommand<string>(OpenUrl);
    }

    public bool ApprovedByMe { get; }

    public bool NotApprovedByMe => !ApprovedByMe;

    public string Id => _mergeRequest.Id;

    public bool HasUri => _mergeRequest.WebUrl != null;

    public string Uri => _mergeRequest.WebUrl ?? string.Empty;

    public string Title => _mergeRequest.Title;

    public string Project => _mergeRequest.Project.Name;

    public string ProjectId => _mergeRequest.Project.Id.ToString();

    public ICommand OpenUrlCommand { get; }

    public IEnumerable<MergeRequestReviewerViewModel> Reviewers { get; set; }

    private void OpenUrl(string url)
    {
        _browserService.OpenUrl(new Uri(url));
    }
}