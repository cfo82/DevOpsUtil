namespace DevOpsUtil.AzureDevOps.Core.Services;

using System;
using System.Threading.Tasks;
using DevOpsUtil.AzureDevOps.Core.Contracts;

public class ErrorHttpClient : IHttpClient
{
    private bool _disposed;

    ~ErrorHttpClient()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task<IHttpResponseMessage> GetAsync(string requestUri)
    {
        throw new Exception("Error");
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
        }

        _disposed = true;
    }
}
