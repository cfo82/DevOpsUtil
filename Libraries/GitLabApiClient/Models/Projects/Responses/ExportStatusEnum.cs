namespace GitLabApiClient.Models.Projects.Responses;

using System.Runtime.Serialization;

public enum ExportStatusEnum
{
    [EnumMember(Value = "none")]
    None,
    [EnumMember(Value = "started")]
    Started,
    [EnumMember(Value = "after_export_action")]
    AfterExportAction,
    [EnumMember(Value = "finished")]
    Finished,
}
