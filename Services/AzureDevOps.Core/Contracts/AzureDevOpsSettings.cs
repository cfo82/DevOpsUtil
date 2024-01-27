namespace DevOpsUtil.AzureDevOps.Core.Contracts;

using Microsoft.Extensions.Configuration;

public class AzureDevOpsSettings
{
    public const string Location = "AzureDevOps";

    public string Key { get; set; } = string.Empty;

    public string BaseAddress { get; set; } = string.Empty;

    public string Organization { get; set; } = string.Empty;

    public string Project { get; set; } = string.Empty;

    public string PersonalAccessToken { get; set; } = string.Empty;

    public static AzureDevOpsSettings[] Load(IConfiguration configuration)
    {
        return configuration.GetSection(Location).Get<AzureDevOpsSettings[]>() ??
                       throw new InvalidOperationException("Unable to read gitlab settings.");
    }
}