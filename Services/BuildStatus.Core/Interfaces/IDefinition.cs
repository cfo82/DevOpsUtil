namespace DevOpsUtil.BuildStatus.Core.Interfaces;

using System;

/// <summary>
/// One element in the list of items shown on the UI. This can be either a build or a release for example.
/// </summary>
public interface IDefinition
{
    event EventHandler Changed;

    string Type { get; }

    bool WasSuccessful { get; }

    bool IsDisabled { get; }

    bool IsIgnored { get; }

    string Name { get; }

    bool HasUri { get; }

    Uri Uri { get; }

    bool HasRunInstance { get; }

    bool HasFailed { get; }

    bool IsRunning { get; }

    string Result { get; }

    /// <summary>
    /// Age of the build in days.
    /// </summary>
    int Age { get; }
}
