namespace DevOpsUtil.AzureDevOps.Releases.Wpf.ViewModels;

using DevOpsUtil.AzureDevOps.Releases.Contracts;
using DevOpsUtil.Core.Contracts;
using Prism.Mvvm;

public class ReleaseDefinitionViewModel : BindableBase
{
    private readonly IBrowserService _browserService;
    private IReleaseDefinition _releaseDefinition;

    public ReleaseDefinitionViewModel(IBrowserService browserService, IReleaseDefinition releaseDefinition)
    {
        _browserService = browserService;
        _releaseDefinition = releaseDefinition;

        if (_releaseDefinition.LatestRelease != null)
        {
            LatestRelease = new ReleaseViewModel(_browserService, _releaseDefinition.LatestRelease);
        }
    }

    public ReleaseViewModel? LatestRelease { get; private set; }

    public bool HasLatestRelease => LatestRelease != null;

    public IReleaseDefinition ReleaseDefinition
    {
        get => _releaseDefinition;
        set
        {
            if (_releaseDefinition != value)
            {
                _releaseDefinition = value;

                if (_releaseDefinition.LatestRelease != null)
                {
                    LatestRelease = new ReleaseViewModel(_browserService, _releaseDefinition.LatestRelease);
                }

                RaisePropertyChanged(string.Empty);
            }
        }
    }

    public string Name => _releaseDefinition.Name;

    public bool IsIgnored => LatestRelease == null || _releaseDefinition.IsIgnored;

    public bool IsRunning => !IsIgnored && _releaseDefinition.IsRunning;

    public bool WasSuccessful => !IsIgnored && _releaseDefinition.WasSuccessful;

    public bool HasFailed
    {
        get
        {
            if (IsIgnored)
            {
                return false;
            }

            return _releaseDefinition.HasFailed;
        }
    }
}