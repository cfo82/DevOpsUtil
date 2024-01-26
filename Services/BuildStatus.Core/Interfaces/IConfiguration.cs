namespace DevOpsUtil.BuildStatus.Core.Interfaces;

public interface IConfiguration
{
    string UserName { get; }

    string BuildAccessToken { get; }

    string BaseAddress { get; }

    int PollingInterval { get; }

    string[] BuildsToIgnore { get; }

    string[] ReleasesToIgnore { get; }

    int BlinkingInterval { get; }

    /// <summary>
    /// Interval in milliseconds in which the build definitions and release definitions are refreshed.
    /// </summary>
    int DefinitionListingInterval { get; }

    string LocalFolderPath { get; }

    bool ShouldIgnoreBuild(string buildDefinitionName);

    bool ShouldIgnoreRelease(string releaseDefinitionName);
}
