namespace GitLabApiClient.Models.MergeRequests.Responses;

using System.Collections.Generic;
using Newtonsoft.Json;

public class MergeRequestApproval : ModifiableObject
{
    [JsonProperty("project_id")]
    public string ProjectId { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("state")]
    public MergeRequestState State { get; set; }

    [JsonProperty("merge_status")]
    public MergeStatus Status { get; set; }

    [JsonProperty("approved")]
    public bool Approved { get; set; }

    [JsonProperty("approvals_required")]
    public int ApprovalsRequired { get; set; }

    [JsonProperty("approvals_left")]
    public int ApprovalsLeft { get; set; }

    [JsonProperty("require_password_to_approve")]
    public bool RequirePasswordToApprove { get; set; }

    [JsonProperty("approved_by")]
    public List<MergeRequestApprovalItem> ApprovedBy { get; set; }

    [JsonProperty("suggested_approvers")]
    public List<Assignee> SuggestedApprovers { get; set; }

    /* ... some more members
    // approvers
    // approver_groups*/

    [JsonProperty("user_has_approved")]
    public bool UserHasApproved { get; set; }

    [JsonProperty("user_can_approve")]
    public bool UserCanApprove { get; set; }

    /* approval_rules_left */

    [JsonProperty("has_approval_rules")]
    public bool HasApprovalRules { get; set; }

    [JsonProperty("merge_request_approvers_available")]
    public bool MergeRequestApproversAvailable { get; set; }

    [JsonProperty("multiple_approval_rules_available")]
    public bool MultipleApprovalRulesAvailable { get; set; }

    // invalid_approvers_rules
}