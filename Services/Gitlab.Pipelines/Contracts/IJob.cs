namespace DevOpsUtil.Gitlab.Pipelines.Contracts;

public interface IJob
{
    string Name { get; }

    bool IsRunning { get; }

    bool WasSuccessful { get; }

    bool HasFailed { get; }
}