namespace DevOpsUtil.Gitlab.MergeRequests.Wpf.Views;

using System.Windows.Navigation;
using DevOpsUtil.Gitlab.MergeRequests.Wpf.ViewModels;

public partial class MergeRequestsView
{
    public MergeRequestsView(MergeRequestsViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }

    private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ((MergeRequestsViewModel)DataContext).MergeRequests.First().OpenUrlCommand.Execute(e.Uri.ToString());
    }
}