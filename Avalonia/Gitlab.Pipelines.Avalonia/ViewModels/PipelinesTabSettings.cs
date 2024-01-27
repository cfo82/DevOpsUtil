namespace DevOpsUtil.Gitlab.Pipelines.Avalonia.ViewModels;

using DevOpsUtil.Avalonia.Core;
using Microsoft.Extensions.Configuration;

public class PipelinesTabSettings : TabSettings
{
    public string GitlabSettingsKey { get; set; } = string.Empty;

    public string[] ProjectsToIgnore { get; init; } = Array.Empty<string>();

    public string[] BranchesToWatch { get; init; } = Array.Empty<string>();

    public static new PipelinesTabSettings Load(IConfigurationSection section)
    {
        return section.Get<PipelinesTabSettings>() ??
               throw new InvalidOperationException("Unable to read tab settings.");
    }
}