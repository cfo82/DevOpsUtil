namespace GitLabApiClient.Models.ToDoList.Responses;

using GitLabApiClient.Models.MergeRequests.Responses;
using Newtonsoft.Json;

public sealed class ToDoMergeRequest : ToDo
{
    [JsonProperty("target")]
    public MergeRequest Target { get; set; }
}
