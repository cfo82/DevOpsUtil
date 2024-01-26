namespace DevOpsUtil.Gitlab.Pipelines.Wpf;

using DevOpsUtil.Gitlab.Pipelines.Contracts;
using DevOpsUtil.Gitlab.Pipelines.Services;
using DevOpsUtil.Gitlab.Pipelines.Wpf.ViewModels;
using DevOpsUtil.Gitlab.Pipelines.Wpf.Views;
using DevOpsUtil.Wpf.Core;
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
        containerRegistry.RegisterForNavigation<ProjectView, ProjectViewModel>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        _regionManager.RegisterViewWithRegion("Region_Gitlab_Pipelines_Projects", typeof(ProjectView));
        // _regionManager.RegisterViewWithRegion("Region_DevOpsPages", typeof(PipelinesView));
    }
}