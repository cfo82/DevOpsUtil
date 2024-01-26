namespace GitLabApiClient.Models.Issues.Responses;

using Newtonsoft.Json;

public class IssueTaskCompletionStatus
{
    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("completed_count")]
    public int Completed { get; set; }
}
