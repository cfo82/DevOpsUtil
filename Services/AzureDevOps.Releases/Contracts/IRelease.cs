namespace DevOpsUtil.AzureDevOps.Releases.Contracts;

using System.Collections.Immutable;

public interface IRelease
{
    int Id { get; }

    /// <summary>
    /// Gets the age of the release in days.
    /// </summary>
    int Age { get; }

    bool WasSuccessful { get; }

    bool HasFailed { get; }

    bool IsRunning { get; }

    string Name { get; }

    Uri Uri { get; }

    public ImmutableArray<IEnvironment> Environments { get; }
}
