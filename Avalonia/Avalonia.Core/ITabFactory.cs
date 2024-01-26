namespace DevOpsUtil.Avalonia.Core;

using global::Avalonia.Controls;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Factory class to create the main tabs in the UI.
/// </summary>
public interface ITabFactory
{
    /// <summary>
    /// Create a new tab.
    /// </summary>
    /// <param name="title">Title for the new tab.</param>
    /// <param name="configurationSection">The configuration section containing the tabs configuration.</param>
    /// <returns>An instance of the new tab.</returns>
    UserControl CreateTab(string title, IConfigurationSection configurationSection);
}