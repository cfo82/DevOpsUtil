namespace DevOpsUtil.Gitlab.Pipelines.Services;

using System.Collections.Immutable;
using DevOpsUtil.Gitlab.Pipelines.Contracts;
using DevOpsUtil.Gitlab.Pipelines.GraphQL;

public class Project : IProject
{
    private readonly IPipelinesServiceSettings _settings;
    private readonly IQueryPipelines_Group_Projects_Nodes _masterProject;
    private readonly IQueryPipelines_Group_Projects_Nodes _developProject;
    private readonly List<IPipeline> _pipelines;

    public Project(
        IPipelinesServiceSettings settings,
        IQueryPipelines_Group_Projects_Nodes masterProject,
        IQueryPipelines_Group_Projects_Nodes developProject)
    {
        _settings = settings;
        _masterProject = masterProject;
        _developProject = developProject;
        _pipelines = new List<IPipeline>();

        AddPipelines(masterProject);
        AddPipelines(developProject);
    }

    public bool IsIgnored => _settings.ShouldIgnoreProject(Name);

    public bool WasSuccessful => !IsIgnored && !IsRunning && _pipelines.All(p => p.WasSuccessful);

    public bool HasFailed => !IsIgnored && _pipelines.Any(p => p.HasFailed);

    public bool IsRunning => !IsIgnored && _pipelines.Any(p => p.IsRunning);

    public bool IsArchived => _masterProject.Archived ?? false;

    public string Name => _masterProject.Name;

    public ImmutableArray<IPipeline> Pipelines => _pipelines.ToImmutableArray();

    private void AddPipelines(IQueryPipelines_Group_Projects_Nodes project)
    {
        foreach (var pipeline in project.Pipelines?.Nodes ??
                                 new List<IQueryPipelines_Group_Projects_Nodes_Pipelines_Nodes>())
        {
            if (pipeline == null)
            {
                continue;
            }

            _pipelines.Add(new Pipeline(project.FullPath, pipeline));
        }
    }
}