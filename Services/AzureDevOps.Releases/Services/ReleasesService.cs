namespace DevOpsUtil.AzureDevOps.Releases.Services;

using System.Collections.Immutable;
using System.Threading.Tasks;
using DevOpsUtil.AzureDevOps.Core.Contracts;
using DevOpsUtil.AzureDevOps.Releases.Contracts;
using DevOpsUtil.Core.Contracts;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.WebApi;

public class ReleasesService : IReleasesService, IRefreshable, IErrorStateProvider
{
    private readonly IReleasesServiceSettings _settings;
    private readonly AzureDevOpsSettings _azureDevOpsSettings;
    private readonly VssConnection _vssConnection;
    private readonly List<IReleaseDefinition> _releaseDefinitions;

    public ReleasesService(
        IRefreshService refreshService,
        IErrorStateService errorStateService,
        IAzureDevOpsSettingsService azureDevOpsSettingsService,
        IReleasesServiceSettings settings)
    {
        _settings = settings;
        _azureDevOpsSettings = azureDevOpsSettingsService.Get(settings.AzureDevOpsSettingsKey);
        _releaseDefinitions = new List<IReleaseDefinition>();

        var baseAddressUri = new Uri($"{_azureDevOpsSettings.BaseAddress}/{_azureDevOpsSettings.Organization}");

        _vssConnection = new VssConnection(baseAddressUri, new VssBasicCredential(string.Empty, _azureDevOpsSettings.PersonalAccessToken));

        refreshService.RegisterRefreshable(this);
        errorStateService.SubscribeErrorProvider(this);
    }

    public event EventHandler? ReleaseDefinitionsChanged;

    public event EventHandler? ErrorStateChanged;

    public ImmutableArray<IReleaseDefinition> ReleaseDefinitions => _releaseDefinitions.ToImmutableArray();

    public bool HasError => _releaseDefinitions.Count > 0 && _releaseDefinitions.Any(definition => definition.HasFailed);

    public async Task RefreshAsync(bool forceRefresh)
    {
        var releaseHttpClient = _vssConnection.GetClient<ReleaseHttpClient>();

        _releaseDefinitions.Clear();

        var definitions = await releaseHttpClient.GetReleaseDefinitionsAsync(_azureDevOpsSettings.Project);

        foreach (var definition in definitions)
        {
            _releaseDefinitions.Add(new ReleaseDefinition(_settings, _azureDevOpsSettings, definition));
        }

        foreach (var definition in _releaseDefinitions)
        {
            await definition.UpdateAsync(releaseHttpClient);
        }

        ReleaseDefinitionsChanged?.Invoke(this, EventArgs.Empty);
        ErrorStateChanged?.Invoke(this, EventArgs.Empty);
    }
}
