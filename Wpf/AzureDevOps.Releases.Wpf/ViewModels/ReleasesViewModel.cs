namespace DevOpsUtil.AzureDevOps.Releases.Wpf.ViewModels;

using System.Collections.ObjectModel;
using DevOpsUtil.AzureDevOps.Releases.Contracts;
using DevOpsUtil.Core.Contracts;
using Prism.Mvvm;

public class ReleasesViewModel : BindableBase
{
    private readonly IBrowserService _browserService;
    private readonly IReleasesService _releasesService;

    public ReleasesViewModel(
        IUiDispatcherService uiDispatcherService,
        IBrowserService browserService,
        IReleasesService releasesService,
        string title)
    {
        _browserService = browserService;
        _releasesService = releasesService;
        Header = title;

        ReleaseDefinitions = new ObservableCollection<ReleaseDefinitionViewModel>();

        _releasesService.ReleaseDefinitionsChanged += (_, _) => uiDispatcherService.Post(OnReleaseDefinitionsChanged);
    }

    public string Header { get; }

    public ObservableCollection<ReleaseDefinitionViewModel> ReleaseDefinitions { get; private set; }

    private void OnReleaseDefinitionsChanged()
    {
        var releaseDefinitions = _releasesService.ReleaseDefinitions;

        for (int i = 0; i < releaseDefinitions.Length; ++i)
        {
            if (i < ReleaseDefinitions.Count)
            {
                ReleaseDefinitions[i].ReleaseDefinition = releaseDefinitions[i];
            }
            else
            {
                ReleaseDefinitions.Add(new ReleaseDefinitionViewModel(_browserService, releaseDefinitions[i]));
            }
        }

        while (ReleaseDefinitions.Count > releaseDefinitions.Length)
        {
            ReleaseDefinitions.RemoveAt(ReleaseDefinitions.Count - 1);
        }

        SortBuildDefinitions();
    }

    /// <summary>
    /// Sort all the definitions and update the taskbar item and icon.
    /// </summary>
    private void SortBuildDefinitions()
    {
        var projectList = ReleaseDefinitions.Select(definition => definition.ReleaseDefinition).ToList();

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

            /*if (a.Builds.Length > 0 && b.Builds.Length == 0)
            {
                return -1;
            }

            if (a.Builds.Length == 0 && b.Builds.Length > 0)
            {
                return 1;
            }*/

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

        for (var i = 0; i < ReleaseDefinitions.Count; ++i)
        {
            ReleaseDefinitions[i].ReleaseDefinition = projectList[i];
        }
    }
}