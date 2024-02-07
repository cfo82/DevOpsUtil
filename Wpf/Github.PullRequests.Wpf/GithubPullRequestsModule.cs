namespace DevOpsUtil.Github.PullRequests.Wpf;

using DevOpsUtil.Github.PullRequests.Contracts;
using DevOpsUtil.Github.PullRequests.Services;
using DevOpsUtil.Github.PullRequests.Wpf.ViewModels;
using DevOpsUtil.Github.PullRequests.Wpf.Views;
using DevOpsUtil.Wpf.Core;
using Prism.Ioc;
using Prism.Modularity;

public class GithubPullRequestsModule : IModule
{
    public GithubPullRequestsModule()
    {
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<IPullRequestService, PullRequestService>();
        containerRegistry.Register<ITabFactory, PullRequestTabFactory>("Github.PullRequests");

        containerRegistry.RegisterForNavigation<PullRequestsView, PullRequestsViewModel>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
    }
}