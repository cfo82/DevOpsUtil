namespace DevOpsUtil.Core.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using DevOpsUtil.Core.Contracts;

public class ErrorStateService : IErrorStateService
{
    private readonly IRefreshService _refreshService;
    private readonly List<IErrorStateProvider> _errorStateProviders;
    private bool _hasError;
    private bool _refreshing;
    private bool _needsRefresh;

    public ErrorStateService(IRefreshService refreshService)
    {
        _refreshService = refreshService;
        _errorStateProviders = new List<IErrorStateProvider>();
        _hasError = false;
        _refreshing = false;
        _needsRefresh = false;

        _refreshService.OnBeginRefresh += BeginRefresh;
        _refreshService.OnEndRefresh += EndRefresh;
    }

    public event EventHandler? ErrorStateChanged;

    public bool HasError => _hasError;

    public void SubscribeErrorProvider(IErrorStateProvider errorStateProvider)
    {
        if (!_errorStateProviders.Contains(errorStateProvider))
        {
            _errorStateProviders.Add(errorStateProvider);
            errorStateProvider.ErrorStateChanged += RefreshErrorStates;
            RefreshErrorStates(null, EventArgs.Empty);
        }
    }

    public void UnsubscribeErrorProvider(IErrorStateProvider errorStateProvider)
    {
        if (_errorStateProviders.Contains(errorStateProvider))
        {
            _errorStateProviders.Remove(errorStateProvider);
            errorStateProvider.ErrorStateChanged -= RefreshErrorStates;
            RefreshErrorStates(null, EventArgs.Empty);
        }
    }

    private void RefreshErrorStates(object? sender, EventArgs e)
    {
        if (_refreshing)
        {
            _needsRefresh = true;
            return;
        }

        bool oldHasError = _hasError;
        _needsRefresh = false;
        _hasError = _errorStateProviders.Count > 0 &&
                    _errorStateProviders.Any(provider => provider.HasError);

        if (oldHasError != _hasError)
        {
            ErrorStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void BeginRefresh(object? sender, EventArgs e)
    {
        _refreshing = true;
    }

    private void EndRefresh(object? sender, EventArgs e)
    {
        _refreshing = false;

        if (_needsRefresh)
        {
            RefreshErrorStates(null, EventArgs.Empty);
        }
    }
}