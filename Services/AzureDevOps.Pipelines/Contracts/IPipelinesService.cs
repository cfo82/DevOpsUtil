namespace DevOpsUtil.AzureDevOps.Pipelines.Contracts;

using System.Collections.Immutable;

public interface IPipelinesService
{
    event EventHandler? BuildDefinitionsChanged;

    ImmutableArray<IBuildDefinition> BuildDefinitions { get; }
}
