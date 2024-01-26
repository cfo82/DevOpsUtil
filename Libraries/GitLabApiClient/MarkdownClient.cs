namespace GitLabApiClient;

using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Models.Markdown.Request;
using GitLabApiClient.Models.Markdown.Response;

/// <summary>
/// Used to render a markdown document.
/// </summary>
public sealed class MarkdownClient : IMarkdownClient
{
    private readonly GitLabHttpFacade _httpFacade;

    internal MarkdownClient(GitLabHttpFacade httpFacade) =>
        _httpFacade = httpFacade;

    public async Task<Markdown> RenderAsync(RenderMarkdownRequest request) =>
        await _httpFacade.Post<Markdown>($"markdown", request);
}
