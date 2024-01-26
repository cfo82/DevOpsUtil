namespace DevOpsUtil.BuildStatus.Wpf.Views;

using System;
using System.Windows;
using DevOpsUtil.BuildStatus.Wpf.Contracts;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow(
        ITaskbarIconService taskbarIconService)
    {
        InitializeComponent();

        taskbarIconService.Activate += ViewModel_OnActivateEvent;
    }

    private void ViewModel_OnActivateEvent(object? sender, EventArgs e)
    {
        if (WindowState == WindowState.Minimized)
        {
            WindowState = WindowState.Normal;
        }

        Activate();
    }
}