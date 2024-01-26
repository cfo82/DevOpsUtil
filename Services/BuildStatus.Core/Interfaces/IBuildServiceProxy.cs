namespace DevOpsUtil.BuildStatus.Core.Interfaces;

using System;
using System.Threading.Tasks;

public interface IBuildServiceProxy
{
    event EventHandler DefinitionsChanged;

    event EventHandler AllDefinitionsUpdated;

    Task<IDefinition[]> GetDefinitions();

    /// <summary>
    /// Refresh everyhing including the definitions. This can be triggered from the UI.
    /// </summary>
    Task ManualRefresh(bool forceDefinitionUpdate);
}
