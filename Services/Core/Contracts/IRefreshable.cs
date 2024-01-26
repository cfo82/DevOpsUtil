namespace DevOpsUtil.Core.Contracts;

using System.Threading.Tasks;

public interface IRefreshable
{
    Task RefreshAsync(bool forceRefresh);
}