namespace DevOpsUtil.BuildStatus.Avalonia.ViewModels;

using System;
using System.Windows.Input;
using DevOpsUtil.Core.Contracts;
using Prism.Commands;
using Prism.Mvvm;

public class ToolbarViewModel : BindableBase
{
    private readonly IErrorHandler _errorHandler;
    private readonly IRefreshService _refreshService;

    public ToolbarViewModel(IErrorHandler errorHandler, IRefreshService refreshService)
    {
        _errorHandler = errorHandler;
        _refreshService = refreshService;
        RefreshCommand = new DelegateCommand(OnRefresh);
    }

    public ICommand RefreshCommand { get; private set; }

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