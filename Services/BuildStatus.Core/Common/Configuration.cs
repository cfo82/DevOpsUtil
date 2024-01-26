namespace DevOpsUtil.BuildStatus.Core.Common;

using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;
using DevOpsUtil.BuildStatus.Core.Interfaces;
using Newtonsoft.Json;

public abstract class Configuration : IConfiguration
{
    private readonly ConfigurationData _data;

    public Configuration()
    {
        string directory = Path.GetDirectoryName(typeof(Configuration).GetTypeInfo().Assembly.Location);
        string path = Path.Combine(directory, "Configuration.json");
        _data = JsonConvert.DeserializeObject<ConfigurationData>(File.ReadAllText(path));

        // make sure to have a slash at the end of the base address
        if (!_data.BaseAddress.EndsWith("/"))
        {
            _data.BaseAddress = _data.BaseAddress + "/";
        }
    }

    public string UserName => _data.UserName;

    public string BuildAccessToken => _data.BuildAccessToken;

    public string BaseAddress => _data.BaseAddress;

    public int PollingInterval => _data.PollingInterval;

    public int BlinkingInterval => _data.BlinkingInterval;

    public int DefinitionListingInterval => _data.DefinitionListingInterval;

    public string[] BuildsToIgnore
    {
        get
        {
            var copy = new string[_data.BuildsToIgnore.Length];
            Array.Copy(_data.BuildsToIgnore, copy, copy.Length);
            return copy;
        }
    }

    public string[] ReleasesToIgnore
    {
        get
        {
            var copy = new string[_data.ReleasesToIgnore.Length];
            Array.Copy(_data.ReleasesToIgnore, copy, copy.Length);
            return copy;
        }
    }

    public abstract string LocalFolderPath { get; }

    public bool ShouldIgnoreBuild(string buildDefinitionName)
    {
        foreach (var str in BuildsToIgnore)
        {
            if (Regex.IsMatch(buildDefinitionName, WildCardToRegular(str)))
            {
                return true;
            }
        }

        return false;
    }

    public bool ShouldIgnoreRelease(string releaseDefinitionName)
    {
        foreach (var str in ReleasesToIgnore)
        {
            if (Regex.IsMatch(releaseDefinitionName, WildCardToRegular(str)))
            {
                return true;
            }
        }

        return false;
    }

    // https://stackoverflow.com/questions/30299671/matching-strings-with-wildcard
    // If you want to implement both "*" and "?"
    private static string WildCardToRegular(string value)
    {
        return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
    }
}
