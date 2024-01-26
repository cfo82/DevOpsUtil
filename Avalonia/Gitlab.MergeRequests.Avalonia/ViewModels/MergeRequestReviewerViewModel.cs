namespace DevOpsUtil.Gitlab.MergeRequests.Avalonia.ViewModels;

using DevOpsUtil.Gitlab.MergeRequests.Contracts;

public class MergeRequestReviewerViewModel
{
    public MergeRequestReviewerViewModel(IReviewer reviewer, bool notLastReviewer)
    {
        Name = reviewer.Name;
        HasApproved = reviewer.HasApproved;
        NotLastReviewer = notLastReviewer;
    }

    public bool NotLastReviewer { get; }

    public bool HasApproved { get; }

    public string Name { get; }
}