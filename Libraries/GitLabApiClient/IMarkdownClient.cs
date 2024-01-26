namespace GitLabApiClient;

using System.Threading.Tasks;
using GitLabApiClient.Models.Markdown.Request;
using GitLabApiClient.Models.Markdown.Response;

public interface IMarkdownClient
{
    Task<Markdown> RenderAsync(RenderMarkdownRequest request);
}
