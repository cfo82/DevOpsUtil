namespace GitLabApiClient.Models.Discussions.Responses;

using System.Collections.Generic;
using GitLabApiClient.Models.Notes.Responses;
using Newtonsoft.Json;

public sealed class Discussion
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("individual_note")]
    public bool IndividualNote { get; set; }

    [JsonProperty("notes")]
    public IList<Note> Notes { get; set; } = new List<Note>();
}
