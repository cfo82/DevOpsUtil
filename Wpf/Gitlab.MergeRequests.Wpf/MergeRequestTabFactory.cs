namespace DevOpsUtil.Gitlab.MergeRequests.Wpf;

using System.Windows.Controls;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Gitlab.Core.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.Services;
using DevOpsUtil.Gitlab.MergeRequests.Wpf.ViewModels;
using DevOpsUtil.Gitlab.MergeRequests.Wpf.Views;
using DevOpsUtil.Wpf.Core;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;

public class MergeRequestTabFactory : ITabFactory
{
    private readonly IContainerProvider _containerProvider;

    public MergeRequestTabFactory(IContainerProvider containerProvider)
    {
        _containerProvider = containerProvider;
    }

    public UserControl CreateTab(string regionName, string title, IConfigurationSection configurationSection)
    {
        var settings = MergeRequestsTabSettings.Load(configurationSection);
        return new MergeRequestsView(new MergeRequestsViewModel(
            _containerProvider.Resolve<IBrowserService>(),
            _containerProvider.Resolve<IGitlabSettingsService>(),
            _containerProvider.Resolve<IMergeRequestService>((typeof(IMergeRequestServiceSettings),
                new MergeRequestServiceSettings { GitlabSettingsKey = settings.GitlabSettings })),
            title,
            settings));
    }
}