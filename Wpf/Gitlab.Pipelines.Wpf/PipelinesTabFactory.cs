namespace DevOpsUtil.Gitlab.Pipelines.Wpf;

using System.Windows.Controls;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Gitlab.Pipelines.Contracts;
using DevOpsUtil.Gitlab.Pipelines.Services;
using DevOpsUtil.Gitlab.Pipelines.Wpf.ViewModels;
using DevOpsUtil.Gitlab.Pipelines.Wpf.Views;
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
            GitlabSettingsKey = settings.GitlabSettingsKey,
            ProjectsToIgnore = settings.ProjectsToIgnore,
            BranchesToWatch = settings.BranchesToWatch,
        };
        return new PipelinesView(new PipelinesViewModel(
            _containerProvider.Resolve<IUiDispatcherService>(),
            _containerProvider.Resolve<IBrowserService>(),
            _containerProvider.Resolve<IPipelinesService>((typeof(IPipelinesServiceSettings), pipelinesServiceSettings)),
            title));
    }
}