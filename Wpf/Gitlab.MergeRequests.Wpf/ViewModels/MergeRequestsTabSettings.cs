namespace DevOpsUtil.Gitlab.MergeRequests.Wpf.ViewModels;

using DevOpsUtil.Wpf.Core;
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