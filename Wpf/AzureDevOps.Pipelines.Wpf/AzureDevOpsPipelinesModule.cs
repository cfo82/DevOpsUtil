namespace DevOpsUtil.AzureDevOps.Pipelines.Wpf;

using DevOpsUtil.AzureDevOps.Pipelines.Contracts;
using DevOpsUtil.AzureDevOps.Pipelines.Services;
using DevOpsUtil.AzureDevOps.Pipelines.Wpf.ViewModels;
using DevOpsUtil.AzureDevOps.Pipelines.Wpf.Views;
using DevOpsUtil.Wpf.Core;
using Prism.Ioc;
using Prism.Modularity;

public class AzureDevOpsPipelinesModule : IModule
{
    public AzureDevOpsPipelinesModule()
    {
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<IPipelinesService, PipelinesService>();
        containerRegistry.Register<ITabFactory, PipelinesTabFactory>("AzureDevOps.Pipelines");

        containerRegistry.RegisterForNavigation<PipelinesView, PipelinesViewModel>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
    }
}