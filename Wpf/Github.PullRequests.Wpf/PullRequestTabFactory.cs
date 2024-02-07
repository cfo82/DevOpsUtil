namespace DevOpsUtil.Github.PullRequests.Wpf;

using System.Windows.Controls;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Github.Core.Contracts;
using DevOpsUtil.Github.PullRequests.Contracts;
using DevOpsUtil.Github.PullRequests.Services;
using DevOpsUtil.Github.PullRequests.Wpf.ViewModels;
using DevOpsUtil.Github.PullRequests.Wpf.Views;
using DevOpsUtil.Wpf.Core;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;

public class PullRequestTabFactory : ITabFactory
{
    private readonly IContainerProvider _containerProvider;

    public PullRequestTabFactory(IContainerProvider containerProvider)
    {
        _containerProvider = containerProvider;
    }

    public UserControl CreateTab(string regionName, string title, IConfigurationSection configurationSection)
    {
        var settings = PullRequestsTabSettings.Load(configurationSection);
        return new PullRequestsView(new PullRequestsViewModel(
            _containerProvider.Resolve<IBrowserService>(),
            _containerProvider.Resolve<IGithubSettingsService>(),
            _containerProvider.Resolve<IPullRequestService>((typeof(IPullRequestServiceSettings),
                new PullRequestServiceSettings { GithubSettingsKey = settings.GithubSettingsKey })),
            title,
            settings));
    }
}