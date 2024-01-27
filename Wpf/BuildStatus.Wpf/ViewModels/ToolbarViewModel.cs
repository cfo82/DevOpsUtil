namespace DevOpsUtil.BuildStatus.Wpf.ViewModels;

using System;
using System.Windows.Input;
using DevOpsUtil.Core.Contracts;
using Prism.Mvvm;

public class ToolbarViewModel : BindableBase
{
    private readonly IErrorHandler _errorHandler;
    private readonly IRefreshService _refreshService;
    private readonly IStartupService _startupService;
    private bool _isRefreshInProgress;

    public ToolbarViewModel(
        IErrorHandler errorHandler,
        IRefreshService refreshService,
        IStartupService startupService)
    {
        _errorHandler = errorHandler;
        _refreshService = refreshService;
        _startupService = startupService;

        RefreshCommand = new Prism.Commands.DelegateCommand(OnRefresh);

        _refreshService.OnBeginRefresh += OnBeginRefresh;
        _refreshService.OnEndRefresh += OnEndRefresh;
    }

    public ICommand RefreshCommand { get; private set; }

    public bool IsActiveOnStartup
    {
        get => _startupService.IsActiveOnStartup;
        set
        {
            _startupService.IsActiveOnStartup = value;
            RaisePropertyChanged();
        }
    }

    public bool IsRefreshInProgress
    {
        get => _isRefreshInProgress;
        set { SetProperty(ref _isRefreshInProgress, value); }
    }

    private async void OnRefresh()
    {
        try
        {
            if (IsRefreshInProgress)
            {
                return;
            }

            await _refreshService.RefreshManualAsync();

            _errorHandler.Error = null;
        }
        catch (Exception e)
        {
            _errorHandler.Error = e;
        }
    }

    private void OnBeginRefresh(object? sender, EventArgs e)
    {
        IsRefreshInProgress = true;
    }

    private void OnEndRefresh(object? sender, EventArgs e)
    {
        IsRefreshInProgress = false;
    }
}