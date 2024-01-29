namespace DevOpsUtil.AzureDevOps.Releases.Wpf.ViewModels;

using DevOpsUtil.AzureDevOps.Releases.Contracts;
using Prism.Mvvm;

public class EnvironmentViewModel : BindableBase
{
    private readonly IEnvironment _environment;

    public EnvironmentViewModel(IEnvironment environment, bool isNotLast)
    {
        _environment = environment;
        IsNotLast = isNotLast;
    }

    public string Name => _environment.Name;

    public bool IsRunning => _environment.IsRunning;

    public bool WasSuccessful => _environment.WasSuccessful;

    public bool HasFailed => _environment.HasFailed;

    public bool IsNotLast { get; }
}