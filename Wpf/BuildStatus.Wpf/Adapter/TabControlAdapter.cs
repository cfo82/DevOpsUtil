namespace DevOpsUtil.BuildStatus.Wpf.Adapter;

using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using Prism.Regions;

public class TabControlAdapter : RegionAdapterBase<TabControl>
{
    public TabControlAdapter(IRegionBehaviorFactory regionBehaviorFactory)
        : base(regionBehaviorFactory)
    {
    }

    protected override void Adapt(IRegion region, TabControl regionTarget)
    {
        region.Views.CollectionChanged += (s, e) =>
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
            {
                foreach (UserControl item in e.NewItems)
                {
                    var tabItem = new TabItem
                    {
                        Content = item,
                        DataContext = item.DataContext,
                    };
                    var binding = new Binding("Header");
                    tabItem.SetBinding(HeaderedContentControl.HeaderProperty, binding);
                    regionTarget.Items.Add(tabItem);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    var tabToDelete = regionTarget.Items.OfType<TabItem>().First(n => n.Content == item);
                    regionTarget.Items.Remove(tabToDelete);
                }
            }
        };
    }

    protected override IRegion CreateRegion()
    {
        return new SingleActiveRegion();
    }
}