namespace DevOpsUtil.AzureDevOps.Releases.Contracts;

public interface IEnvironment
{
    public string Name { get; }

    bool WasSuccessful { get; }

    bool HasFailed { get; }

    bool IsRunning { get; }
}
