namespace DevOpsUtil.Gitlab.MergeRequests.Avalonia;

using DevOpsUtil.Avalonia.Core;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Gitlab.Core.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.Avalonia.ViewModels;
using DevOpsUtil.Gitlab.MergeRequests.Avalonia.Views;
using DevOpsUtil.Gitlab.MergeRequests.Contracts;
using DevOpsUtil.Gitlab.MergeRequests.Services;
using global::Avalonia.Controls;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;

public class MergeRequestTabFactory : ITabFactory
{
    private readonly IContainerProvider _containerProvider;

    public MergeRequestTabFactory(IContainerProvider containerProvider)
    {
        _containerProvider = containerProvider;
    }

    public UserControl CreateTab(string title, IConfigurationSection configurationSection)
    {
        var settings = MergeRequestsTabSettings.Load(configurationSection);
        return new MergeRequestsView(new MergeRequestsViewModel(
            _containerProvider.Resolve<IBrowserService>(),
            _containerProvider.Resolve<IGitlabSettingsService>(),
            _containerProvider.Resolve<IMergeRequestService>((typeof(IMergeRequestServiceSettings), new MergeRequestServiceSettings { GitlabSettingsKey = settings.GitlabSettingsKey })),
            title,
            settings));
    }
}