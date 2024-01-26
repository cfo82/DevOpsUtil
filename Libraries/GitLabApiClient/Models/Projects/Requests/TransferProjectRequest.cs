namespace GitLabApiClient.Models.Projects.Requests;

using Newtonsoft.Json;

public class TransferProjectRequest
{
    /// <summary>
    /// The ID or path of the namespace to transfer to project to
    /// </summary>
    [JsonProperty("namespace")]
    public string NameSpace { get; set; }
}
