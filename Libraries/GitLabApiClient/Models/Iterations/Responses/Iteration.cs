namespace GitLabApiClient.Models.Iterations.Responses;

using System;
using Newtonsoft.Json;

public sealed class Iteration : ModifiableObject
{
    public string Title { get; set; }

    [JsonProperty("group_id")]
    public int GroupId { get; set; }

    public string Description { get; set; }

    public IterationState State { get; set; }

    [JsonProperty("due_date")]
    public DateTime DueDate { get; set; }

    [JsonProperty("start_date")]
    public DateTime StartDate { get; set; }

    [JsonProperty("web_url")]
    public Uri WebUrl { get; set; }
}
