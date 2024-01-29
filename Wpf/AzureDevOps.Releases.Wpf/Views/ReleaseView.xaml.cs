namespace DevOpsUtil.AzureDevOps.Releases.Wpf.Views;

using System.Windows.Controls;
using System.Windows.Navigation;
using DevOpsUtil.AzureDevOps.Releases.Wpf.ViewModels;

public partial class ReleaseView : UserControl
{
    public ReleaseView()
    {
        InitializeComponent();
    }

    private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ((ReleaseViewModel)DataContext).OpenUrlCommand.Execute(e.Uri);
    }
}