namespace GitLabApiClient.Models.Projects.Responses;

using System;
using Newtonsoft.Json;

public sealed class Owner
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}
