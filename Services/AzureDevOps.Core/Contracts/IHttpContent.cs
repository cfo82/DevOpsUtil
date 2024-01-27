namespace DevOpsUtil.AzureDevOps.Core.Contracts;

using System;
using System.Threading.Tasks;

public interface IHttpContent : IDisposable
{
    Task<string> ReadAsStringAsync();
}
