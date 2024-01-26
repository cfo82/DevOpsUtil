namespace GitLabApiClient.Models.Commits.Responses;

using Newtonsoft.Json;

public class CommitStats
{
    [JsonProperty("additions")]
    public int Additions { get; set; }

    [JsonProperty("deletions")]
    public int Deletions { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }
}
