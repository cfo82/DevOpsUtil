namespace GitLabApiClient.Models.MergeRequests.Responses;

using Newtonsoft.Json;

public class MergeRequestApprovalItem
{
    [JsonProperty("user")]
    public Assignee User { get; set; }
}