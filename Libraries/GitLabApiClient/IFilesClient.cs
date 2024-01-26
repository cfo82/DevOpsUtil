namespace GitLabApiClient;

using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Files.Responses;

public interface IFilesClient
{
    Task<File> GetAsync(ProjectId projectId, string filePath, string reference = "master");
}
