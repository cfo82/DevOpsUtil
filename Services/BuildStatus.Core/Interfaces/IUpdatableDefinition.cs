namespace DevOpsUtil.BuildStatus.Core.Interfaces;

using System.Threading.Tasks;

public interface IUpdatableDefinition : IDefinition
{
    Task Update(IBuildServiceProxy proxy);
}
