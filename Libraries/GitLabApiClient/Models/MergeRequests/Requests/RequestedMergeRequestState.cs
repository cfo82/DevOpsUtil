namespace GitLabApiClient.Models.MergeRequests.Requests;

using System.Runtime.Serialization;

public enum RequestedMergeRequestState
{
    [EnumMember(Value = "close")]
    Close,
    [EnumMember(Value = "reopen")]
    Reopen,
}
