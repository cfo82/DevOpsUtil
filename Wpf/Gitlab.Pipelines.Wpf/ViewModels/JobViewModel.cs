namespace DevOpsUtil.Gitlab.Pipelines.Wpf.ViewModels;

using DevOpsUtil.Gitlab.Pipelines.Contracts;
using Prism.Mvvm;

public class JobViewModel : BindableBase
{
    private readonly IJob _job;
    private readonly bool _isLast;

    public JobViewModel(IJob job, bool isLast)
    {
        _job = job;
        _isLast = isLast;
    }

    public bool IsNotLast => !_isLast;

    public string Name => _job.Name;

    public bool IsRunning => _job.IsRunning;

    public bool WasSuccessful => _job.WasSuccessful;

    public bool HasFailed => _job.HasFailed;
}