namespace DevOpsUtil.Gitlab.Core.Contracts;

public interface IGitlabSettingsService
{
    GitlabSettings Get(string key);
}