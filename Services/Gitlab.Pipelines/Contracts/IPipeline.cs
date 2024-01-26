namespace DevOpsUtil.Gitlab.Pipelines.Contracts;

using System.Collections.Immutable;

public interface IPipeline
{
    bool IsRunning { get; }

    bool WasSuccessful { get; }

    bool HasFailed { get; }

    public string Ref { get; }

    public string StatusText { get; }

    Uri Uri { get; }

    int Age { get; }

    ImmutableArray<IJob> Jobs { get; }
}