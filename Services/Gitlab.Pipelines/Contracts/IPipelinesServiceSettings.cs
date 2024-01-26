namespace DevOpsUtil.Gitlab.Pipelines.Contracts;

public interface IPipelinesServiceSettings
{
    string GitlabSettingsKey { get; }

    string[] ProjectsToIgnore { get; }

    string[] BranchesToWatch { get; }

    bool ShouldIgnoreProject(string projectName);
}