namespace GitLabApiClient.Models.Groups.Requests;

using System.Runtime.Serialization;

public enum GroupsProjectsOrder
{
    [EnumMember(Value = "created_at")]
    CreatedAt,
    [EnumMember(Value = "id")]
    Id,
    [EnumMember(Value = "name")]
    Name,
    [EnumMember(Value = "path")]
    Path,
    [EnumMember(Value = "updated_at")]
    UpdatedAt,
    [EnumMember(Value = "last_activity_at")]
    LastiActivityAt,
}
