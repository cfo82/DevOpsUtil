namespace DevOpsUtil.Github.Core.Contracts;

using Microsoft.Extensions.Configuration;

public class GithubSettings
{
    public const string Location = "Github";

    public string Key { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public static GithubSettings[] Load(IConfiguration configuration)
    {
        return configuration.GetSection(Location).Get<GithubSettings[]>() ??
                       throw new InvalidOperationException("Unable to read github settings.");
    }
}