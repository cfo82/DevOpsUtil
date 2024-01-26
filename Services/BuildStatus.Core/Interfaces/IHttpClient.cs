namespace DevOpsUtil.BuildStatus.Core.Interfaces;

using System;
using System.Threading.Tasks;

public interface IHttpClient : IDisposable
{
    Task<IHttpResponseMessage> GetAsync(string requestUri);
}
