namespace DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

using System.Xml.Serialization;

public class BuildDefinitionListData
{
    [XmlElement(ElementName = "count")]
    public int Count { get; set; }

    [XmlElement(ElementName = "value")]
    public BuildDefinitionData[] Value { get; set; }
}
