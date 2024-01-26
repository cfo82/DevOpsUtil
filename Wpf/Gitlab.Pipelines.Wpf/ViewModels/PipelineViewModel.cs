namespace DevOpsUtil.Gitlab.Pipelines.Wpf.ViewModels;

using System.Collections.ObjectModel;
using System.Windows.Input;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Gitlab.Pipelines.Contracts;
using Prism.Commands;
using Prism.Mvvm;

public class PipelineViewModel : BindableBase
{
    private const int NumberOfDaysUntilTooOld = 14;

    private readonly IBrowserService _browserService;
    private readonly IPipeline _pipeline;

    public PipelineViewModel(IBrowserService browserService, IPipeline pipeline)
    {
        _browserService = browserService;
        _pipeline = pipeline;

        Jobs = new ObservableCollection<JobViewModel>();
        RefreshJobs();
        OpenUrlCommand = new DelegateCommand<Uri?>(OpenUrl);
    }

    public ObservableCollection<JobViewModel> Jobs { get; }

    public ICommand OpenUrlCommand { get; }

    public bool IsOld => _pipeline.Age > NumberOfDaysUntilTooOld;

    public bool IsNotOld => !IsOld;

    public int Age => _pipeline.Age;

    public Uri Uri => _pipeline.Uri;

    public string Ref => _pipeline.Ref;

    public string StatusText => _pipeline.StatusText;

    private void OpenUrl(Uri? url)
    {
        if (url != null)
        {
            _browserService.OpenUrl(url);
        }
    }

    private void RefreshJobs()
    {
        Jobs.Clear();

        foreach (var job in _pipeline.Jobs)
        {
            Jobs.Add(new JobViewModel(job, job == _pipeline.Jobs[^1]));
        }
    }
}