namespace GitLabApiClient.Models.Oauth.Requests;

using Newtonsoft.Json;

public class AccessTokenRequest
{
    [JsonProperty("grant_type")]
    public string GrantType { get; set; }

    [JsonProperty("scope")]
    public string Scope { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }
}
