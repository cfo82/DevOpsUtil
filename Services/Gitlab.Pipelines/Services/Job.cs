namespace DevOpsUtil.Gitlab.Pipelines.Services;

using DevOpsUtil.Gitlab.Pipelines.Contracts;
using DevOpsUtil.Gitlab.Pipelines.GraphQL;

public class Job : IJob
{
    private readonly IQueryPipelines_Group_Projects_Nodes_Pipelines_Nodes_Jobs_Nodes _jobDetail;

    public Job(IQueryPipelines_Group_Projects_Nodes_Pipelines_Nodes_Jobs_Nodes jobDetail)
    {
        _jobDetail = jobDetail;
    }

    public string Name => _jobDetail.Name ?? string.Empty;

    public bool IsRunning => _jobDetail.Status == CiJobStatus.Created ||
                             _jobDetail.Status == CiJobStatus.Pending ||
                             _jobDetail.Status == CiJobStatus.Scheduled ||
                             _jobDetail.Status == CiJobStatus.Preparing ||
                             _jobDetail.Status == CiJobStatus.Running;

    public bool WasSuccessful => _jobDetail.Status == CiJobStatus.Success;

    public bool HasFailed => _jobDetail.Status == CiJobStatus.Canceled ||
                             _jobDetail.Status == CiJobStatus.Failed ||
                             _jobDetail.Status == CiJobStatus.Skipped;
}