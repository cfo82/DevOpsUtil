namespace DevOpsUtil.Gitlab.MergeRequests.Services;

using DevOpsUtil.Gitlab.MergeRequests.Contracts;

public class MergeRequestServiceSettings : IMergeRequestServiceSettings
{
    public string GitlabSettingsKey { get; set; } = string.Empty;
}