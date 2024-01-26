namespace DevOpsUtil.Gitlab.MergeRequests.Services;

using DevOpsUtil.Gitlab.MergeRequests.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.GraphQL;

public class Project : IProject
{
    public Project(IQueryMergeRequests_Group_Projects_Nodes project)
    {
        Id = project.Id;
        Name = project.Name;
    }

    public string Id { get; }

    public string Name { get; }
}