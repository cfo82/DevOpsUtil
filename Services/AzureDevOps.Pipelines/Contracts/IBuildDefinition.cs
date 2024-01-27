namespace DevOpsUtil.AzureDevOps.Pipelines.Contracts;

using System;
using System.Collections.Immutable;
using Microsoft.TeamFoundation.Build.WebApi;

/// <summary>
/// One element in the list of items shown on the UI. This can be either a build or a release for example.
/// </summary>
public interface IBuildDefinition
{
    event EventHandler Changed;

    int Id { get; }

    bool IsIgnored { get; }

    bool IsDraft { get; }

    bool IsDisabled { get; }

    bool WasSuccessful { get; }

    bool HasFailed { get; }

    bool IsRunning { get; }

    string Name { get; }

    Uri Uri { get; }

    ImmutableArray<IBuild> Builds { get;  }

    Task UpdateAsync(BuildHttpClient buildHttpClient);
}
