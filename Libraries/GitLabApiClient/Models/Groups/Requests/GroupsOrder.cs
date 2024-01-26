namespace GitLabApiClient.Models.Groups.Requests;

using System.Runtime.Serialization;

public enum GroupsOrder
{
    [EnumMember(Value = "name")]
    Name,
    [EnumMember(Value = "path")]
    Path,
}
