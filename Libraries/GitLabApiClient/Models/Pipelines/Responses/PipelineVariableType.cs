namespace GitLabApiClient.Models.Pipelines.Responses;

using System.Runtime.Serialization;

public enum PipelineVariableType
{
    Unknown,
    [EnumMember(Value = "env_var")]
    Environment,
    [EnumMember(Value = "file")]
    File,
}
