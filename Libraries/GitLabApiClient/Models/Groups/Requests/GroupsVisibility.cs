namespace GitLabApiClient.Models.Groups.Requests;

using System.Runtime.Serialization;

public enum GroupsVisibility
{
    [EnumMember(Value = "public")]
    Public,

    [EnumMember(Value = "internal")]
    Internal,

    [EnumMember(Value = "private")]
    Private,
}
