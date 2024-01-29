namespace DevOpsUtil.AzureDevOps.Releases.Services;

using DevOpsUtil.AzureDevOps.Releases.Contracts;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;

public class Environment : IEnvironment
{
    private ReleaseEnvironment _environment;

    public Environment(ReleaseEnvironment environment)
    {
        _environment = environment;
    }

    public bool WasSuccessful => _environment.Status == EnvironmentStatus.Succeeded;

    public bool HasFailed =>
        // basically failed is everything that is not WasSuccessful and not IsRunning which would
        // mean expected states would be 'rejected' or 'partiallySucceeded' in case it needs to be coded
        // explicitly once.
        !WasSuccessful && !IsRunning;

    public bool IsRunning =>
        _environment.Status == EnvironmentStatus.Scheduled ||
        _environment.Status == EnvironmentStatus.Queued ||
        _environment.Status == EnvironmentStatus.NotStarted ||
        _environment.Status == EnvironmentStatus.InProgress;

    public string Name => _environment.Name;
}
