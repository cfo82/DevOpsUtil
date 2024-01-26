namespace DevOpsUtil.Gitlab.Pipelines.Services;

using System.Collections.Immutable;
using DevOpsUtil.Gitlab.Pipelines.Contracts;
using DevOpsUtil.Gitlab.Pipelines.GraphQL;

public class Pipeline : IPipeline
{
    private readonly string _fullProjectPath;
    private readonly IQueryPipelines_Group_Projects_Nodes_Pipelines_Nodes _pipelineDetail;
    private readonly List<IJob> _jobs;

    public Pipeline(
        string fullProjectPath,
        IQueryPipelines_Group_Projects_Nodes_Pipelines_Nodes pipelineDetail)
    {
        _fullProjectPath = fullProjectPath;
        _pipelineDetail = pipelineDetail;
        _jobs = new List<IJob>();

        foreach (var job in _pipelineDetail.Jobs?.Nodes ??
                            new List<IQueryPipelines_Group_Projects_Nodes_Pipelines_Nodes_Jobs_Nodes>())
        {
            if (job == null)
            {
                continue;
            }

            _jobs.Add(new Job(job));
        }
    }

    public bool IsRunning => _pipelineDetail.Status == PipelineStatusEnum.Created ||
                             _pipelineDetail.Status == PipelineStatusEnum.Pending ||
                             _pipelineDetail.Status == PipelineStatusEnum.Scheduled ||
                             _pipelineDetail.Status == PipelineStatusEnum.Preparing ||
                             _pipelineDetail.Status == PipelineStatusEnum.Running;

    public bool WasSuccessful => _pipelineDetail.Status == PipelineStatusEnum.Success;

    public bool HasFailed => _pipelineDetail.Status == PipelineStatusEnum.Canceled ||
                             _pipelineDetail.Status == PipelineStatusEnum.Failed ||
                             _pipelineDetail.Status == PipelineStatusEnum.Skipped;

    public string Ref => _pipelineDetail.Ref ?? string.Empty;

    public string StatusText => Enum.GetName(_pipelineDetail.Status) ?? string.Empty;

    public Uri Uri => new Uri($"https://gitlab.com/{_fullProjectPath}/-/pipelines/{_pipelineDetail.Id.Split('/').Last()}");

    public int Age => (DateTime.Now - DateTime.Parse(_pipelineDetail.CreatedAt)).Days;

    public ImmutableArray<IJob> Jobs => _jobs.ToImmutableArray();
}