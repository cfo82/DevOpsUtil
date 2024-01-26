namespace DevOpsUtil.Avalonia.Core;

using System;
using Microsoft.Extensions.Configuration;

/// <summary>
/// General settings that apply to each tab.
/// </summary>
public class TabSettings
{
    /// <summary>
    /// Gets the location within the appsettings.json where the configuration is to be located.
    /// </summary>
    public const string Location = "Tabs";

    /// <summary>
    /// Gets or sets the type of the tab.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the title to be used for the tab.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Load tab settings from an <see cref="IConfigurationSection"/>.
    /// </summary>
    /// <param name="section">The section to load the settings from.</param>
    /// <returns>An instance of TabSettings that contains the parsed settings.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the settings can't be loaded.</exception>
    public static TabSettings Load(IConfigurationSection section)
    {
        return section.Get<TabSettings>() ??
               throw new InvalidOperationException("Unable to read tab settings.");
    }
}