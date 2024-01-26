namespace GitLabApiClient.Models.Issues.Requests;

using System.Runtime.Serialization;

public enum UpdatedIssueState
{
    [EnumMember(Value = "close")]
    Close,

    [EnumMember(Value = "reopen")]
    Reopen,
}
