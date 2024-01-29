namespace DevOpsUtil.AzureDevOps.Releases.Contracts;

using System.Collections.Immutable;

public interface IReleasesService
{
    event EventHandler? ReleaseDefinitionsChanged;

    ImmutableArray<IReleaseDefinition> ReleaseDefinitions { get; }
}
