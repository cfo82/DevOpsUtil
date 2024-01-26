namespace GitLabApiClient.Models.ToDoList.Responses;

using GitLabApiClient.Models.Projects.Responses;
using Newtonsoft.Json;

public abstract class ToDo : IToDo
{
    [JsonProperty("id")]
    public int? Id { get; set; }

    [JsonProperty("project")]
    public Project Project { get; set; }

    [JsonProperty("author")]
    public Assignee Author { get; set; }

    [JsonProperty("action_name")]
    public ToDoActionType? ActionType { get; set; }

    [JsonProperty("target_type")]
    public ToDoTargetType? TargetType { get; set; }

    [JsonProperty("target_url")]
    public string TargetUrl { get; set; }

    [JsonProperty("body")]
    public string Body { get; set; }

    [JsonProperty("state")]
    public ToDoState? State { get; set; }

    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }
}
