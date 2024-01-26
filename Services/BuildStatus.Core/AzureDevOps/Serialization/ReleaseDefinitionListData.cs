namespace DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

using System.Xml.Serialization;

public class ReleaseDefinitionListData
{
    [XmlElement(ElementName = "count")]
    public int Count { get; set; }

    [XmlElement(ElementName = "value")]
    public ReleaseDefinitionData[] Value { get; set; }
}
