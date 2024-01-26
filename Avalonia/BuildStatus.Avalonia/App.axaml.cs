namespace DevOpsUtil.BuildStatus.Avalonia;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using DevOpsUtil.Avalonia.Core;
using DevOpsUtil.BuildStatus.Avalonia.Adapter;
using DevOpsUtil.BuildStatus.Avalonia.Services;
using DevOpsUtil.BuildStatus.Avalonia.ViewModels;
using DevOpsUtil.BuildStatus.Avalonia.Views;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Core.Services;
using DevOpsUtil.Gitlab.Core.Contracts;
using DevOpsUtil.Gitlab.Core.Services;
using DevOpsUtil.Gitlab.MergeRequests.Avalonia;
using DevOpsUtil.Gitlab.Pipelines.Avalonia;
using global::Avalonia;
using global::Avalonia.Controls;
using global::Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Serilog;

public partial class App : PrismApplication
{
    public static bool IsSingleViewLifetime =>
        Environment.GetCommandLineArgs()
            .Any(a => a == "--fbdev" || a == "--drm");

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        base.Initialize();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                                throw new InvalidOperationException("Unable to find application directory.");

        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(assemblyDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var configuration = configurationBuilder.Build();

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        var loggerFactory = new LoggerFactory()
            .AddSerilog(logger);

        // Register Services
        containerRegistry.RegisterInstance<IConfiguration>(configuration);
        containerRegistry.RegisterInstance(loggerFactory);
        containerRegistry.Register(typeof(ILogger<>), typeof(Logger<>));
        containerRegistry.RegisterSingleton<IRefreshService, RefreshService>();
        containerRegistry.RegisterSingleton<IErrorStateService, ErrorStateService>();
        containerRegistry.Register<IUiDispatcherService, UiDispatcherService>();
        containerRegistry.RegisterSingleton<IErrorHandler, ErrorHandler>();
        containerRegistry.Register<IBrowserService, BrowserService>();
        containerRegistry.RegisterSingleton<IGitlabSettingsService, GitlabSettingsService>();

        // Views - Generic

        // Views - Region Navigation
        containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
        containerRegistry.RegisterForNavigation<ToolbarView, ToolbarViewModel>();
        containerRegistry.RegisterForNavigation<RefreshView, RefreshViewModel>();
    }

    protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
    {
        base.ConfigureRegionAdapterMappings(regionAdapterMappings);
        regionAdapterMappings.RegisterMapping(typeof(TabControl), Container.Resolve<TabControlAdapter>());
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        // Register modules
        moduleCatalog.AddModule<GitlabPipelinesModule>();
        moduleCatalog.AddModule<GitlabMergeRequestsModule>();

        base.ConfigureModuleCatalog(moduleCatalog);
    }

    protected override AvaloniaObject CreateShell()
    {
        /*if (IsSingleViewLifetime)
            return Container.Resolve<MainControl>(); // For Linux Framebuffer or DRM
        else*/
        return Container.Resolve<MainWindow>();
    }

    /// <summary>Called after <seealso cref="Initialize"/>.</summary>
    protected override void OnInitialized()
    {
        // Register initial Views to Region.
        var regionManager = Container.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion("Region_Toolbar", typeof(ToolbarView));
        regionManager.RegisterViewWithRegion("Region_Error", typeof(ErrorView));
        regionManager.RegisterViewWithRegion("Region_Refreshing", typeof(RefreshView));

        base.OnInitialized();

        var tabRegion = regionManager.Regions["Region_DevOpsPages"];

        var configuration = Container.Resolve<IConfiguration>();
        var tabSections = configuration.GetSection(TabSettings.Location).GetChildren();
        foreach (var section in tabSections)
        {
            var settings = TabSettings.Load(section);
            var tabFactory = Container.Resolve<ITabFactory>(settings.Type);
            var tab = tabFactory.CreateTab(settings.Title, section);
            tabRegion.Add(tab);
        }
    }
}