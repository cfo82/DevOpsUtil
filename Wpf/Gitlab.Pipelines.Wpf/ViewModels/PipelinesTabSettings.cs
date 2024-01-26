namespace DevOpsUtil.Gitlab.Pipelines.Wpf.ViewModels;

using DevOpsUtil.Wpf.Core;
using Microsoft.Extensions.Configuration;

public class PipelinesTabSettings : TabSettings
{
    public string GitlabSettings { get; set; } = string.Empty;

    public string[] ProjectsToIgnore { get; init; } = Array.Empty<string>();

    public string[] BranchesToWatch { get; init; } = Array.Empty<string>();

    public static new PipelinesTabSettings Load(IConfigurationSection section)
    {
        return section.Get<PipelinesTabSettings>() ??
               throw new InvalidOperationException("Unable to read tab settings.");
    }
}