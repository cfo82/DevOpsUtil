namespace DevOpsUtil.AzureDevOps.Pipelines.Contracts;

public interface IPipelinesServiceSettings
{
    string AzureDevOpsSettingsKey { get; }

    string[] BuildDefinitionstoIgnore { get; }

    string[] BranchesToWatch { get; }

    bool ShouldIgnoreBuildDefinition(string projectName);
}