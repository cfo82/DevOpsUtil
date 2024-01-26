namespace DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

using System.Xml.Serialization;

public class ReleaseListData
{
    [XmlElement(ElementName = "count")]
    public int Count { get; set; }

    [XmlElement(ElementName = "value")]
    public ReleaseData[] Value { get; set; }
}
