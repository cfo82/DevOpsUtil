namespace DevOpsUtil.Github.Core.Contracts;

public interface IGithubSettingsService
{
    GithubSettings Get(string key);
}