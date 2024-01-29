namespace DevOpsUtil.AzureDevOps.Releases.Wpf.ViewModels;

using DevOpsUtil.Wpf.Core;
using Microsoft.Extensions.Configuration;

public class ReleasesTabSettings : TabSettings
{
    public string AzureDevOpsSettingsKey { get; set; } = string.Empty;

    public string[] ReleaseDefinitionstoIgnore { get; init; } = Array.Empty<string>();

    public static new ReleasesTabSettings Load(IConfigurationSection section)
    {
        return section.Get<ReleasesTabSettings>() ??
               throw new InvalidOperationException("Unable to read tab settings.");
    }
}