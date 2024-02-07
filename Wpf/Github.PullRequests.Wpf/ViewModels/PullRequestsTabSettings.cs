namespace DevOpsUtil.Github.PullRequests.Wpf.ViewModels;

using DevOpsUtil.Wpf.Core;
using Microsoft.Extensions.Configuration;

public class PullRequestsTabSettings : TabSettings
{
    public string GithubSettingsKey { get; set; } = string.Empty;

    public static new PullRequestsTabSettings Load(IConfigurationSection section)
    {
        return section.Get<PullRequestsTabSettings>() ??
               throw new InvalidOperationException("Unable to read tab settings.");
    }
}