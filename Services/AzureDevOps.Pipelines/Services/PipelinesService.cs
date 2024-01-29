namespace DevOpsUtil.AzureDevOps.Pipelines.Services;

using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using System.Threading.Tasks;
using DevOpsUtil.AzureDevOps.Core.Contracts;
using DevOpsUtil.AzureDevOps.Pipelines.Contracts;
using DevOpsUtil.Core.Contracts;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

public class PipelinesService : IPipelinesService, IRefreshable, IErrorStateProvider
{
    private readonly IPipelinesServiceSettings _settings;
    private readonly AzureDevOpsSettings _azureDevOpsSettings;
    private readonly VssConnection _vssConnection;
    private readonly List<IBuildDefinition> _buildDefinitions;

    public PipelinesService(
        IRefreshService refreshService,
        IErrorStateService errorStateService,
        IAzureDevOpsSettingsService azureDevOpsSettingsService,
        IPipelinesServiceSettings settings)
    {
        _settings = settings;
        _azureDevOpsSettings = azureDevOpsSettingsService.Get(settings.AzureDevOpsSettingsKey);
        _buildDefinitions = new List<IBuildDefinition>();

        var baseAddressUri = new Uri($"{_azureDevOpsSettings.BaseAddress}/{_azureDevOpsSettings.Organization}");

        _vssConnection = new VssConnection(baseAddressUri, new VssBasicCredential(string.Empty, _azureDevOpsSettings.PersonalAccessToken));

        refreshService.RegisterRefreshable(this);
        errorStateService.SubscribeErrorProvider(this);
    }

    public event EventHandler? BuildDefinitionsChanged;

    public event EventHandler? ErrorStateChanged;

    public ImmutableArray<IBuildDefinition> BuildDefinitions => _buildDefinitions.ToImmutableArray();

    public bool HasError => _buildDefinitions.Count > 0 && _buildDefinitions.Any(definition => definition.HasFailed);

    public async Task RefreshAsync(bool forceRefresh)
    {
        var buildHttpClient = _vssConnection.GetClient<BuildHttpClient>();

        _buildDefinitions.Clear();

        var definitions = await buildHttpClient.GetDefinitionsAsync(_azureDevOpsSettings.Project);

        foreach (var definition in definitions)
        {
            _buildDefinitions.Add(new BuildDefinition(_settings, _azureDevOpsSettings, definition));
        }

        foreach (var definition in _buildDefinitions)
        {
            await definition.UpdateAsync(buildHttpClient);
        }

        BuildDefinitionsChanged?.Invoke(this, EventArgs.Empty);
        ErrorStateChanged?.Invoke(this, EventArgs.Empty);
    }
}
