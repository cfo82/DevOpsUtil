namespace DevOpsUtil.AzureDevOps.Releases.Services;

using System;
using System.Threading.Tasks;
using DevOpsUtil.AzureDevOps.Core.Contracts;
using DevOpsUtil.AzureDevOps.Releases.Contracts;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

public class ReleaseDefinition : IReleaseDefinition
{
    private readonly IReleasesServiceSettings _settings;
    private readonly AzureDevOpsSettings _azureDevOpsSettings;
    private readonly Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.ReleaseDefinition _releaseDefinition;
    private IRelease? _latestRelease;

    public ReleaseDefinition(
        IReleasesServiceSettings settings,
        AzureDevOpsSettings azureDevOpsSettings,
        Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.ReleaseDefinition buildDefinition)
    {
        _settings = settings;
        _azureDevOpsSettings = azureDevOpsSettings;
        _releaseDefinition = buildDefinition;
    }

    public event EventHandler? Changed;

    public int Id => _releaseDefinition.Id;

    public bool IsIgnored => _settings.ShouldIgnoreReleaseDefinition(Name);

    public bool WasSuccessful => !IsIgnored && !IsRunning && _latestRelease != null && _latestRelease.WasSuccessful;

    public bool HasFailed => !IsIgnored && (_latestRelease == null || _latestRelease.HasFailed);

    public bool IsRunning => !IsIgnored && _latestRelease != null && _latestRelease.IsRunning;

    public string Name => _releaseDefinition.Name;

    public string Uri => _releaseDefinition.Url;

    public IRelease? LatestRelease => _latestRelease;

    public async Task UpdateAsync(ReleaseHttpClient buildHttpClient)
    {
        if (IsIgnored)
        {
            return;
        }

        _latestRelease = null;

        // project: _releaseDefinition.ProjectReference.Id,
        var releases = await buildHttpClient.GetReleasesAsync(
            definitionId: _releaseDefinition.Id,
            expand: Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Contracts.ReleaseExpands.Environments,
            top: 1);

        if (releases.Count > 0)
        {
            _latestRelease = new Release(_azureDevOpsSettings, releases[0]);
        }

        Changed?.Invoke(this, EventArgs.Empty);
    }
}
