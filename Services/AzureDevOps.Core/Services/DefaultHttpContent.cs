namespace DevOpsUtil.AzureDevOps.Core.Services;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using DevOpsUtil.AzureDevOps.Core.Contracts;

internal class DefaultHttpContent : IHttpContent
{
    private readonly HttpContent _content;
    private bool _disposed;

    public DefaultHttpContent(HttpContent content)
    {
        _content = content;
    }

    ~DefaultHttpContent()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task<string> ReadAsStringAsync()
    {
        return _content.ReadAsStringAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _content.Dispose();
        }

        _disposed = true;
    }
}