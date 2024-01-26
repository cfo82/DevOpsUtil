namespace DevOpsUtil.Wpf.Core;

using Microsoft.Extensions.Configuration;

public class TabSettings
{
    public const string Location = "Tabs";

    public string Type { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public static TabSettings Load(IConfigurationSection section)
    {
        return section.Get<TabSettings>() ??
               throw new InvalidOperationException("Unable to read tab settings.");
    }
}