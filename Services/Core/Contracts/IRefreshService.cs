namespace DevOpsUtil.Core.Contracts;

using System;
using System.Threading.Tasks;

public interface IRefreshService
{
    public event EventHandler<EventArgs>? OnBeginRefresh;

    public event EventHandler<EventArgs>? OnEndRefresh;

    Task RefreshManualAsync();

    void RegisterRefreshable(IRefreshable refreshable);
}