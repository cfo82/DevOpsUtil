namespace DevOpsUtil.BuildStatus.Core.Interfaces;

using System;

public interface IHttpResponseMessage : IDisposable
{
    IHttpContent Content { get; }
}
