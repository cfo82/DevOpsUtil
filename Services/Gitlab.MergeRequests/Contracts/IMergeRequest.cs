namespace DevOpsUtil.Gitlab.MergeRequests.Contracts;

public interface IMergeRequest
{
    string Title { get; }

    string Id { get; }

    public string? WebUrl { get; }

    IProject Project { get; }

    IEnumerable<IReviewer> Reviewers { get; }
}