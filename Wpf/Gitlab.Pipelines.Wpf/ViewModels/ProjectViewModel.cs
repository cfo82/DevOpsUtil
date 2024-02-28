namespace DevOpsUtil.Gitlab.Pipelines.Wpf.ViewModels;

using System.Collections.ObjectModel;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Gitlab.Pipelines.Contracts;
using Prism.Mvvm;

public class ProjectViewModel : BindableBase
{
    private readonly IBrowserService _browserService;
    private IProject _project;

    public ProjectViewModel(IBrowserService browserService, IProject project)
    {
        _browserService = browserService;

        _project = project;

        Pipelines = new ObservableCollection<PipelineViewModel>();

        RefreshPipelines();
    }

    public ObservableCollection<PipelineViewModel> Pipelines { get; }

    public IProject Project
    {
        get => _project;
        set
        {
            if (_project != value)
            {
                _project = value;

                RefreshPipelines();

                RaisePropertyChanged(string.Empty);
            }
        }
    }

    public bool IsIgnored => _project.IsIgnored;

    public bool IsRunning => !IsIgnored && _project.IsRunning;

    public bool WasSuccessful => !IsIgnored && _project.WasSuccessful && !HasFailed;

    public bool IsArchived => _project.IsArchived;

    public bool HasFailed
    {
        get
        {
            if (IsIgnored)
            {
                return false;
            }

            return _project.HasFailed || (!IsArchived && Pipelines.Count == 0);
        }
    }

    public string Name => _project.Name;

    private void RefreshPipelines()
    {
        Pipelines.Clear();

        foreach (var pipeline in _project.Pipelines)
        {
            Pipelines.Add(new PipelineViewModel(_browserService, pipeline));
        }
    }
}