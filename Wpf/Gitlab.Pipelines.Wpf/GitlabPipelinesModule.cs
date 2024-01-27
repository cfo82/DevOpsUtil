namespace DevOpsUtil.Gitlab.Pipelines.Wpf;

using DevOpsUtil.Gitlab.Pipelines.Contracts;
using DevOpsUtil.Gitlab.Pipelines.Services;
using DevOpsUtil.Gitlab.Pipelines.Wpf.ViewModels;
using DevOpsUtil.Gitlab.Pipelines.Wpf.Views;
using DevOpsUtil.Wpf.Core;
using Prism.Ioc;
using Prism.Modularity;

public class GitlabPipelinesModule : IModule
{
    public GitlabPipelinesModule()
    {
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
    }
}