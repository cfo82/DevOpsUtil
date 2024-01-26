namespace DevOpsUtil.BuildStatus.Core.AzureDevOps;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using DevOpsUtil.BuildStatus.Core.Interfaces;

public class DefaultHttpClient : IHttpClient
{
    private readonly HttpClient _client;
    private bool _disposed;

    public DefaultHttpClient(HttpClient client)
    {
        _client = client;
    }

    ~DefaultHttpClient()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<IHttpResponseMessage> GetAsync(string requestUri)
    {
        return new DefaultHttpResponse(await _client.GetAsync(requestUri));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _client.Dispose();
        }

        _disposed = true;
    }
}
