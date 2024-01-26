namespace GitLabApiClient.Models.Issues.Responses;

using Newtonsoft.Json;

public sealed class ClosedBy : ModifiableObject
{
    [JsonProperty("active")]
    public string State { get; set; }

    [JsonProperty("web_url")]
    public string WebUrl { get; set; }

    [JsonProperty("avatar_url")]
    public string AvatarUrl { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}
