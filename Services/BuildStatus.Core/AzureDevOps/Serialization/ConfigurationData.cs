namespace DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

using System;

public class ConfigurationData
{
    public string UserName { get; set; } = string.Empty;

    public string BuildAccessToken { get; set; } = string.Empty;

    public string BaseAddress { get; set; } = string.Empty;

    public int PollingInterval { get; set; }

    public string[] BuildsToIgnore { get; set; } = Array.Empty<string>();

    public string[] ReleasesToIgnore { get; set; } = Array.Empty<string>();

    public int BlinkingInterval { get; set; }

    public int DefinitionListingInterval { get; set; }
}
