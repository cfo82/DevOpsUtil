namespace DevOpsUtil.Github.PullRequests.Services;

using DevOpsUtil.Github.PullRequests.Contracts;

public class Repository : IRepository
{
    public Repository(Octokit.Repository repository)
    {
        Id = repository.Id;
        Name = repository.Name;
    }

    public long Id { get; }

    public string Name { get; }
}