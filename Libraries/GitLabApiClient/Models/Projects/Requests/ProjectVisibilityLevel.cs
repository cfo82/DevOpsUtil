namespace GitLabApiClient.Models.Projects.Requests;

using System.Runtime.Serialization;

public enum ProjectVisibilityLevel
{
    [EnumMember(Value = "private")]
    Private,
    [EnumMember(Value = "internal")]
    Internal,
    [EnumMember(Value = "public")]
    Public,
}
