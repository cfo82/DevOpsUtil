namespace DevOpsUtil.BuildStatus.Avalonia.ViewModels;

using System;
using DevOpsUtil.Core.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Mvvm;

public class MainWindowViewModel : BindableBase
{
    private readonly IErrorHandler _errorHandler;
    private readonly IRefreshService _refreshService;
    private bool _hasError;
    private bool _refreshing;

    public MainWindowViewModel(
        IContainerProvider containerProvider,
        IConfiguration configuration,
        ILogger<MainWindowViewModel> logger,
        IErrorHandler errorHandler,
        IRefreshService refreshService)
    {
        _errorHandler = errorHandler;
        _refreshService = refreshService;

        _errorHandler.ErrorChanged += OnErrorChanged;

        _refreshService.OnBeginRefresh += OnBeginRefresh;
        _refreshService.OnEndRefresh += OnEndRefresh;
    }

    public bool HasError
    {
        get => _hasError;
        set { SetProperty(ref _hasError, value); }
    }

    public bool IsRefreshing
    {
        get => _refreshing;
        set => SetProperty(ref _refreshing, value);
    }

    private void OnErrorChanged(object? sender, EventArgs e)
    {
        HasError = _errorHandler.Error != null;
    }

    private void OnBeginRefresh(object? sender, EventArgs e)
    {
        IsRefreshing = true;
    }

    private void OnEndRefresh(object? sender, EventArgs e)
    {
        IsRefreshing = false;
    }
}