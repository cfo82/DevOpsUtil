namespace DevOpsUtil.BuildStatus.Avalonia.Adapter;

using System.Collections.Specialized;
using System.Linq;
using global::Avalonia.Controls;
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
                    regionTarget.Items.Add(new TabItem { Header = item.Name, Content = item });
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