namespace GitLabApiClient.Models.Runners.Requests;

using System.Runtime.Serialization;

public enum RunnerType
{
    All,
    [EnumMember(Value = "instance_type")]
    InstanceType,
    [EnumMember(Value = "group_type")]
    GroupType,
    [EnumMember(Value = "project_type")]
    ProjectType,
}
