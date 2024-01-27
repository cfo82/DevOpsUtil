namespace DevOpsUtil.AzureDevOps.Core.Contracts;

using System;

public interface IHttpResponseMessage : IDisposable
{
    IHttpContent Content { get; }
}
