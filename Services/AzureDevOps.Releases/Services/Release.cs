namespace DevOpsUtil.AzureDevOps.Releases.Services;

using System.Collections.Generic;
using System.Collections.Immutable;
using DevOpsUtil.AzureDevOps.Core.Contracts;
using DevOpsUtil.AzureDevOps.Releases.Contracts;

public class Release : IRelease
{
    private readonly AzureDevOpsSettings _settings;
    private readonly Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Release _release;
    private readonly List<IEnvironment> _environments;

    public Release(AzureDevOpsSettings settings, Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Release release)
    {
        _settings = settings;
        _release = release;
        _environments = new List<IEnvironment>();
        foreach (var environment in release.Environments)
        {
            _environments.Add(new Environment(environment));
        }
    }

    public int Id => _release.Id;

    public int Age => (int)(DateTime.Now - _release.CreatedOn).TotalDays;

    public bool WasSuccessful =>
        _release.Environments.All(x =>
            x.Status == Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.EnvironmentStatus.Succeeded ||
            x.Status == Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.EnvironmentStatus.NotStarted);

    public bool HasFailed =>
        // basically failed is everything that is not WasSuccessful and not IsRunning which would
        // mean expected states would be 'rejected' or 'partiallySucceeded' in case it needs to be coded
        // explicitly once.
        !WasSuccessful && !IsRunning;

    public bool IsRunning =>
        _release.Environments.Any(x => x.Status == Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.EnvironmentStatus.InProgress);

    public string Name => _release.Name;

    public Uri Uri => new Uri($"{_settings.BaseWebAddress}{_settings.Project}/_apps/hub/ms.vss-releaseManagement-web.cd-release-progress?_a=release-pipeline-progress&releaseId={Id}");

    public ImmutableArray<IEnvironment> Environments => _environments.ToImmutableArray();
}
