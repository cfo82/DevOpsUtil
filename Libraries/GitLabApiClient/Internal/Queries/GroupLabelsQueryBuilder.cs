namespace GitLabApiClient.Internal.Queries;

using GitLabApiClient.Models.Groups.Requests;

internal class GroupLabelsQueryBuilder : QueryBuilder<GroupLabelsQueryOptions>
{
    /// <inheritdoc />
    protected override void BuildCore(Query query, GroupLabelsQueryOptions options)
    {
        if (options.WithCounts)
        {
            query.Add("with_counts", true);
        }

        if (!options.IncludeAncestorGroups)
        {
            query.Add("include_ancestor_groups", false);
        }
    }
}
