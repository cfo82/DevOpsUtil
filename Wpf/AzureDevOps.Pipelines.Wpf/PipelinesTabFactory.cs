namespace DevOpsUtil.AzureDevOps.Pipelines.Wpf;

using System.Windows.Controls;
using DevOpsUtil.AzureDevOps.Pipelines.Contracts;
using DevOpsUtil.AzureDevOps.Pipelines.Services;
using DevOpsUtil.AzureDevOps.Pipelines.Wpf.ViewModels;
using DevOpsUtil.AzureDevOps.Pipelines.Wpf.Views;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Wpf.Core;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;

public class PipelinesTabFactory : ITabFactory
{
    private readonly IContainerProvider _containerProvider;

    public PipelinesTabFactory(IContainerProvider containerProvider)
    {
        _containerProvider = containerProvider;
    }

    public UserControl CreateTab(string regionName, string title, IConfigurationSection configurationSection)
    {
        var settings = PipelinesTabSettings.Load(configurationSection);
        var pipelinesServiceSettings = new PipelinesServiceSettings
        {
            AzureDevOpsSettingsKey = settings.AzureDevOpsSettingsKey,
            BuildDefinitionstoIgnore = settings.BuildDefinitionsToIgnore,
            BranchesToWatch = settings.BranchesToWatch,
        };
        return new PipelinesView(new PipelinesViewModel(
            _containerProvider.Resolve<IUiDispatcherService>(),
            _containerProvider.Resolve<IBrowserService>(),
            _containerProvider.Resolve<IPipelinesService>((typeof(IPipelinesServiceSettings), pipelinesServiceSettings)),
            title));
    }
}