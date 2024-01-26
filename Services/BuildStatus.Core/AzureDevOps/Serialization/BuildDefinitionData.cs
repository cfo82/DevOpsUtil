namespace DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

using System.Xml.Serialization;

public class BuildDefinitionData
{
    [XmlElement(ElementName = "_links")]
    public object Links { get; set; }

    [XmlElement(ElementName = "quality")]
    public string Quality { get; set; }

    [XmlElement(ElementName = "authoredBy")]
    public AuthorData AuthoredBy { get; set; }

    [XmlElement(ElementName = "queue")]
    public object Queue { get; set; }

    [XmlElement(ElementName = "uri")]
    public string Uri { get; set; }

    [XmlElement(ElementName = "path")]
    public string Path { get; set; }

    [XmlElement(ElementName = "type")]
    public string Type { get; set; }

    [XmlElement(ElementName = "revision")]
    public int Revision { get; set; }

    [XmlElement(ElementName = "createdDate")]
    public string CreatedDate { get; set; }

    [XmlElement(ElementName = "id")]
    public int Id { get; set; }

    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "url")]
    public string Url { get; set; }

    [XmlElement(ElementName = "project")]
    public object Project { get; set; }

    [XmlElement(ElementName = "queueStatus")]
    public string QueueStatus { get; set; }
}
