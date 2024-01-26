namespace GitLabApiClient.Internal.Queries;

using GitLabApiClient.Models.Branches.Requests;

internal class BranchQueryBuilder : QueryBuilder<BranchQueryOptions>
{
    protected override void BuildCore(Query query, BranchQueryOptions options)
    {
        if (!string.IsNullOrEmpty(options.Search))
        {
            query.Add("search", options.Search);
        }
    }
}
