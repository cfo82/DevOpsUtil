namespace DevOpsUtil.AzureDevOps.Releases.Wpf;

using DevOpsUtil.AzureDevOps.Releases.Contracts;
using DevOpsUtil.AzureDevOps.Releases.Services;
using DevOpsUtil.AzureDevOps.Releases.Wpf.ViewModels;
using DevOpsUtil.AzureDevOps.Releases.Wpf.Views;
using DevOpsUtil.Wpf.Core;
using Prism.Ioc;
using Prism.Modularity;

public class AzureDevOpsReleasesModule : IModule
{
    public AzureDevOpsReleasesModule()
    {
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<IReleasesService, ReleasesService>();
        containerRegistry.Register<ITabFactory, ReleasesTabFactory>("AzureDevOps.Releases");

        containerRegistry.RegisterForNavigation<ReleasesView, ReleasesViewModel>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
    }
}