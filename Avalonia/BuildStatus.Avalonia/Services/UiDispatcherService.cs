namespace DevOpsUtil.BuildStatus.Avalonia.Services;

using System;
using DevOpsUtil.Core.Contracts;
using global::Avalonia.Threading;

public class UiDispatcherService : IUiDispatcherService
{
    public void Post(Action action)
    {
        Dispatcher.UIThread.Post(action);
    }

    public void CheckUiThread()
    {
        if (!Dispatcher.UIThread.CheckAccess())
        {
            throw new InvalidOperationException("Must be running on the ui thread.");
        }
    }
}