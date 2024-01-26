namespace DevOpsUtil.Gitlab.Pipelines.Wpf.Views;

using DevOpsUtil.Gitlab.Pipelines.Wpf.ViewModels;

public partial class PipelinesView
{
    public PipelinesView(PipelinesViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}