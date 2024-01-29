namespace DevOpsUtil.AzureDevOps.Releases.Wpf.ViewModels;

using System.Collections.ObjectModel;
using System.Windows.Input;
using DevOpsUtil.AzureDevOps.Releases.Contracts;
using DevOpsUtil.Core.Contracts;
using Prism.Commands;
using Prism.Mvvm;

public class ReleaseViewModel : BindableBase
{
    private const int NumberOfDaysUntilTooOld = 14;
    private readonly IBrowserService _browserService;
    private readonly IRelease _release;

    public ReleaseViewModel(IBrowserService browserService, IRelease release)
    {
        _browserService = browserService;
        _release = release;

        OpenUrlCommand = new DelegateCommand<Uri?>(OpenUrl);
        Environments = new ObservableCollection<EnvironmentViewModel>();
        foreach (var environment in release.Environments)
        {
            Environments.Add(new EnvironmentViewModel(
                environment, environment != release.Environments.Last()));
        }
    }

    public ICommand OpenUrlCommand { get; }

    public ObservableCollection<EnvironmentViewModel> Environments { get; }

    public string Name => _release.Name;

    public bool IsOld => _release.Age > NumberOfDaysUntilTooOld;

    public bool IsNotOld => !IsOld;

    public int Age => _release.Age;

    public Uri Uri => _release.Uri;

    private void OpenUrl(Uri? url)
    {
        if (url != null)
        {
            _browserService.OpenUrl(url);
        }
    }
}