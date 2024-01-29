namespace DevOpsUtil.AzureDevOps.Releases.Services;

using System.Text.RegularExpressions;
using DevOpsUtil.AzureDevOps.Releases.Contracts;

public class ReleasesServiceSettings : IReleasesServiceSettings
{
    public string AzureDevOpsSettingsKey { get; init; } = string.Empty;

    public string[] ReleaseDefinitionstoIgnore { get; init; } = Array.Empty<string>();

    public bool ShouldIgnoreReleaseDefinition(string projectName)
    {
        return ReleaseDefinitionstoIgnore
            .Any(pattern => Regex.IsMatch(projectName, WildCardToRegular(pattern)));
    }

    // https://stackoverflow.com/questions/30299671/matching-strings-with-wildcard
    // If you want to implement both "*" and "?"
    private static string WildCardToRegular(string value)
    {
        return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
    }
}