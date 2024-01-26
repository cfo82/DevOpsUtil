namespace DevOpsUtil.Gitlab.Pipelines.Avalonia;

using DevOpsUtil.Avalonia.Core;
using DevOpsUtil.Gitlab.Pipelines.Avalonia.ViewModels;
using DevOpsUtil.Gitlab.Pipelines.Avalonia.Views;
using DevOpsUtil.Gitlab.Pipelines.Contracts;
using DevOpsUtil.Gitlab.Pipelines.Services;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

public class GitlabPipelinesModule : IModule
{
    private readonly IRegionManager _regionManager;

    public GitlabPipelinesModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<IPipelinesService, PipelinesService>();
        containerRegistry.Register<ITabFactory, PipelinesTabFactory>("Gitlab.Pipelines");

        containerRegistry.RegisterForNavigation<PipelinesView, PipelinesViewModel>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        // _regionManager.RegisterViewWithRegion("Region_DevOpsPages", typeof(PipelinesView));
    }
}