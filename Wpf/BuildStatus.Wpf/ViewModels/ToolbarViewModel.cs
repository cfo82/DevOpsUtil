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

    public ToolbarViewModel(
        IErrorHandler errorHandler,
        IRefreshService refreshService,
        IStartupService startupService)
    {
        _errorHandler = errorHandler;
        _refreshService = refreshService;
        _startupService = startupService;

        RefreshCommand = new Prism.Commands.DelegateCommand(OnRefresh);
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

    private async void OnRefresh()
    {
        try
        {
            await _refreshService.RefreshManualAsync();

            _errorHandler.Error = null;
        }
        catch (Exception e)
        {
            _errorHandler.Error = e;
        }
    }
}