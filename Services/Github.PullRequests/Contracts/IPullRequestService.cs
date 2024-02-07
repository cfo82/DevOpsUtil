namespace DevOpsUtil.Github.PullRequests.Contracts;

using System.Collections.Immutable;

public interface IPullRequestService
{
    event EventHandler<EventArgs>? OnPullRequestsChanged;

    ImmutableArray<IPullRequest> PullRequests { get; }
}
