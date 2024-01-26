namespace GitLabApiClient.Models.Commits.Requests;

using System;

public sealed class CommitQueryOptions
{
    internal CommitQueryOptions()
    {
    }

    public string RefName { get; set; }

    public DateTime? Since { get; set; }

    public DateTime? Until { get; set; }

    public string Path { get; set; }

    public bool? All { get; set; }

    public bool? WithStats { get; set; }

    public bool? FirstParent { get; set; }
}
