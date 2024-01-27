namespace DevOpsUtil.AzureDevOps.Core.Services;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using DevOpsUtil.AzureDevOps.Core.Contracts;

public class DefaultHttpClientFactory : IHttpClientFactory
{
    private readonly string _baseAddress;
    private readonly string _accessToken;

    public DefaultHttpClientFactory(string baseAddress, string accessToken)
    {
        _baseAddress = baseAddress;
        _accessToken = accessToken;
    }

    private string Credentials => Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", string.Empty, _accessToken)));

    public IHttpClient CreateClient()
    {
        HttpClient? client = null;

        try
        {
            client = new HttpClient { BaseAddress = new Uri(_baseAddress) };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Credentials);

            return new DefaultHttpClient(client);
        }
        catch (Exception)
        {
            client?.Dispose();
            throw;
        }
    }
}
