namespace DevOpsUtil.AzureDevOps.Core.Contracts;

public interface IAzureDevOpsSettingsService
{
    AzureDevOpsSettings Get(string key);
}