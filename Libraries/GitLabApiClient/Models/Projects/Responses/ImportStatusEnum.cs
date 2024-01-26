namespace GitLabApiClient.Models.Projects.Responses;

using System.Runtime.Serialization;

public enum ImportStatusEnum
{
    [EnumMember(Value = "none")]
    None,
    [EnumMember(Value = "scheduled")]
    Scheduled,
    [EnumMember(Value = "failed")]
    Failed,
    [EnumMember(Value = "started")]
    Started,
    [EnumMember(Value = "finished")]
    Finished,
}
