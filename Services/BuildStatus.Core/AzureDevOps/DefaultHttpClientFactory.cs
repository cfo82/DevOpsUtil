namespace DevOpsUtil.BuildStatus.Core.AzureDevOps;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using DevOpsUtil.BuildStatus.Core.Interfaces;

public class DefaultHttpClientFactory : IHttpClientFactory
{
    private readonly IConfiguration _configuration;

    public DefaultHttpClientFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private string Credentials => Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", string.Empty, _configuration.BuildAccessToken)));

    public IHttpClient CreateClient()
    {
        HttpClient client = null;

        try
        {
            client = new HttpClient { BaseAddress = new Uri(_configuration.BaseAddress) };

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
