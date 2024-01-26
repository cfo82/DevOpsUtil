namespace DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

using System.Xml.Serialization;

public class ReleaseDefinitionData
{
    [XmlElement(ElementName = "source")]
    public string Source { get; set; }

    [XmlElement(ElementName = "id")]
    public int Id { get; set; }

    [XmlElement(ElementName = "revision")]
    public int Revision { get; set; }

    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "description")]
    public string Description { get; set; }

    [XmlElement(ElementName = "createdBy")]
    public AuthorData CreatedBy { get; set; }

    [XmlElement(ElementName = "createdOn")]
    public object CreatedOn { get; set; }

    [XmlElement(ElementName = "modifiedBy")]
    public AuthorData ModifiedBy { get; set; }

    [XmlElement(ElementName = "modifiedOn")]
    public object ModifiedOn { get; set; }

    [XmlElement(ElementName = "path")]
    public string Path { get; set; }

    [XmlElement(ElementName = "variableGroups")]
    public object VariableGroups { get; set; }

    [XmlElement(ElementName = "releaseNameFormat")]
    public string ReleaseNameFormat { get; set; }

    [XmlElement(ElementName = "url")]
    public string Url { get; set; }

    [XmlElement(ElementName = "_links")]
    public object Links { get; set; }

    [XmlElement(ElementName = "properties")]
    public object Properties { get; set; }
}
