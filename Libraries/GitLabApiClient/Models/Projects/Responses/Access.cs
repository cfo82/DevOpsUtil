namespace GitLabApiClient.Models.Projects.Responses;

using Newtonsoft.Json;

public sealed class Access
{
    [JsonProperty("access_level")]
    public int AccessLevel { get; set; }

    [JsonProperty("notification_level")]
    public int NotificationLevel { get; set; }
}
