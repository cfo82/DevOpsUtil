namespace DevOpsUtil.Gitlab.Pipelines.Contracts;

using System.Collections.Immutable;

public interface IPipelinesService
{
    event EventHandler? ProjectsChanged;

    ImmutableArray<IProject> Projects { get; }
}