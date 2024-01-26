namespace DevOpsUtil.BuildStatus.Avalonia.ViewModels;

using System;
using DevOpsUtil.Core.Contracts;
using Prism.Mvvm;

public class ErrorViewModel : BindableBase
{
    private readonly IErrorHandler _errorHandler;
    private string _errorText;

    public ErrorViewModel(IErrorHandler errorHandler)
    {
        _errorHandler = errorHandler;
        _errorText = string.Empty;

        _errorHandler.ErrorChanged += OnErrorChanged;
    }

    public string ErrorText
    {
        get => _errorText;
        set { SetProperty(ref _errorText, value); }
    }

    private void OnErrorChanged(object? sender, EventArgs e)
    {
        ErrorText = _errorHandler.Error?.ToString() ?? string.Empty;
    }
}