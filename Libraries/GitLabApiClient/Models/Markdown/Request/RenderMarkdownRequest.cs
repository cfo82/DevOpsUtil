namespace GitLabApiClient.Models.Markdown.Request;

using GitLabApiClient.Internal.Utilities;
using Newtonsoft.Json;

/// <summary>
/// Used to render a markdown document
/// </summary>
public sealed class RenderMarkdownRequest
{
    public RenderMarkdownRequest(string text)
    {
        Guard.NotEmpty(text, nameof(text));
        Text = text;
    }

    /// <summary>
    /// The markdown text to render
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }

    /// <summary>
    /// Render text using GitLab Flavored Markdown
    /// </summary>
    [JsonProperty("gfm")]
    public bool? FlavoredMarkdown { get; set; } = false;

    /// <summary>
    /// Use as a context when creating references using GitLab Flavored Markdown. Authentication is required if a project is not public.
    /// </summary>
    [JsonProperty("project")]
    public string Project { get; set; }
}
