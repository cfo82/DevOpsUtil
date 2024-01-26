namespace GitLabApiClient.Internal.Queries;

using System;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Notes.Requests;

internal class ProjectMergeRequestsApprovalQueryBuilder : QueryBuilder<MergeRequestApprovalQueryOptions>
{
    protected override void BuildCore(Query query, MergeRequestApprovalQueryOptions options)
    {
    }
}
