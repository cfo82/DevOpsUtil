namespace DevOpsUtil.Core.Contracts;

using System;

public interface IUiDispatcherService
{
    void Post(Action action);

    void CheckUiThread();
}