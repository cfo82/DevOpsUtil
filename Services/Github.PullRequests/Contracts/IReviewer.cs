namespace DevOpsUtil.Github.PullRequests.Contracts;

public interface IReviewer
{
    public string Name { get; }

    public string UserName { get; }

    public bool HasApproved { get; }
}