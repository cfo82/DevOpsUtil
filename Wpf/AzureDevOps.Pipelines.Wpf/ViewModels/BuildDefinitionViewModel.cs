namespace DevOpsUtil.AzureDevOps.Pipelines.Wpf.ViewModels;

using System.Collections.ObjectModel;
using DevOpsUtil.AzureDevOps.Pipelines.Contracts;
using DevOpsUtil.Core.Contracts;
using Prism.Mvvm;

public class BuildDefinitionViewModel : BindableBase
{
    private readonly IBrowserService _browserService;
    private IBuildDefinition _buildDefinition;

    public BuildDefinitionViewModel(IBrowserService browserService, IBuildDefinition buildDefinition)
    {
        _browserService = browserService;

        _buildDefinition = buildDefinition;

        Builds = new ObservableCollection<BuildViewModel>();

        RefreshBuilds();
    }

    public ObservableCollection<BuildViewModel> Builds { get; }

    public IBuildDefinition BuildDefinition
    {
        get => _buildDefinition;
        set
        {
            if (_buildDefinition != value)
            {
                _buildDefinition = value;

                RefreshBuilds();

                RaisePropertyChanged(string.Empty);
            }
        }
    }

    public bool IsIgnored => Builds.Count == 0 || _buildDefinition.IsIgnored;

    public bool IsRunning => !IsIgnored && _buildDefinition.IsRunning;

    public bool WasSuccessful => !IsIgnored && _buildDefinition.WasSuccessful;

    public bool HasFailed
    {
        get
        {
            if (IsIgnored)
            {
                return false;
            }

            return _buildDefinition.HasFailed;
        }
    }

    public string Name => _buildDefinition.Name;

    private void RefreshBuilds()
    {
        Builds.Clear();

        foreach (var build in _buildDefinition.Builds)
        {
            Builds.Add(new BuildViewModel(_browserService, build));
        }
    }
}