namespace DevOpsUtil.BuildStatus.Core.AzureDevOps;

using System;
using System.Threading.Tasks;
using DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;
using DevOpsUtil.BuildStatus.Core.Interfaces;
using Newtonsoft.Json;

public class ReleaseDefinition : IUpdatableDefinition
{
    private readonly IConfiguration _configuration;
    private readonly ReleaseDefinitionData _releaseDefinitionData;
    private Release _latestRelease;

    public ReleaseDefinition(IConfiguration configuration, ReleaseDefinitionData releaseDefinitionData)
    {
        _configuration = configuration;
        _releaseDefinitionData = releaseDefinitionData;
    }

    public event EventHandler Changed;

    public string Type => "Release";

    public bool WasSuccessful => _latestRelease?.WasSuccessful ?? false;

    public bool IsDisabled => false;

    public bool IsIgnored => _configuration.ShouldIgnoreRelease(Name);

    public string Name => _releaseDefinitionData.Name;

    public bool HasUri => _latestRelease != null;

    public Uri Uri => new Uri(HasUri ? $"{_configuration.BaseAddress}_release?releaseId={_latestRelease.Id}&_a=release-summary" : _configuration.BaseAddress);

    public bool HasRunInstance => _latestRelease != null;

    public bool HasFailed => _latestRelease?.HasFailed ?? true;

    public bool IsRunning => _latestRelease?.IsRunning ?? false;

    public string Result
    {
        get
        {
            if (IsIgnored)
            {
                return "ignored";
            }

            return _latestRelease?.Result ?? string.Empty;
        }
    }

    public int Age => _latestRelease != null ? _latestRelease.Age : 0;

    public async Task Update(IBuildServiceProxy proxy)
    {
        if (IsIgnored)
        {
            return;
        }

        using (var client = ((BuildServiceProxy)proxy).HttpClientFactory.CreateClient())
        {
            using (var response = await client.GetAsync($"_apis/release/releases?definitionId={_releaseDefinitionData.Id}&status_filter=completed&top=10&api-version=3.0-preview.1"))
            {
                using (var content = response.Content)
                {
                    var responseString = await content.ReadAsStringAsync();

                    try
                    {
                        var releaseState = JsonConvert.DeserializeObject<ReleaseListData>(responseString);

                        if (releaseState.Count > 0)
                        {
                            var latestReleaseIndex = 0;

                            while (
                                latestReleaseIndex < releaseState.Count &&
                                string.Equals(releaseState.Value[latestReleaseIndex].Status, "abandoned"))
                            {
                                ++latestReleaseIndex;
                            }

                            if (latestReleaseIndex >= releaseState.Count)
                            {
                                _latestRelease = null;
                                Changed?.Invoke(this, new EventArgs());
                            }
                            else
                            {
                                var url = releaseState.Value[latestReleaseIndex].Url;

                                using (var detailedResponse = await client.GetAsync(url))
                                {
                                    using (var detailedContent = detailedResponse.Content)
                                    {
                                        responseString = await detailedContent.ReadAsStringAsync();

                                        var release = JsonConvert.DeserializeObject<ReleaseData>(responseString);

                                        _latestRelease = new Release(release);

                                        Changed?.Invoke(this, new EventArgs());
                                    }
                                }
                            }
                        }
                    }
                    catch (JsonReaderException e)
                    {
                        throw new Exception("Json read exception: " + e.Message + ", JSON: " + responseString, e);
                    }
                }
            }
        }
    }
}
