namespace DevOpsUtil.Gitlab.MergeRequests.Services;

using System.Collections.Immutable;
using DevOpsUtil.Gitlab.MergeRequests.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.GraphQL;

public class MergeRequest : IMergeRequest
{
    public MergeRequest(
        IProject project,
        IQueryMergeRequests_Group_Projects_Nodes_MergeRequests_Nodes mergeRequest)
    {
        Title = mergeRequest.Title;
        Id = mergeRequest.Id;
        WebUrl = mergeRequest.WebUrl;
        Project = project;

        var reviewers = new List<IReviewer>();
        foreach (var reviewer in mergeRequest.Reviewers?.Nodes ??
                                 new List<IQueryMergeRequests_Group_Projects_Nodes_MergeRequests_Nodes_Reviewers_Nodes>())
        {
            if (reviewer == null)
            {
                continue;
            }

            var hasApproved = mergeRequest.ApprovedBy?.Nodes?.Any(
                n => string.Equals(n?.Username, reviewer.Username)) ?? false;

            reviewers.Add(new Reviewer(reviewer, hasApproved));
        }

        Reviewers = reviewers.ToImmutableArray();
    }

    public string Title { get; }

    public string Id { get; }

    public string? WebUrl { get; }

    public IProject Project { get; }

    public IEnumerable<IReviewer> Reviewers { get; }
}