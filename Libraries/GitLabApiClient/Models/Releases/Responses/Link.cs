namespace GitLabApiClient.Models.Releases.Responses;

using Newtonsoft.Json;

public sealed class Link
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("external")]
    public bool External { get; set; }
}
