namespace DevOpsUtil.Github.PullRequests.Services;

using DevOpsUtil.Github.PullRequests.Contracts;

public class Reviewer : IReviewer
{
    public Reviewer(Octokit.User reviewer, bool hasApproved)
    {
        Name = reviewer.Name;
        UserName = reviewer.Login;
        HasApproved = hasApproved;
    }

    public string Name { get; }

    public string UserName { get; }

    public bool HasApproved { get; }
}