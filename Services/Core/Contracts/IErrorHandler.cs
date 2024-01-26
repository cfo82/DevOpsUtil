namespace DevOpsUtil.Core.Contracts;

using System;

public interface IErrorHandler
{
    event EventHandler? ErrorChanged;

    Exception? Error { get; set; }
}