namespace DevOpsUtil.Core.Windows.Services;

using System.Reflection;
using DevOpsUtil.Core.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

public class StartupService : IStartupService
{
    private readonly ILogger<StartupService> _logger;
    private bool _isActiveOnStartup;

    public StartupService(ILogger<StartupService> logger)
    {
        _logger = logger;
        _isActiveOnStartup = CheckActiveOnStartup();
    }

    public bool IsActiveOnStartup
    {
        get => _isActiveOnStartup;
        set
        {
            if (_isActiveOnStartup != value)
            {
                if (value)
                {
                    InstallActiveOnStartup();
                }
                else
                {
                    UninstallActiveOnStartup();
                }

                _isActiveOnStartup = CheckActiveOnStartup();
            }
        }
    }

    private string EntryAssemblyName
    {
        get
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            return currentAssembly?.GetName().Name
                   ?? throw new InvalidOperationException("Unable to evaluate the assembly name.");
        }
    }

    private string EntryAssemblyLocation
    {
        get
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var location = currentAssembly?.Location
                   ?? throw new InvalidOperationException("Unable to evaluate the assembly name.");
            return Path.ChangeExtension(location, "exe");
        }
    }

    public void InstallActiveOnStartup()
    {
        using var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        key?.SetValue(EntryAssemblyName, EntryAssemblyLocation);
    }

    public void UninstallActiveOnStartup()
    {
        using var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        key?.DeleteValue(EntryAssemblyName);
    }

    private bool CheckActiveOnStartup()
    {
        try
        {
            using var key =
                Registry.CurrentUser.OpenSubKey(
                    "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",
                    false);
            if (key != null)
            {
                _logger.LogInformation($"Checking for startup with {EntryAssemblyName}:{EntryAssemblyLocation}.");
                var value = key.GetValue(EntryAssemblyName);
                return Equals(value, EntryAssemblyLocation);
            }

            return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to verify if this app is active on startup!");
            return false;
        }
    }
}