namespace DevOpsUtil.AzureDevOps.Pipelines.Contracts;

public interface IBuild
{
    string BuildNumber { get; }

    bool IsRunning { get; }

    bool WasSuccessful { get; }

    bool HasFailed { get; }

    /// <summary>
    /// Age of the build in days.
    /// </summary>
    int Age { get; }

    Uri Uri { get; }

    string SourceBranch { get; }
}
