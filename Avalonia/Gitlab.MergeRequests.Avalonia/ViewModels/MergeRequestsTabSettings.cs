namespace DevOpsUtil.Gitlab.MergeRequests.Avalonia.ViewModels;

using DevOpsUtil.Avalonia.Core;
using Microsoft.Extensions.Configuration;

public class MergeRequestsTabSettings : TabSettings
{
    public string GitlabSettingsKey { get; set; } = string.Empty;

    public static new MergeRequestsTabSettings Load(IConfigurationSection section)
    {
        return section.Get<MergeRequestsTabSettings>() ??
               throw new InvalidOperationException("Unable to read tab settings.");
    }
}