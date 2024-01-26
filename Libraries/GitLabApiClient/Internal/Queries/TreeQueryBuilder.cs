namespace GitLabApiClient.Internal.Queries;

using GitLabApiClient.Models.Trees.Requests;

internal class TreeQueryBuilder : QueryBuilder<TreeQueryOptions>
{
    protected override void BuildCore(Query query, TreeQueryOptions options)
    {
        if (!string.IsNullOrEmpty(options.Reference))
        {
            query.Add("ref", options.Reference);
        }

        if (!string.IsNullOrEmpty(options.Path))
        {
            query.Add("path", options.Path);
        }

        if (options.Recursive)
        {
            query.Add("recursive", options.Recursive);
        }
    }
}
