namespace DevOpsUtil.AzureDevOps.Pipelines.Services;

using System.Text.RegularExpressions;
using DevOpsUtil.AzureDevOps.Pipelines.Contracts;

public class PipelinesServiceSettings : IPipelinesServiceSettings
{
    public string AzureDevOpsSettingsKey { get; init; } = string.Empty;

    public string[] BuildDefinitionstoIgnore { get; init; } = Array.Empty<string>();

    public string[] BranchesToWatch { get; init; } = Array.Empty<string>();

    public bool ShouldIgnoreBuildDefinition(string projectName)
    {
        return BuildDefinitionstoIgnore
            .Any(pattern => Regex.IsMatch(projectName, WildCardToRegular(pattern)));
    }

    // https://stackoverflow.com/questions/30299671/matching-strings-with-wildcard
    // If you want to implement both "*" and "?"
    private static string WildCardToRegular(string value)
    {
        return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
    }
}