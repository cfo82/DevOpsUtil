namespace DevOpsUtil.BuildStatus.Core.AzureDevOps;

using DevOpsUtil.BuildStatus.Core.Interfaces;

public class ErrorHttpClientFactory : IHttpClientFactory
{
    public IHttpClient CreateClient()
    {
        return new ErrorHttpClient();
    }
}
