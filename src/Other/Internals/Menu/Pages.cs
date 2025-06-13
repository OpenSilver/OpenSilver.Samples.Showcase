using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OpenSilver.Samples.Showcase
{
    public static class Pages
    {
        static ObservableCollection<PageCategoryInfo> _allPagesAndCategories;
        static PageInfo _landingPageInfo;
        static PageInfo _searchPageInfo;

        public static ObservableCollection<PageCategoryInfo> AllPagesAndCategories
        {
            get
            {
                if (_allPagesAndCategories == null)
                {
                    _allPagesAndCategories = new ObservableCollection<PageCategoryInfo>
                    {
                        new PageCategoryInfo
                        {
                            Name = "XAML & UI",
                            Foreground = new SolidColorBrush(Color.FromRgb(85, 119, 240)),
                            Pages = new ObservableCollection<PageInfo>
                            {
                                new PageInfo { Name = "Controls", Path = "/XAML_Controls", IsVisibleInMenu = true },
                                new PageInfo { Name = "XAML Features", Path = "/XAML_Features", IsVisibleInMenu = true },
                                new PageInfo { Name = "Layout", Path = "/XAML_Layout", IsVisibleInMenu = true },
                                new PageInfo { Name = "JS Libs", Path = "/JS_Libs", IsVisibleInMenu = true },
                                new PageInfo { Name = "Charts", Path = "/Charts", IsVisibleInMenu = true },
                                new PageInfo { Name = "Icons", Path = "/Icons", IsVisibleInMenu = true }
                            }
                        },
                        new PageCategoryInfo
                        {
                            Name = "NON-UI",
                            Foreground = new SolidColorBrush(Color.FromRgb(205, 63, 186)),
                            Pages = new ObservableCollection<PageInfo>
                            {
                                new PageInfo { Name = "Client / Server", Path = "/Client_Server", IsVisibleInMenu = true },
                                new PageInfo { Name = ".NET Framework", Path = "/Net_Framework", IsVisibleInMenu = true },
                                new PageInfo { Name = "Native APIs", Path = "/Maui_Hybrid", IsVisibleInMenu = true }
                            }
                        },
                        new PageCategoryInfo
                        {
                            Name = "OTHER",
                            Foreground = new SolidColorBrush(Color.FromRgb(253, 163, 28)),
                            Pages = new ObservableCollection<PageInfo>
                            {
                                new PageInfo { Name = "Interop", Path = "/Interop_Samples", IsVisibleInMenu = true },
                                new PageInfo { Name = "Performance", Path = "/Performance", IsVisibleInMenu = true },
                                new PageInfo { Name = "Third-Party", Path = "/Third_Party", IsVisibleInMenu = true },
                                LandingPageInfo,
                                SearchPageInfo
                            }
                        }
                    };
                }

                return _allPagesAndCategories;
            }
        }

        public static IEnumerable<PageInfo> AllPages
        {
            get
            {
                return _allPagesAndCategories.SelectMany(c => c.Pages ?? Enumerable.Empty<PageInfo>());
            }
        }

        public static PageInfo LandingPageInfo
        {
            get
            {
                if (_landingPageInfo == null)
                {
                    _landingPageInfo = new PageInfo { Name = "Home", Path = "/Welcome", IsVisibleInMenu = false };
                }
                return _landingPageInfo;
            }
        }

        public static PageInfo SearchPageInfo
        {
            get
            {
                if (_searchPageInfo == null)
                {
                    _searchPageInfo = new PageInfo { Name = "Search", Path = "/Search", IsVisibleInMenu = false };
                }
                return _searchPageInfo;
            }
        }

        //public static async Task<bool> SelectPageInTreeView(TreeView treeView, PageInfo pageToSelect, bool searchInCollapsedNodesToo = false)
        //{
        //    await UIElementHelpers.WaitForLoadedAsync(treeView);
        //    await TreeViewHelpers.WaitForContainerGenerationAsync(treeView.ItemContainerGenerator);

        //    foreach (var category in treeView.Items)
        //    {
        //        if (treeView.ItemContainerGenerator.ContainerFromItem(category) is TreeViewItem categoryItem)
        //        {
        //            if (searchInCollapsedNodesToo)
        //            {
        //                // Make sure the child items are created
        //                categoryItem.IsExpanded = true;
        //                categoryItem.UpdateLayout();
        //            }

        //            foreach (var page in ((PageCategoryInfo)category).Pages)
        //            {
        //                await TreeViewHelpers.WaitForContainerGenerationAsync(categoryItem.ItemContainerGenerator);
        //                if (categoryItem.ItemContainerGenerator.ContainerFromItem(page) is TreeViewItem pageItem)
        //                {
        //                    if (page == pageToSelect)
        //                    {
        //                        pageItem.IsSelected = true;
        //                        return true;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return false;
        //}
    }

}
