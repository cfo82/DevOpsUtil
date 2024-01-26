namespace GitLabApiClient.Models.Runners.Responses;

using Newtonsoft.Json;

public sealed class RunnerToken
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("token")]
    public string Token { get; set; }
}
