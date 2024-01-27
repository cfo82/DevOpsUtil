namespace DevOpsUtil.BuildStatus.Core.AzureDevOps;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevOpsUtil.BuildStatus.Core.AzureDevOps.Serialization;
using DevOpsUtil.BuildStatus.Core.Interfaces;
using DevOpsUtil.Core.Contracts;
using Newtonsoft.Json;

/*public class BuildServiceProxy : IBuildServiceProxy
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IErrorHandler _errorHandler;
    private readonly ILogService _logService;
    private readonly ITrafficLightService _trafficLightService;
    private readonly List<IUpdatableDefinition> _definitions;
    private readonly SemaphoreSlim _definitionsGuard;
    private bool _definitionsInitialized;
    private DateTime _lastDefinitionListUpdate;
    private string _lastFailedMessage;

    public BuildServiceProxy(IConfiguration configuration, IHttpClientFactory httpClientFactory, IErrorHandler errorHandler, ILogService logService, ITrafficLightService trafficLightService)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _errorHandler = errorHandler;
        _logService = logService;
        _trafficLightService = trafficLightService;

        _definitions = new List<IUpdatableDefinition>();
        _definitionsGuard = new SemaphoreSlim(1, 1);
    }

    public event EventHandler DefinitionsChanged;

    public event EventHandler AllDefinitionsUpdated;

    public IHttpClientFactory HttpClientFactory
    {
        get { return _httpClientFactory; }
    }

    public async Task ManualRefresh(bool forceDefinitionUpdate)
    {
        await Refresh(true);
    }

    public async Task<IDefinition[]> GetDefinitions()
    {
        try
        {
            await _definitionsGuard.WaitAsync();

            // issues with co-variant
            return _definitions.Select(e => (IDefinition)e).ToArray();
        }
        finally
        {
            _definitionsGuard.Release();
        }
    }

    private async Task ListDefinitions(bool forceUpdate)
    {
        try
        {
            await _definitionsGuard.WaitAsync();

            if (_definitionsInitialized && !forceUpdate)
            {
                return;
            }

            _definitions.Clear();

            using (var client = _httpClientFactory.CreateClient())
            {
                using (var response = await client.GetAsync("_apis/build/definitions?api-version=2.0"))
                {
                    using (var content = response.Content)
                    {
                        var responseString = await content.ReadAsStringAsync();

                        try
                        {
                            var buildDefinitions = JsonConvert.DeserializeObject<BuildDefinitionListData>(responseString);

                            foreach (var def in buildDefinitions.Value)
                            {
                                _definitions.Add(new BuildDefinition(_configuration, def));

                                await _definitions.Last().Update(this);
                            }
                        }
                        catch (JsonReaderException e)
                        {
                            throw new Exception("Json read exception: " + e.Message + ", JSON: " + responseString, e);
                        }
                    }
                }

                using (var response = await client.GetAsync("_apis/release/definitions?api-version=3.0-preview.1"))
                {
                    using (var content = response.Content)
                    {
                        var responseString = await content.ReadAsStringAsync();

                        try
                        {
                            var releaseDefinitions = JsonConvert.DeserializeObject<ReleaseDefinitionListData>(responseString);

                            foreach (var def in releaseDefinitions.Value)
                            {
                                _definitions.Add(new ReleaseDefinition(_configuration, def));

                                await _definitions.Last().Update(this);
                            }
                        }
                        catch (JsonReaderException e)
                        {
                            throw new Exception("Json read exception: " + e.Message + ", JSON: " + responseString, e);
                        }
                    }
                }
            }

            UpdateTrafficLight();

            DefinitionsChanged?.Invoke(this, new EventArgs());

            _errorHandler.Error = null;
            _definitionsInitialized = true;
            _lastDefinitionListUpdate = DateTime.Now;
        }
        catch (Exception e)
        {
            _errorHandler.Error = e;
        }
        finally
        {
            _definitionsGuard.Release();
        }
    }

    private async Task Refresh(bool forceDefinitionUpdate)
    {
        try
        {
            if (!_definitionsInitialized)
            {
                await ListDefinitions(false);
            }

            if (forceDefinitionUpdate || (DateTime.Now - _lastDefinitionListUpdate).TotalMilliseconds > _configuration.DefinitionListingInterval)
            {
                await ListDefinitions(true);
            }

            if (_definitionsInitialized)
            {
                // if builds are not initialized => try to initialize again
                try
                {
                    await _definitionsGuard.WaitAsync();

                    foreach (var definition in _definitions)
                    {
                        await definition.Update(this);
                    }

                    UpdateTrafficLight();
                }
                finally
                {
                    _definitionsGuard.Release();
                }

                AllDefinitionsUpdated?.Invoke(this, new EventArgs());

                _errorHandler.Error = null;
            }
        }
        catch (Exception e)
        {
            _errorHandler.Error = e;
        }
    }

    // NOTE: must be called from within the Semaphore!
    private void UpdateTrafficLight()
    {
        // somehow with the UWP app the LINQ query does not work. The equivalent
        // foreach loop works without a problem
        var failedDefinitions = new List<IUpdatableDefinition>();
        foreach (var def in _definitions)
        {
            if ((!def.HasRunInstance || def.HasFailed) && !(def.IsIgnored || def.IsDisabled))
            {
                failedDefinitions.Add(def);
            }
        }

        /*var failedDefinitions = (from def in _definitions
                                 where (!def.HasRunInstance || def.HasFailed) && !(def.IsIgnored  || def.IsDisabled)
                                 select def).ToList();/

        var hasFailed = failedDefinitions.Any();

        if (hasFailed)
        {
            var failedNames = failedDefinitions.Select(item => item.Name);

            var failedList = failedNames.Aggregate((first, second) => first + ", " + second);

            var message = $"BuildServiceProxy: Definitions failed are '{failedList}'";

            if (!string.Equals(_lastFailedMessage, message))
            {
                _logService.WriteEntry(message);
                _lastFailedMessage = message;
            }
        }
        else
        {
            _lastFailedMessage = string.Empty;
        }

        _trafficLightService.Color = hasFailed ? TrafficLightColor.Red : TrafficLightColor.Green;
    }
}*/
