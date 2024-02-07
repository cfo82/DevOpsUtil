namespace DevOpsUtil.Github.PullRequests.Wpf.ViewModels;

using DevOpsUtil.Github.PullRequests.Contracts;

public class PullRequestReviewerViewModel
{
    public PullRequestReviewerViewModel(IReviewer reviewer, bool notLastReviewer)
    {
        Name = reviewer.Name;
        HasApproved = reviewer.HasApproved;
        NotLastReviewer = notLastReviewer;
    }

    public bool NotLastReviewer { get; }

    public bool HasApproved { get; }

    public string Name { get; }
}