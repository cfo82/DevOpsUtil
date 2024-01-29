namespace DevOpsUtil.AzureDevOps.Releases.Contracts;

public interface IReleasesServiceSettings
{
    string AzureDevOpsSettingsKey { get; }

    string[] ReleaseDefinitionstoIgnore { get; }

    bool ShouldIgnoreReleaseDefinition(string projectName);
}