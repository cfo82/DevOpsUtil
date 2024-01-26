namespace DevOpsUtil.Core.Services;

using System;
using Microsoft.Extensions.Configuration;

public class RefreshSettings
{
    public const string Location = "Refresh";

    public long PollingInterval { get; set; }

    public long ForceUpdateInterval { get; set; }

    public static RefreshSettings Load(IConfiguration configuration)
    {
        return configuration.GetSection(Location).Get<RefreshSettings>() ??
               throw new InvalidOperationException("Unable to read refresh settings.");
    }
}