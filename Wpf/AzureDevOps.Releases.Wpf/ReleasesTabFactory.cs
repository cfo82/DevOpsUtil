namespace DevOpsUtil.AzureDevOps.Releases.Wpf;

using System.Windows.Controls;
using DevOpsUtil.AzureDevOps.Releases.Contracts;
using DevOpsUtil.AzureDevOps.Releases.Services;
using DevOpsUtil.AzureDevOps.Releases.Wpf.ViewModels;
using DevOpsUtil.AzureDevOps.Releases.Wpf.Views;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Wpf.Core;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;

public class ReleasesTabFactory : ITabFactory
{
    private readonly IContainerProvider _containerProvider;

    public ReleasesTabFactory(IContainerProvider containerProvider)
    {
        _containerProvider = containerProvider;
    }

    public UserControl CreateTab(string regionName, string title, IConfigurationSection configurationSection)
    {
        var settings = ReleasesTabSettings.Load(configurationSection);
        var releasesServiceSettings = new ReleasesServiceSettings
        {
            AzureDevOpsSettingsKey = settings.AzureDevOpsSettingsKey,
            ReleaseDefinitionstoIgnore = settings.ReleaseDefinitionstoIgnore,
        };
        return new ReleasesView(new ReleasesViewModel(
            _containerProvider.Resolve<IUiDispatcherService>(),
            _containerProvider.Resolve<IBrowserService>(),
            _containerProvider.Resolve<IReleasesService>((typeof(IReleasesServiceSettings), releasesServiceSettings)),
            title));
    }
}