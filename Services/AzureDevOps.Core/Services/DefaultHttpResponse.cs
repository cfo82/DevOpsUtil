namespace DevOpsUtil.AzureDevOps.Core.Services;

using System;
using System.Net.Http;
using DevOpsUtil.AzureDevOps.Core.Contracts;

internal class DefaultHttpResponse : IHttpResponseMessage
{
    private readonly HttpResponseMessage _httpResponseMessage;
    private bool _disposed;

    public DefaultHttpResponse(HttpResponseMessage httpResponseMessage)
    {
        _httpResponseMessage = httpResponseMessage;
    }

    ~DefaultHttpResponse()
    {
        Dispose(false);
    }

    public IHttpContent Content
    {
        get { return new DefaultHttpContent(_httpResponseMessage.Content); }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _httpResponseMessage.Dispose();
        }

        _disposed = true;
    }
}