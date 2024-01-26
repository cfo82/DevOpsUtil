namespace DevOpsUtil.BuildStatus.Core.Interfaces;

using System;
using System.Threading.Tasks;

public interface IHttpContent : IDisposable
{
    Task<string> ReadAsStringAsync();
}
