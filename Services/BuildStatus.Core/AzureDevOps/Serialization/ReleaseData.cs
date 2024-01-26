namespace DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

using System;
using System.Xml.Serialization;

public class ReleaseData
{
    [XmlElement(ElementName = "id")]
    public int Id { get; set; }

    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "status")]
    public string Status { get; set; }

    [XmlElement(ElementName = "createdOn")]
    public DateTime CreatedOn { get; set; }

    [XmlElement(ElementName = "modifiedOn")]
    public object ModifiedOn { get; set; }

    [XmlElement(ElementName = "modifiedBy")]
    public AuthorData ModifiedBy { get; set; }

    [XmlElement(ElementName = "createdBy")]
    public AuthorData CreatedBy { get; set; }

    [XmlElement(ElementName = "variables")]
    public object Variables { get; set; }

    [XmlElement(ElementName = "variableGroups")]
    public object VariableGruops { get; set; }

    [XmlElement(ElementName = "releaseDefinition")]
    public ReleaseDefinitionData ReleaseDefinition { get; set; }

    [XmlElement(ElementName = "description")]
    public string Description { get; set; }

    [XmlElement(ElementName = "reason")]
    public string Reason { get; set; }

    [XmlElement(ElementName = "releaseNameFormat")]
    public string ReleaseNameFormat { get; set; }

    [XmlElement(ElementName = "keepForever")]
    public bool KeepForever { get; set; }

    [XmlElement(ElementName = "definitionSnapshotRevision")]
    public int DefinitionSnapshotRevision { get; set; }

    [XmlElement(ElementName = "logsContainerUrl")]
    public string LogsContainerUrl { get; set; }

    [XmlElement(ElementName = "url")]
    public string Url { get; set; }

    [XmlElement(ElementName = "_links")]
    public object Links { get; set; }

    [XmlElement(ElementName = "tags")]
    public object Tags { get; set; }

    [XmlElement(ElementName = "projectReference")]
    public object ProjectReference { get; set; }

    [XmlElement(ElementName = "properties")]
    public object Properties { get; set; }

    [XmlElement(ElementName = "environments")]
    public EnvironmentData[] Environments { get; set; }
}
