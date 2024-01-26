namespace GitLabApiClient.Models.Commits.Responses;

using System.Runtime.Serialization;

public enum CommitRefType
{
    [EnumMember(Value = "all")]
    All,
    [EnumMember(Value = "branch")]
    Branch,
    [EnumMember(Value = "tag")]
    Tag,
}
