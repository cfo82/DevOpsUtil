namespace DevOpsUtil.BuildStatus.Wpf;

using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DevOpsUtil.BuildStatus.Wpf.Adapter;
using DevOpsUtil.BuildStatus.Wpf.Contracts;
using DevOpsUtil.BuildStatus.Wpf.Services;
using DevOpsUtil.BuildStatus.Wpf.ViewModels;
using DevOpsUtil.BuildStatus.Wpf.Views;
using DevOpsUtil.Core.Contracts;
using DevOpsUtil.Core.Services;
using DevOpsUtil.Core.Windows.Services;
using DevOpsUtil.Gitlab.Core.Contracts;
using DevOpsUtil.Gitlab.Core.Services;
using DevOpsUtil.Gitlab.MergeRequests.Wpf;
using DevOpsUtil.Gitlab.Pipelines.Wpf;
using DevOpsUtil.Wpf.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Serilog;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : PrismApplication
{
    /*

     TODO: Implement those exception handlers at appropriate locations

    // catch all exceptions and pass them to the error handler
    // https://stackoverflow.com/questions/1472498/wpf-global-exception-handler/1472562#1472562
    AppDomain.CurrentDomain.UnhandledException += (_, args) =>
    {
        var exception = (Exception) args.ExceptionObject;
        errorHandler.Error = exception;
        logService.WriteEntry($"Unhandled Exception caught. Process is {(args.IsTerminating ? "" : " not")} terminating. Exception is {exception}");
    };
    TaskScheduler.UnobservedTaskException += (_, args) =>
    {
        errorHandler.Error = args.Exception;
        args.SetObserved();
        logService.WriteEntry($"TaskScheduler.UnobservedTaskException caught. Exception is {args.Exception}");
    };*/

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
        containerRegistry.RegisterInstance<ILoggerFactory>(loggerFactory);
        containerRegistry.Register(typeof(ILogger<>), typeof(Logger<>));
        containerRegistry.RegisterSingleton<IRefreshService, RefreshService>();
        containerRegistry.RegisterSingleton<IErrorStateService, ErrorStateService>();
        containerRegistry.RegisterSingleton<IStartupService, StartupService>();
        containerRegistry.Register<IUiDispatcherService, UiDispatcherService>();
        containerRegistry.RegisterSingleton<IErrorHandler, ErrorHandler>();
        containerRegistry.Register<IBrowserService, BrowserService>();
        containerRegistry.RegisterSingleton<IGitlabSettingsService, GitlabSettingsService>();
        containerRegistry.RegisterSingleton<ITaskbarIconService, TaskbarIconService>();

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

    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

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
            var tab = tabFactory.CreateTab(string.Empty, settings.Title, section);
            tabRegion.Add(tab);
        }
    }
}