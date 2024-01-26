namespace DevOpsUtil.BuildStatus.Core.Interfaces;

public interface IHttpClientFactory
{
    IHttpClient CreateClient();
}
