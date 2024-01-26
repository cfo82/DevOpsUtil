namespace GitLabApiClient.Models.Pipelines.Responses;

using Newtonsoft.Json;

public class PipelineVariable
{
    [JsonProperty("variable_type")]
    public PipelineVariableType VariableType { get; set; } = PipelineVariableType.Unknown;

    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}
