namespace GitLabApiClient.Models.Groups.Requests;

using System.Runtime.Serialization;

public enum GroupsSort
{
    [EnumMember(Value = "asc")]
    Ascending,

    [EnumMember(Value = "desc")]
    Descending,
}
