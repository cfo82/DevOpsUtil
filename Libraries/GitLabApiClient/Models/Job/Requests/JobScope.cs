namespace GitLabApiClient.Models.Job.Requests;

using System.Runtime.Serialization;

public enum JobScope
{
    All,
    [EnumMember(Value = "created")]
    Created,
    [EnumMember(Value = "pending")]
    Pending,
    [EnumMember(Value = "running")]
    Running,
    [EnumMember(Value = "failed")]
    Failed,
    [EnumMember(Value = "success")]
    Success,
    [EnumMember(Value = "canceled")]
    Canceled,
    [EnumMember(Value = "skipped")]
    Skipped,
    [EnumMember(Value = "manual")]
    Manual,
}
