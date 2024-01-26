namespace DevOpsUtil.BuildStatus.Wpf.ViewModels;

using System;
using DevOpsUtil.Core.Contracts;
using Prism.Mvvm;

public class RefreshViewModel : BindableBase
{
    private readonly IRefreshService _refreshService;
    private bool _refreshing;

    public RefreshViewModel(IRefreshService refreshService)
    {
        _refreshService = refreshService;

        _refreshService.OnBeginRefresh += OnBeginRefresh;
        _refreshService.OnEndRefresh += OnEndRefresh;
    }

    public bool IsRefreshing
    {
        get => _refreshing;
        set => SetProperty(ref _refreshing, value);
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