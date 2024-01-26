namespace GitLabApiClient.Models.Pipelines.Requests;

using System.Runtime.Serialization;

public enum PipelineScope
{
    All,
    [EnumMember(Value = "running")]
    Running,
    [EnumMember(Value = "pending")]
    Pending,
    [EnumMember(Value = "finished")]
    Finished,
    [EnumMember(Value = "branches")]
    Branches,
    [EnumMember(Value = "tags")]
    Tags,
}
