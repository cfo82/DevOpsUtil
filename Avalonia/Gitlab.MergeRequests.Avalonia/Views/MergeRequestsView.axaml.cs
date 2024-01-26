namespace DevOpsUtil.Gitlab.MergeRequests.Avalonia.Views;

using DevOpsUtil.Gitlab.MergeRequests.Avalonia.ViewModels;
using global::Avalonia.Controls;

public partial class MergeRequestsView : UserControl
{
    public MergeRequestsView(MergeRequestsViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}