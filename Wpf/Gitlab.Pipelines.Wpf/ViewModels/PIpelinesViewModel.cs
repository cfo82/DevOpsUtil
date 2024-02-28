namespace DevOpsUtil.Gitlab.Pipelines.Wpf.ViewModels;

using System.Collections.ObjectModel;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Gitlab.Pipelines.Contracts;
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

        Projects = new ObservableCollection<ProjectViewModel>();

        _pipelinesService.ProjectsChanged += (_, _) => uiDispatcherService.Post(OnDefinitionsChanged);
    }

    public string Header { get; }

    public ObservableCollection<ProjectViewModel> Projects { get; private set; }

    private void OnDefinitionsChanged()
    {
        var projects = _pipelinesService.Projects;

        for (int i = 0; i < projects.Length; ++i)
        {
            if (i < Projects.Count)
            {
                Projects[i].Project = projects[i];
            }
            else
            {
                Projects.Add(new ProjectViewModel(_browserService, projects[i]));
            }
        }

        while (Projects.Count > projects.Length)
        {
            Projects.RemoveAt(Projects.Count - 1);
        }

        SortProjects();
    }

    /// <summary>
    /// Sort all the definitions and update the taskbar item and icon.
    /// </summary>
    private void SortProjects()
    {
        var projectList = Projects.Select(definition => definition.Project).ToList();

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

            if (!a.IsArchived && b.IsArchived)
            {
                return -1;
            }

            if (a.IsArchived && !b.IsArchived)
            {
                return 1;
            }

            if (a.Pipelines.Length > 0 && b.Pipelines.Length == 0)
            {
                return -1;
            }

            if (a.Pipelines.Length == 0 && b.Pipelines.Length > 0)
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

        for (var i = 0; i < Projects.Count; ++i)
        {
            Projects[i].Project = projectList[i];
        }
    }
}