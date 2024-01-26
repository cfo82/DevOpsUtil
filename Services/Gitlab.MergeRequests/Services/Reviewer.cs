namespace DevOpsUtil.Gitlab.MergeRequests.Services;

using DevOpsUtil.Gitlab.MergeRequests.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.GraphQL;

public class Reviewer : IReviewer
{
    public Reviewer(IQueryMergeRequests_Group_Projects_Nodes_MergeRequests_Nodes_Reviewers_Nodes reviewer, bool hasApproved)
    {
        Name = reviewer.Name;
        UserName = reviewer.Username;
        HasApproved = hasApproved;
    }

    public string Name { get; }

    public string UserName { get; }

    public bool HasApproved { get; }
}