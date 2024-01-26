namespace DevOpsUtil.Gitlab.Core.Services;

using DevOpsUtil.Gitlab.Core.Contracts;
using Microsoft.Extensions.Configuration;

public class GitlabSettingsService : IGitlabSettingsService
{
    private readonly IDictionary<string, GitlabSettings> _settings;

    public GitlabSettingsService(IConfiguration configuration)
    {
        var settings = GitlabSettings.Load(configuration);
        _settings = settings.ToDictionary(item => item.Key, item => item);
    }

    public GitlabSettings Get(string key)
    {
        if (!_settings.ContainsKey(key))
        {
            throw new KeyNotFoundException($"Unable to find key {key}.");
        }

        return _settings[key];
    }
}