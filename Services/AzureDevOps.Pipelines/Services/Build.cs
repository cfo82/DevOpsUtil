namespace DevOpsUtil.AzureDevOps.Pipelines.Services;

using DevOpsUtil.AzureDevOps.Pipelines.Contracts;

public class Build : IBuild
{
    private readonly Microsoft.TeamFoundation.Build.WebApi.Build _build;

    public Build(Microsoft.TeamFoundation.Build.WebApi.Build build)
    {
        _build = build;
    }

    public string BuildNumber => _build.BuildNumber;

    public bool IsRunning => _build.Status == Microsoft.TeamFoundation.Build.WebApi.BuildStatus.None ||
                             _build.Status == Microsoft.TeamFoundation.Build.WebApi.BuildStatus.NotStarted ||
                             _build.Status == Microsoft.TeamFoundation.Build.WebApi.BuildStatus.Postponed ||
                             _build.Status == Microsoft.TeamFoundation.Build.WebApi.BuildStatus.InProgress;

    public bool WasSuccessful => _build.Status == Microsoft.TeamFoundation.Build.WebApi.BuildStatus.Completed &&
                                 _build.Result == Microsoft.TeamFoundation.Build.WebApi.BuildResult.Succeeded;

    public bool HasFailed => !IsRunning && !WasSuccessful;

    public int Age
    {
        get
        {
            var diff = DateTime.Now - _build.StartTime;

            return (int)diff.GetValueOrDefault(TimeSpan.FromDays(0)).TotalDays;
        }
    }

    public Uri Uri => _build.Uri;

    public string SourceBranch => _build.SourceBranch;
}
