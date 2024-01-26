namespace DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;

using System.Xml.Serialization;

public class EnvironmentData
{
    [XmlElement(ElementName = "id")]
    public int Id { get; set; }

    [XmlElement(ElementName = "releaseId")]
    public int ReleaseId { get; set; }

    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "status")]
    public string Status { get; set; }

    [XmlElement(ElementName = "variables")]
    public object Variables { get; set; }

    [XmlElement(ElementName = "preDeployApprovals")]
    public object[] PreDeployApprovals { get; set; }

    [XmlElement(ElementName = "postDeployApprovals")]
    public object[] PostDeployApprovals { get; set; }

    [XmlElement(ElementName = "preApprovalsSnapshot")]
    public object PreApprovalsSnapshot { get; set; }

    [XmlElement(ElementName = "postApprovalsSnapshot")]
    public object PostApprovalsSnapshot { get; set; }

    [XmlElement(ElementName = "deploySteps")]
    public object[] DeploySteps { get; set; }

    [XmlElement(ElementName = "rank")]
    public int Rank { get; set; }

    [XmlElement(ElementName = "definitionEnvironmentId")]
    public int DefinitionEnvironmentId { get; set; }

    [XmlElement(ElementName = "environmentOptions")]
    public object EnvironmentOptions { get; set; }

    [XmlElement(ElementName = "conditions")]
    public object[] Conditions { get; set; }

    [XmlElement(ElementName = "createdOn")]
    public object CreatedOn { get; set; }

    [XmlElement(ElementName = "modifiedOn")]
    public object ModifiedOn { get; set; }

    [XmlElement(ElementName = "workflowTasks")]
    public object[] WorkflowTasks { get; set; }

    [XmlElement(ElementName = "deployPhaseSnapshot")]
    public object DeployPhaseSnapshot { get; set; }

    [XmlElement(ElementName = "owner")]
    public AuthorData Owner { get; set; }

    [XmlElement(ElementName = "schedules")]
    public object[] Schedules { get; set; }

    [XmlElement(ElementName = "release")]
    public ReleaseData Release { get; set; }

    [XmlElement(ElementName = "releaseDefinition")]
    public ReleaseDefinitionData ReleaseDefnition { get; set; }

    [XmlElement(ElementName = "releaseCreatedBy")]
    public AuthorData ReleaseCreatedBy { get; set; }

    [XmlElement(ElementName = "triggerReason")]
    public string TriggerReason { get; set; }

    [XmlElement(ElementName = "timeToDeploy")]
    public double TimeToDeploy { get; set; }

    [XmlElement(ElementName = "processParameters")]
    public object ProcessParameters { get; set; }
}
