namespace DevOpsUtil.Github.Core.Services;

using DevOpsUtil.Github.Core.Contracts;
using Microsoft.Extensions.Configuration;

public class GithubSettingsService : IGithubSettingsService
{
    private readonly IDictionary<string, GithubSettings> _settings;

    public GithubSettingsService(IConfiguration configuration)
    {
        var settings = GithubSettings.Load(configuration);
        _settings = settings.ToDictionary(item => item.Key, item => item);
    }

    public GithubSettings Get(string key)
    {
        if (!_settings.ContainsKey(key))
        {
            throw new KeyNotFoundException($"Unable to find key {key}.");
        }

        return _settings[key];
    }
}