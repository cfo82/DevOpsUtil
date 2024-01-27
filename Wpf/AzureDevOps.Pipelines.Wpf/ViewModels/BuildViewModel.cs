namespace DevOpsUtil.AzureDevOps.Pipelines.Wpf.ViewModels;

using System.Collections.ObjectModel;
using System.Windows.Input;
using DevOpsUtil.AzureDevOps.Pipelines.Contracts;
using DevOpsUtil.Core.Contracts;
using Prism.Commands;
using Prism.Mvvm;

public class BuildViewModel : BindableBase
{
    private const int NumberOfDaysUntilTooOld = 14;

    private readonly IBrowserService _browserService;
    private readonly IBuild _build;

    public BuildViewModel(IBrowserService browserService, IBuild build)
    {
        _browserService = browserService;
        _build = build;

        OpenUrlCommand = new DelegateCommand<Uri?>(OpenUrl);
    }

    public ICommand OpenUrlCommand { get; }

    public bool IsOld => _build.Age > NumberOfDaysUntilTooOld;

    public bool IsNotOld => !IsOld;

    public int Age => _build.Age;

    public Uri Uri => _build.Uri;

    public string SourceBranch => _build.SourceBranch;

    public bool IsRunning => _build.IsRunning;

    public bool WasSuccessful => _build.WasSuccessful;

    public bool HasFailed => _build.HasFailed;

    private void OpenUrl(Uri? url)
    {
        if (url != null)
        {
            _browserService.OpenUrl(url);
        }
    }
}