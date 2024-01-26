namespace DevOpsUtil.BuildStatus.Wpf.Services;

using System;
using System.Windows.Media.Imaging;
using DevOpsUtil.BuildStatus.Wpf.Contracts;
using DevOpsUtil.Core.Contracts;
using Hardcodet.Wpf.TaskbarNotification;

public class TaskbarIconService : ITaskbarIconService
{
    private const string SuccessText = "All builds succeeded.";
    private const string ErrorText = "One or more builds failed.";

    private readonly IErrorStateService _errorStateService;
    private readonly TaskbarIcon _icon;

    public TaskbarIconService(IErrorStateService errorStateService)
    {
        _errorStateService = errorStateService;

        _icon = new TaskbarIcon();
        _icon.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/Images/ok.ico", UriKind.RelativeOrAbsolute));
        _icon.ToolTipText = SuccessText;
        _icon.TrayMouseDoubleClick += (sender, args) => Activate?.Invoke(this, EventArgs.Empty);

        _errorStateService.ErrorStateChanged += OnErrorStateChanged;
    }

    public event EventHandler? Activate;

    private void OnErrorStateChanged(object? sender, EventArgs e)
    {
        if (_errorStateService.HasError)
        {
            _icon.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/Images/alert.ico", UriKind.RelativeOrAbsolute));
            _icon.ToolTipText = ErrorText;
        }
        else
        {
            _icon.IconSource = new BitmapImage(new Uri(@"pack://application:,,,/Images/ok.ico", UriKind.RelativeOrAbsolute));
            _icon.ToolTipText = SuccessText;
        }
    }
}