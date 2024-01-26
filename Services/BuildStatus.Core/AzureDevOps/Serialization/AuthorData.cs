namespace DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

using System.Xml.Serialization;

public class AuthorData
{
    [XmlElement(ElementName = "id")]
    public string Id { get; set; }

    [XmlElement(ElementName = "displayName")]
    public string DisplayName { get; set; }

    [XmlElement(ElementName = "uniqueName")]
    public string UniqueName { get; set; }

    [XmlElement(ElementName = "url")]
    public string Url { get; set; }

    [XmlElement(ElementName = "imageUrl")]
    public string ImageUrl { get; set; }
}
