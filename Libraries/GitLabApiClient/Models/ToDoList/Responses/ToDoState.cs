namespace GitLabApiClient.Models.ToDoList.Responses;

using System.Runtime.Serialization;

public enum ToDoState
{
    [EnumMember(Value = "pending")]
    Pending,

    [EnumMember(Value = "done")]
    Done,
}
