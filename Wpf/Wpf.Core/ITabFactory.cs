namespace DevOpsUtil.Wpf.Core;

using System.Windows.Controls;
using Microsoft.Extensions.Configuration;

public interface ITabFactory
{
    UserControl CreateTab(string regionName, string title, IConfigurationSection configurationSection);
}