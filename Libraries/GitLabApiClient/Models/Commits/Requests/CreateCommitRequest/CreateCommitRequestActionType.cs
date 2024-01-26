namespace GitLabApiClient.Models.Commits.Requests.CreateCommitRequest;

using System.Runtime.Serialization;

public enum CreateCommitRequestActionType
{
    [EnumMember(Value = "create")]
    Create,
    [EnumMember(Value = "delete")]
    Delete,
    [EnumMember(Value = "move")]
    Move,
    [EnumMember(Value = "update")]
    Update,
    [EnumMember(Value = "chmod")]
    Chmod,
}
