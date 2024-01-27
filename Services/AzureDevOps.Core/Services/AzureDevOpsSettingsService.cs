namespace DevOpsUtil.AzureDevOps.Core.Services;

using DevOpsUtil.AzureDevOps.Core.Contracts;
using Microsoft.Extensions.Configuration;

public class AzureDevOpsSettingsService : IAzureDevOpsSettingsService
{
    private readonly IDictionary<string, AzureDevOpsSettings> _settings;

    public AzureDevOpsSettingsService(IConfiguration configuration)
    {
        var settings = AzureDevOpsSettings.Load(configuration);
        _settings = settings.ToDictionary(item => item.Key, item => item);
    }

    public AzureDevOpsSettings Get(string key)
    {
        if (!_settings.ContainsKey(key))
        {
            throw new KeyNotFoundException($"Unable to find key {key}.");
        }

        return _settings[key];
    }
}