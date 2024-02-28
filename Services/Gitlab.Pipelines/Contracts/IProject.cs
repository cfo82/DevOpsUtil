namespace DevOpsUtil.Gitlab.Pipelines.Contracts;

using System.Collections.Immutable;

public interface IProject
{
    bool IsIgnored { get; }

    bool IsRunning { get; }

    bool WasSuccessful { get; }

    bool HasFailed { get; }

    bool IsArchived { get; }

    string Name { get; }

    ImmutableArray<IPipeline> Pipelines { get; }
}