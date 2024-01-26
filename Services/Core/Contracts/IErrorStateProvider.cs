namespace DevOpsUtil.Core.Contracts;

using System;

public interface IErrorStateProvider
{
    event EventHandler ErrorStateChanged;

    bool HasError { get; }
}