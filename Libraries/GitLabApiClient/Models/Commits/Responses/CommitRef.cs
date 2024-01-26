namespace GitLabApiClient.Models.Commits.Responses;

using Newtonsoft.Json;

public class CommitRef
{
    [JsonProperty("type")]
    public CommitRefType Type { get; set; } = CommitRefType.All;

    [JsonProperty("name")]
    public string Name { get; set; }
}
