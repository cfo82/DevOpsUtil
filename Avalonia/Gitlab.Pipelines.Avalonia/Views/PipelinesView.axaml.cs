namespace DevOpsUtil.Gitlab.Pipelines.Avalonia.Views;

using DevOpsUtil.Gitlab.Pipelines.Avalonia.ViewModels;
using global::Avalonia.Controls;

public partial class PipelinesView : UserControl
{
    public PipelinesView(PipelinesViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}