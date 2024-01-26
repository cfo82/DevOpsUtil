namespace DevOpsUtil.BuildStatus.Wpf.Services;

using System;
using System.Threading;
using System.Windows;
using DevOpsUtil.Core.Contracts;

public class UiDispatcherService : IUiDispatcherService
{
    public UiDispatcherService()
    {
    }

    public void Post(Action action)
    {
        if (Application.Current != null)
        {
            Application.Current.Dispatcher.BeginInvoke(action);
        }
    }

    public void CheckUiThread()
    {
        if (Application.Current.Dispatcher.Thread != Thread.CurrentThread)
        {
            throw new InvalidOperationException("Must be running on the UI thread.");
        }
    }
}