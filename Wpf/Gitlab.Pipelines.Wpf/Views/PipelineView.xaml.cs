namespace DevOpsUtil.Gitlab.Pipelines.Wpf.Views;

using System.Windows.Controls;
using System.Windows.Navigation;
using DevOpsUtil.Gitlab.Pipelines.Wpf.ViewModels;

public partial class PipelineView : UserControl
{
    public PipelineView()
    {
        InitializeComponent();
    }

    private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        ((PipelineViewModel)DataContext).OpenUrlCommand.Execute(e.Uri);
    }
}