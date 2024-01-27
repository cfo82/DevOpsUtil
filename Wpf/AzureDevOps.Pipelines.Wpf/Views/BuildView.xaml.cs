namespace DevOpsUtil.AzureDevOps.Pipelines.Wpf.Views;

using System.Windows.Controls;
using System.Windows.Navigation;
using DevOpsUtil.AzureDevOps.Pipelines.Wpf.ViewModels;

public partial class BuildView : UserControl
{
    public BuildView()
    {
        InitializeComponent();
    }

    private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ((BuildViewModel)DataContext).OpenUrlCommand.Execute(e.Uri);
    }
}