namespace GitLabApiClient.Models.Milestones.Requests;

using System.Runtime.Serialization;

public enum UpdatedMilestoneState
{
    [EnumMember(Value = "close")]
    Close,

    [EnumMember(Value = "activate")]
    Activate,
}
