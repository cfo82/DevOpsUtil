namespace DevOpsUtil.Gitlab.Core.Contracts;

using Microsoft.Extensions.Configuration;

public class GitlabSettings
{
    public const string Location = "Gitlab";

    public string Key { get; set; } = string.Empty;

    public string BaseAddress { get; set; } = string.Empty;

    public string GraphQLBaseAddress { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public int GroupId { get; set; }

    public string GroupPath { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public static GitlabSettings[] Load(IConfiguration configuration)
    {
        return configuration.GetSection(Location).Get<GitlabSettings[]>() ??
                       throw new InvalidOperationException("Unable to read gitlab settings.");
    }
}