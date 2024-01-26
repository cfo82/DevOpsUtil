namespace DevOpsUtil.Gitlab.Pipelines.Services;

using System.Text.RegularExpressions;
using DevOpsUtil.Gitlab.Pipelines.Contracts;

public class PipelinesServiceSettings : IPipelinesServiceSettings
{
    public string GitlabSettingsKey { get; init; } = string.Empty;

    public string[] ProjectsToIgnore { get; init; } = Array.Empty<string>();

    public string[] BranchesToWatch { get; init; } = Array.Empty<string>();

    public bool ShouldIgnoreProject(string projectName)
    {
        return ProjectsToIgnore
            .Any(pattern => Regex.IsMatch(projectName, WildCardToRegular(pattern)));
    }

    // https://stackoverflow.com/questions/30299671/matching-strings-with-wildcard
    // If you want to implement both "*" and "?"
    private static string WildCardToRegular(string value)
    {
        return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
    }
}