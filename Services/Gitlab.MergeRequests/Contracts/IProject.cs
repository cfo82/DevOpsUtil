namespace DevOpsUtil.Gitlab.MergeRequests.Contracts;

public interface IProject
{
    string Id { get; }

    string Name { get; }
}