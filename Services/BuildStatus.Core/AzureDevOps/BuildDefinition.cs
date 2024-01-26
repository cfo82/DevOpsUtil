namespace DevOpsUtil.BuildStatus.Core.AzureDevOps;

using System;
using System.Threading.Tasks;
using DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;
using DevOpsUtil.BuildStatus.Core.Interfaces;
using Newtonsoft.Json;

public class BuildDefinition : IUpdatableDefinition
{
    private readonly IConfiguration _configuration;
    private readonly BuildDefinitionData _buildDefinitionData;
    private Build _latestBuild;

    public BuildDefinition(IConfiguration configuration, BuildDefinitionData buildDefinitionData)
    {
        _configuration = configuration;
        _buildDefinitionData = buildDefinitionData;
    }

    public event EventHandler Changed;

    public string Type => "Build";

    public bool IsDraft => string.Equals(_buildDefinitionData.Quality, "draft");

    public bool WasSuccessful => string.Equals(Result, "succeeded");

    public bool IsDisabled => _buildDefinitionData.QueueStatus == "disabled";

    public bool IsIgnored => IsDraft || _configuration.ShouldIgnoreBuild(Name);

    public string Name => _buildDefinitionData.Name;

    public bool HasUri => _latestBuild != null;

    public Uri Uri => new Uri(HasUri ? $"{_configuration.BaseAddress}_build/index?buildId={_latestBuild.Id}&_a=summary" : _configuration.BaseAddress);

    public bool HasRunInstance => _latestBuild != null;

    public bool HasFailed => _latestBuild?.HasFailed ?? true;

    public bool IsRunning => _latestBuild?.IsRunning ?? false;

    public string Result
    {
        get
        {
            if (IsDraft)
            {
                return "draft";
            }

            if (IsDisabled)
            {
                return "disabled";
            }

            if (IsIgnored)
            {
                return "ignored";
            }

            if (_latestBuild == null)
            {
                return "not available";
            }

            return _latestBuild?.Result;
        }
    }

    public int Age => _latestBuild != null ? _latestBuild.Age : 0;

    public async Task Update(IBuildServiceProxy proxy)
    {
        if (IsIgnored || IsDisabled)
        {
            return;
        }

        using (var client = ((BuildServiceProxy)proxy).HttpClientFactory.CreateClient())
        {
            using (var response = await client.GetAsync($"_apis/build/builds?definitions={_buildDefinitionData.Id}&status_filter=completed&top=1&api-version=2.0"))
            {
                using (var content = response.Content)
                {
                    var responseString = await content.ReadAsStringAsync();

                    try
                    {
                        var buildState = JsonConvert.DeserializeObject<BuildListData>(responseString);

                        if (buildState.Count > 0)
                        {
                            var latestBuildIndex = 0;

                            while (
                                latestBuildIndex < buildState.Count &&
                                !string.IsNullOrEmpty(buildState.Value[latestBuildIndex].SourceBranch) &&
                                !buildState.Value[latestBuildIndex].SourceBranch.StartsWith("$") &&
                                !buildState.Value[latestBuildIndex].SourceBranch.StartsWith("refs/heads/master") &&
                                !buildState.Value[latestBuildIndex].SourceBranch.StartsWith("refs/heads/release"))
                            {
                                ++latestBuildIndex;
                            }

                            if (latestBuildIndex < buildState.Count)
                            {
                                _latestBuild = new Build(this, buildState.Value[latestBuildIndex]);

                                Changed?.Invoke(this, new EventArgs());
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
