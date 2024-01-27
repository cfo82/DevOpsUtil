namespace DevOpsUtil.AzureDevOps.Core.Services;

using DevOpsUtil.AzureDevOps.Core.Contracts;

public class ErrorHttpClientFactory : IHttpClientFactory
{
    public IHttpClient CreateClient()
    {
        return new ErrorHttpClient();
    }
}
