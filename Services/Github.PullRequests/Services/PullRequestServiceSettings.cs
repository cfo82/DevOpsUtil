namespace DevOpsUtil.Github.PullRequests.Services;

using DevOpsUtil.Github.PullRequests.Contracts;

public class PullRequestServiceSettings : IPullRequestServiceSettings
{
    public string GithubSettingsKey { get; set; } = string.Empty;
}