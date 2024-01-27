namespace DevOpsUtil.AzureDevOps.Pipelines.Services;

using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using DevOpsUtil.AzureDevOps.Pipelines.Contracts;
using Microsoft.TeamFoundation.Build.WebApi;

public class BuildDefinition : IBuildDefinition
{
    private readonly IPipelinesServiceSettings _settings;
    private readonly BuildDefinitionReference _buildDefinition;
    private readonly List<IBuild> _builds;

    public BuildDefinition(IPipelinesServiceSettings settings, BuildDefinitionReference buildDefinition)
    {
        _settings = settings;
        _buildDefinition = buildDefinition;
        _builds = new List<IBuild>();
    }

    public event EventHandler? Changed;

    public int Id => _buildDefinition.Id;

    public bool IsIgnored => IsDraft || IsDisabled || _settings.ShouldIgnoreBuildDefinition(Name);

    public bool IsDraft => _buildDefinition.DefinitionQuality == DefinitionQuality.Draft;

    public bool IsDisabled => _buildDefinition.QueueStatus == DefinitionQueueStatus.Disabled ||
        _buildDefinition.QueueStatus == DefinitionQueueStatus.Paused;

    public bool WasSuccessful => !IsIgnored && !IsRunning && _builds.All(p => p.WasSuccessful);

    public bool HasFailed => !IsIgnored && _builds.Any(p => p.HasFailed);

    public bool IsRunning => !IsIgnored && _builds.Any(p => p.IsRunning);

    public string Name => _buildDefinition.Name;

    public Uri Uri => _buildDefinition.Uri;

    public ImmutableArray<IBuild> Builds => _builds.ToImmutableArray();

    public async Task UpdateAsync(BuildHttpClient buildHttpClient)
    {
        if (IsIgnored)
        {
            return;
        }

        _builds.Clear();

        foreach (var branchToWatch in _settings.BranchesToWatch)
        {
            var build = await buildHttpClient.GetLatestBuildAsync(
                definition: _buildDefinition.Id.ToString(),
                project: _buildDefinition.Project.Id.ToString(),
                branchName: branchToWatch);

            _builds.Add(new Build(build));
        }

        Changed?.Invoke(this, EventArgs.Empty);
    }
}
