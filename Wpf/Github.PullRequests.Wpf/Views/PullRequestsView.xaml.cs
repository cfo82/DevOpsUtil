namespace DevOpsUtil.Github.PullRequests.Wpf.Views;

using System.Windows.Navigation;
using DevOpsUtil.Github.PullRequests.Wpf.ViewModels;

public partial class PullRequestsView
{
    public PullRequestsView(PullRequestsViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }

    private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ((PullRequestsViewModel)DataContext).PullRequests.First().OpenUrlCommand.Execute(e.Uri.ToString());
    }
}