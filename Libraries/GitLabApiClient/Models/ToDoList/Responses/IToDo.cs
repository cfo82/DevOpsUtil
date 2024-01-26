namespace GitLabApiClient.Models.ToDoList.Responses;

using GitLabApiClient.Models.Projects.Responses;
using Newtonsoft.Json;

[JsonConverter(typeof(ToDoItemConverter))]
public interface IToDo
{
    int? Id { get; set; }

    Project Project { get; set; }

    Assignee Author { get; set; }

    ToDoActionType? ActionType { get; set; }

    ToDoTargetType? TargetType { get; set; }

    string TargetUrl { get; set; }

    string Body { get; set; }

    ToDoState? State { get; set; }

    string CreatedAt { get; set; }
}
