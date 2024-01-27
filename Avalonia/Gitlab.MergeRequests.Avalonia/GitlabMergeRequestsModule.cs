namespace DevOpsUtil.Gitlab.MergeRequests.Avalonia;

using DevOpsUtil.Avalonia.Core;
using DevOpsUtil.Gitlab.MergeRequests.Avalonia.ViewModels;
using DevOpsUtil.Gitlab.MergeRequests.Avalonia.Views;
using DevOpsUtil.Gitlab.MergeRequests.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.Services;
using Prism.Ioc;
using Prism.Modularity;

public class GitlabMergeRequestsModule : IModule
{
    public GitlabMergeRequestsModule()
    {
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<IMergeRequestService, MergeRequestService>();
        containerRegistry.Register<ITabFactory, MergeRequestTabFactory>("Gitlab.MergeRequests");

        containerRegistry.RegisterForNavigation<MergeRequestsView, MergeRequestsViewModel>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
    }
}