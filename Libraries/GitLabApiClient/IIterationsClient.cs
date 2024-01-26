namespace GitLabApiClient;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Iterations.Requests;
using GitLabApiClient.Models.Iterations.Responses;

public interface IIterationsClient
{
    Task<IList<Iteration>> GetAsync(
        ProjectId projectId = null,
        GroupId groupId = null,
        Action<IterationsQueryOptions> options = null);
}
