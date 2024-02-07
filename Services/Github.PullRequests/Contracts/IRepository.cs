namespace DevOpsUtil.Github.PullRequests.Contracts;

public interface IRepository
{
    long Id { get; }

    string Name { get; }
}