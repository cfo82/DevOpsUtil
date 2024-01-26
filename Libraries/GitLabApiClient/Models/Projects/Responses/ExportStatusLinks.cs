namespace GitLabApiClient.Models.Projects.Responses;

using Newtonsoft.Json;

public sealed class ExportStatusLinks
{
    [JsonProperty("api_url")]
    public string ApiUrl { get; set; }

    [JsonProperty("web_url")]
    public string WebUrl { get; set; }
}
