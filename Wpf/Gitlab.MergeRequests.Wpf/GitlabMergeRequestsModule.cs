namespace DevOpsUtil.Gitlab.MergeRequests.Wpf;

using DevOpsUtil.Gitlab.MergeRequests.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.Services;
using DevOpsUtil.Gitlab.MergeRequests.Wpf.ViewModels;
using DevOpsUtil.Gitlab.MergeRequests.Wpf.Views;
using DevOpsUtil.Wpf.Core;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

public class GitlabMergeRequestsModule : IModule
{
    private readonly IRegionManager _regionManager;

    public GitlabMergeRequestsModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<IMergeRequestService, MergeRequestService>();
        containerRegistry.Register<ITabFactory, MergeRequestTabFactory>("Gitlab.MergeRequests");

        containerRegistry.RegisterForNavigation<MergeRequestsView, MergeRequestsViewModel>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        // _regionManager.RegisterViewWithRegion("Region_DevOpsPages", typeof(MergeRequestsView));
    }
}