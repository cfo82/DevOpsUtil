namespace GitLabApiClient.Models.Pipelines;

using System.Runtime.Serialization;

public enum PipelineStatus
{
    All,
    [EnumMember(Value = "running")]
    Running,
    [EnumMember(Value = "pending")]
    Pending,
    [EnumMember(Value = "success")]
    Success,
    [EnumMember(Value = "failed")]
    Failed,
    [EnumMember(Value = "canceled")]
    Canceled,
    [EnumMember(Value = "skipped")]
    Skipped,
    [EnumMember(Value = "preparing")]
    Preparing,
    [EnumMember(Value = "scheduled")]
    Scheduled,
    [EnumMember(Value = "created")]
    Created,
    [EnumMember(Value = "manual")]
    Manual,
}
