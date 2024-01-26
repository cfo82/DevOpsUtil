namespace GitLabApiClient.Models.Runners.Requests;

using System.Runtime.Serialization;

public enum RunnerStatus
{
    All,
    [EnumMember(Value = "active")]
    Active,
    [EnumMember(Value = "paused")]
    Paused,
    [EnumMember(Value = "online")]
    Online,
    [EnumMember(Value = "offline")]
    Offline,
}
