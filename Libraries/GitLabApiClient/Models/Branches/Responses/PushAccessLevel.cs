namespace GitLabApiClient.Models.Branches.Responses;

using Newtonsoft.Json;

public sealed class PushAccessLevel
{
    [JsonProperty("access_level")]
    public ProtectedRefAccessLevels AccessLevel { get; set; }

    [JsonProperty("access_level_description")]
    public string AccessLevelDescription { get; set; }
}
