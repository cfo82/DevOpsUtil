namespace DevOpsUtil.AzureDevOps.Core.Contracts;

public interface IHttpClientFactory
{
    IHttpClient CreateClient();
}
