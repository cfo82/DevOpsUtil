namespace GitLabApiClient.Models.Markdown.Response;

using Newtonsoft.Json;

public sealed class Markdown
{
    [JsonProperty("html")]
    public string Html { get; set; }
}
