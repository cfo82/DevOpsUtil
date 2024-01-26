namespace GitLabApiClient.Models.Trees.Requests;

public sealed class TreeQueryOptions
{
    internal TreeQueryOptions(string reference = null, string path = null, bool recursive = false)
    {
        Reference = reference;
        Path = path;
        Recursive = recursive;
    }

    public string Reference { get; set; }

    public string Path { get; set; }

    public bool Recursive { get; set; }
}
