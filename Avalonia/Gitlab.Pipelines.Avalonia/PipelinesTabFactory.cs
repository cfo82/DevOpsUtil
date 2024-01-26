namespace DevOpsUtil.Gitlab.Pipelines.Avalonia;

using DevOpsUtil.Avalonia.Core;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Gitlab.Pipelines.Avalonia.ViewModels;
using DevOpsUtil.Gitlab.Pipelines.Avalonia.Views;
using DevOpsUtil.Gitlab.Pipelines.Contracts;
using DevOpsUtil.Gitlab.Pipelines.Services;
using global::Avalonia.Controls;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;

public class PipelinesTabFactory : ITabFactory
{
    private readonly IContainerProvider _containerProvider;

    public PipelinesTabFactory(IContainerProvider containerProvider)
    {
        _containerProvider = containerProvider;
    }

    public UserControl CreateTab(string title, IConfigurationSection configurationSection)
    {
        var settings = PipelinesTabSettings.Load(configurationSection);
        var pipelinesServiceSettings = new PipelinesServiceSettings
        {
            GitlabSettingsKey = settings.GitlabSettings,
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