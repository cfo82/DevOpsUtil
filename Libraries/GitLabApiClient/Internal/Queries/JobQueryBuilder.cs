namespace GitLabApiClient.Internal.Queries;

using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Job.Requests;

internal sealed class JobQueryBuilder : QueryBuilder<JobQueryOptions>
{
    /// <inheritdoc />
    protected override void BuildCore(Query query, JobQueryOptions options)
    {
        if (options.Scope != JobScope.All)
        {
            query.Add("scope", options.Scope.ToLowerCaseString());
        }
    }
}
