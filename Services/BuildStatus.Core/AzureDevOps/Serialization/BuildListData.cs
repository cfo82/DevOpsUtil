namespace DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

using System.Xml.Serialization;

public class BuildListData
{
    [XmlElement(ElementName = "count")]
    public int Count { get; set; }

    [XmlElement(ElementName = "value")]
    public BuildData[] Value { get; set; }
}
