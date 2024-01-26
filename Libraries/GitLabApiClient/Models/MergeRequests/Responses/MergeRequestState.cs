namespace GitLabApiClient.Models.MergeRequests.Responses;

using System.Runtime.Serialization;

public enum MergeRequestState
{
    [EnumMember(Value = "opened")]
    Opened,
    [EnumMember(Value = "active")]
    Active,
    [EnumMember(Value = "merged")]
    Merged,
    [EnumMember(Value = "closed")]
    Closed,
    [EnumMember(Value = "reopened")]
    Reopened,
    [EnumMember(Value = "cannot_be_merged_recheck")]
    CannotBeMerged,
    [EnumMember(Value = "locked")]
    Locked,
}
