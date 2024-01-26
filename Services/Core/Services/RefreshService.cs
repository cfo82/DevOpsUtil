namespace DevOpsUtil.Core.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevOpsUtil.Core.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class RefreshService : IRefreshService
{
    private readonly ILogger<RefreshService> _logger;
    private readonly IUiDispatcherService _uiDispatcherService;
    private readonly IErrorHandler _errorHandler;
    private readonly List<IRefreshable> _refreshables;
    private readonly RefreshSettings _settings;
    private bool _refreshing;
    private DateTime _lastForceUpdate;

    public RefreshService(IConfiguration configuration, ILogger<RefreshService> logger, IUiDispatcherService uiDispatcherService, IErrorHandler errorHandler)
    {
        _logger = logger;
        _uiDispatcherService = uiDispatcherService;
        _errorHandler = errorHandler;
        _refreshables = new List<IRefreshable>();
        _settings = RefreshSettings.Load(configuration);
        _refreshing = false;
        _lastForceUpdate = DateTime.Now - TimeSpan.FromMilliseconds(_settings.ForceUpdateInterval - 1);
        _ = new Timer(
            _ => uiDispatcherService.Post(OnTimer),
            null,
            0,
            _settings.PollingInterval);
    }

    public event EventHandler<EventArgs>? OnBeginRefresh;

    public event EventHandler<EventArgs>? OnEndRefresh;

    public async Task RefreshManualAsync()
    {
        await RefreshAsync(true);
    }

    public void RegisterRefreshable(IRefreshable refreshable)
    {
        _uiDispatcherService.CheckUiThread();

        _refreshables.Add(refreshable);
    }

    private async Task RefreshAsync(bool forceRefresh)
    {
        _uiDispatcherService.CheckUiThread();

        if (_refreshing)
        {
            _logger.LogInformation("RefreshAsync was called while another Refresh was in progress.");
            return;
        }

        try
        {
            _refreshing = true;

            OnBeginRefresh?.Invoke(this, EventArgs.Empty);

            // make sure the refresh UI pops up and is visible
            await Task.Delay(200);

            _logger.LogInformation("Starting refresh");

            var exceptions = new List<Exception>();

            foreach (var refreshable in _refreshables)
            {
                try
                {
                    var currentTime = DateTime.Now;
                    forceRefresh = forceRefresh
                                   || (currentTime - _lastForceUpdate).TotalMilliseconds >
                                   _settings.ForceUpdateInterval;

                    await refreshable.RefreshAsync(forceRefresh);

                    if (forceRefresh)
                    {
                        _lastForceUpdate = currentTime;
                    }
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count > 0)
            {
                _errorHandler.Error = new AggregateException(exceptions);
            }

            _logger.LogInformation("Refresh finished");

            OnEndRefresh?.Invoke(this, EventArgs.Empty);
        }
        finally
        {
            _refreshing = false;
        }
    }

    private async void OnTimer()
    {
        try
        {
            await RefreshAsync(false);
        }
        catch (Exception e)
        {
            _errorHandler.Error = e;
        }
    }
}