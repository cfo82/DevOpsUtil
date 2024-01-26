namespace DevOpsUtil.Gitlab.MergeRequests.Contracts;

using System.Collections.Immutable;

public interface IMergeRequestService
{
    event EventHandler<EventArgs>? OnMergeRequestsChanged;

    ImmutableArray<IMergeRequest> MergeRequests { get; }
}
