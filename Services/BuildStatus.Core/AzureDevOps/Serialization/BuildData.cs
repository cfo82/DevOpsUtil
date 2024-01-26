namespace DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

using System;
using System.Xml.Serialization;

public class BuildData
{
    [XmlElement(ElementName = "_links")]
    public object Links { get; set; }

    [XmlElement(ElementName = "plans")]
    public object Plans { get; set; }

    [XmlElement(ElementName = "id")]
    public int Id { get; set; }

    [XmlElement(ElementName = "buildNumber")]
    public string BuildNumber { get; set; }

    [XmlElement(ElementName = "status")]
    public string Status { get; set; }

    [XmlElement(ElementName = "queueTime")]
    public string QueueTime { get; set; }

    [XmlElement(ElementName = "startTime")]
    public DateTime StartTime { get; set; }

    [XmlElement(ElementName = "url")]
    public string Url { get; set; }

    [XmlElement(ElementName = "definition")]
    public BuildDefinitionData DefinitionData { get; set; }

    [XmlElement(ElementName = "project")]
    public object Project { get; set; }

    [XmlElement(ElementName = "uri")]
    public string Uri { get; set; }

    [XmlElement(ElementName = "sourceBranch")]
    public string SourceBranch { get; set; }

    [XmlElement(ElementName = "sourceVersion")]
    public string SourceVersion { get; set; }

    [XmlElement(ElementName = "queue")]
    public object Queue { get; set; }

    [XmlElement(ElementName = "priority")]
    public string Priority { get; set; }

    [XmlElement(ElementName = "reason")]
    public string Reason { get; set; }

    [XmlElement(ElementName = "requestedFor")]
    public AuthorData RequestedFor { get; set; }

    [XmlElement(ElementName = "requestedBy")]
    public AuthorData RequestedBy { get; set; }

    [XmlElement(ElementName = "lastChangedDate")]
    public string LastChangedDate { get; set; }

    [XmlElement(ElementName = "lastChangedBy")]
    public AuthorData LastChangedBy { get; set; }

    [XmlElement(ElementName = "orchestrationPlan")]
    public object OrchestrationPlan { get; set; }

    [XmlElement(ElementName = "logs")]
    public object Logs { get; set; }

    [XmlElement(ElementName = "repository")]
    public object Repository { get; set; }

    [XmlElement(ElementName = "keepForever")]
    public bool KeepForever { get; set; }

    [XmlElement(ElementName = "retainedByRelease")]
    public bool RetainedByRelease { get; set; }

    [XmlElement(ElementName = "result")]
    public string Result { get; set; }
}
