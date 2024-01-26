namespace DevOpsUtil.Gitlab.MergeRequests.Wpf.ViewModels;

using System.Collections.ObjectModel;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Gitlab.Core.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.Contracts;

public class MergeRequestsViewModel
{
    private readonly IBrowserService _browserService;
    private readonly IGitlabSettingsService _gitlabSettingsService;
    private readonly IMergeRequestService _mergeRequestService;
    private readonly MergeRequestsTabSettings _settings;

    public MergeRequestsViewModel(
        IBrowserService browserService,
        IGitlabSettingsService gitlabSettingsService,
        IMergeRequestService mergeRequestService,
        string title,
        MergeRequestsTabSettings settings)
    {
        _browserService = browserService;
        _gitlabSettingsService = gitlabSettingsService;
        _mergeRequestService = mergeRequestService;
        _settings = settings;
        Header = title;
        MergeRequests = new ObservableCollection<MergeRequestViewModel>();

        _mergeRequestService.OnMergeRequestsChanged += MergeRequestsChanged;
    }

    public string Header { get; }

    public ObservableCollection<MergeRequestViewModel> MergeRequests { get; }

    private void MergeRequestsChanged(object? sender, EventArgs e)
    {
        MergeRequests.Clear();
        foreach (var mergeRequest in _mergeRequestService.MergeRequests)
        {
            MergeRequests.Add(new MergeRequestViewModel(
                _browserService,
                _gitlabSettingsService,
                mergeRequest,
                _settings));
        }
    }
}