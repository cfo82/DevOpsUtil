namespace DevOpsUtil.AzureDevOps.Releases.Wpf.Views;

using DevOpsUtil.AzureDevOps.Releases.Wpf.ViewModels;

public partial class ReleasesView
{
    public ReleasesView(ReleasesViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}