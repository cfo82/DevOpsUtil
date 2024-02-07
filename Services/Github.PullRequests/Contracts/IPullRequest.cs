namespace DevOpsUtil.Github.PullRequests.Contracts;

public interface IPullRequest
{
    long Id { get; }

    int Number { get; }

    string Title { get; }

    public string WebUrl { get; }

    IRepository Repository { get; }

    IEnumerable<IReviewer> Reviewers { get; }
}