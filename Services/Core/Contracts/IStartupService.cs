namespace DevOpsUtil.Core.Contracts;

public interface IStartupService
{
    bool IsActiveOnStartup { get; set; }

    void InstallActiveOnStartup();

    void UninstallActiveOnStartup();
}