namespace GitLabApiClient.Models.ToDoList.Responses;

using System.Runtime.Serialization;

public enum ToDoTargetType
{
    [EnumMember(Value = "Issue")]
    Issue,

    [EnumMember(Value = "MergeRequest")]
    MergeRequest,
}
