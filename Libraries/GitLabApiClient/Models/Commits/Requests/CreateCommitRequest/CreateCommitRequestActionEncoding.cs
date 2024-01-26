namespace GitLabApiClient.Models.Commits.Requests.CreateCommitRequest;

using System.Runtime.Serialization;

public enum CreateCommitRequestActionEncoding
{
    [EnumMember(Value = "text")]
    Text,
    [EnumMember(Value = "base64")]
    Base64,
}
