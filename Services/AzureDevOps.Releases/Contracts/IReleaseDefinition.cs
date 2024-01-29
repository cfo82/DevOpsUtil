namespace DevOpsUtil.AzureDevOps.Releases.Contracts;

using System;
using System.Collections.Immutable;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

/// <summary>
/// One element in the list of items shown on the UI. This can be either a build or a release for example.
/// </summary>
public interface IReleaseDefinition
{
    event EventHandler Changed;

    int Id { get; }

    bool IsIgnored { get; }

    bool WasSuccessful { get; }

    bool HasFailed { get; }

    bool IsRunning { get; }

    string Name { get; }

    string Uri { get; }

    IRelease? LatestRelease { get; }

    Task UpdateAsync(ReleaseHttpClient buildHttpClient);
}
