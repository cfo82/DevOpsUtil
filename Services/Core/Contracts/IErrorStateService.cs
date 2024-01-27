namespace DevOpsUtil.Core.Contracts;

using System;

public interface IErrorStateService
{
    event EventHandler? ErrorStateChanged;

    bool HasError { get; }

    void SubscribeErrorProvider(IErrorStateProvider errorStateProvider);

    void UnsubscribeErrorProvider(IErrorStateProvider errorStateProvider);
}