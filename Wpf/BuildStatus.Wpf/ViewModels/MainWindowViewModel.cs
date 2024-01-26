namespace DevOpsUtil.BuildStatus.Wpf.ViewModels;

using System;
using System.Windows.Shell;
using DevOpsUtil.Core.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Mvvm;

public class MainWindowViewModel : BindableBase
{
    private readonly IErrorStateService _errorStateService;
    private readonly IErrorHandler _errorHandler;
    private readonly IRefreshService _refreshService;
    private bool _hasError;
    private bool _refreshing;

    public MainWindowViewModel(
        IContainerProvider containerProvider,
        IConfiguration configuration,
        ILogger<MainWindowViewModel> logger,
        IErrorStateService errorStateService,
        IErrorHandler errorHandler,
        IRefreshService refreshService)
    {
        _errorStateService = errorStateService;
        _errorHandler = errorHandler;
        _refreshService = refreshService;

        _errorHandler.ErrorChanged += OnErrorChanged;

        _refreshService.OnBeginRefresh += OnBeginRefresh;
        _refreshService.OnEndRefresh += OnEndRefresh;

        _errorStateService.ErrorStateChanged += OnErrorStateChanged;
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

    public bool HasFailed
    {
        get { return _errorStateService.HasError; }
    }

    public bool Succeeded
    {
        get { return !HasFailed; }
    }

    public double TaskBarProgress
    {
        get { return Succeeded ? 0 : 1.0; }
    }

    public TaskbarItemProgressState TaskBarItemState
    {
        get { return Succeeded ? TaskbarItemProgressState.Normal : TaskbarItemProgressState.Error; }
    }

    private void OnErrorStateChanged(object? sender, EventArgs e)
    {
        RaisePropertyChanged(nameof(HasFailed));
        RaisePropertyChanged(nameof(Succeeded));
        RaisePropertyChanged(nameof(TaskBarProgress));
        RaisePropertyChanged(nameof(TaskBarItemState));
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