namespace GitLabApiClient.Models.ToDoList.Responses;

using GitLabApiClient.Models.Issues.Responses;
using Newtonsoft.Json;

public sealed class ToDoIssue : ToDo
{
    [JsonProperty("target")]
    public Issue Target { get; set; }
}
