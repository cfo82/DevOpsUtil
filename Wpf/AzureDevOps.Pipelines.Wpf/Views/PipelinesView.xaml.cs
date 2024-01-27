namespace DevOpsUtil.AzureDevOps.Pipelines.Wpf.Views;

using DevOpsUtil.AzureDevOps.Pipelines.Wpf.ViewModels;

public partial class PipelinesView
{
    public PipelinesView(PipelinesViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}