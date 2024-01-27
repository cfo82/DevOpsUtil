namespace DevOpsUtil.AzureDevOps.Pipelines.Wpf.ViewModels;

using System.Collections.ObjectModel;
using DevOpsUtil.AzureDevOps.Pipelines.Contracts;
using DevOpsUtil.Core.Contracts;
using Prism.Mvvm;

public class PipelinesViewModel : BindableBase
{
    private readonly IUiDispatcherService _uiDispatcherService;
    private readonly IBrowserService _browserService;
    private readonly IPipelinesService _pipelinesService;

    public PipelinesViewModel(
        IUiDispatcherService uiDispatcherService,
        IBrowserService browserService,
        IPipelinesService pipelinesService,
        string title)
    {
        _uiDispatcherService = uiDispatcherService;
        _browserService = browserService;
        _pipelinesService = pipelinesService;
        Header = title;

        BuildDefinitions = new ObservableCollection<BuildDefinitionViewModel>();

        _pipelinesService.BuildDefinitionsChanged += (_, _) => uiDispatcherService.Post(OnBuildDefinitionsChanged);
    }

    public string Header { get; }

    public ObservableCollection<BuildDefinitionViewModel> BuildDefinitions { get; private set; }

    private void OnBuildDefinitionsChanged()
    {
        var buildDefinitions = _pipelinesService.BuildDefinitions;

        for (int i = 0; i < buildDefinitions.Length; ++i)
        {
            if (i < BuildDefinitions.Count)
            {
                BuildDefinitions[i].BuildDefinition = buildDefinitions[i];
            }
            else
            {
                BuildDefinitions.Add(new BuildDefinitionViewModel(_browserService, buildDefinitions[i]));
            }
        }

        while (BuildDefinitions.Count > buildDefinitions.Length)
        {
            BuildDefinitions.RemoveAt(BuildDefinitions.Count - 1);
        }

        SortBuildDefinitions();
    }

    /// <summary>
    /// Sort all the definitions and update the taskbar item and icon.
    /// </summary>
    private void SortBuildDefinitions()
    {
        var projectList = BuildDefinitions.Select(definition => definition.BuildDefinition).ToList();

        projectList.Sort((a, b) =>
        {
            if (!a.IsIgnored && b.IsIgnored)
            {
                return -1;
            }

            if (a.IsIgnored && !b.IsIgnored)
            {
                return 1;
            }

            if (a.Builds.Length > 0 && b.Builds.Length == 0)
            {
                return -1;
            }

            if (a.Builds.Length == 0 && b.Builds.Length > 0)
            {
                return 1;
            }

            if (a.IsRunning && !b.IsRunning)
            {
                return -1;
            }

            if (!a.IsRunning && b.IsRunning)
            {
                return 1;
            }

            if (a.HasFailed && !b.HasFailed)
            {
                return -1;
            }

            if (!a.HasFailed && b.HasFailed)
            {
                return 1;
            }

            return string.Compare(a.Name, b.Name, StringComparison.Ordinal);
        });

        for (var i = 0; i < BuildDefinitions.Count; ++i)
        {
            BuildDefinitions[i].BuildDefinition = projectList[i];
        }
    }
}