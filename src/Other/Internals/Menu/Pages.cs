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
                    var brush1 = new SolidColorBrush(Color.FromRgb(85, 119, 240));
                    var brush2 = new SolidColorBrush(Color.FromRgb(205, 63, 186));
                    var brush3 = new SolidColorBrush(Color.FromRgb(253, 163, 28));
                    var brush4 = new SolidColorBrush(Color.FromRgb(71, 194, 14));

                    _allPagesAndCategories = new ObservableCollection<PageCategoryInfo>
                        {
                            new PageCategoryInfo
                            {
                                Name = "XAML & UI",
                                Foreground = brush1,
                                Pages = new ObservableCollection<PageInfo>
                                {
                                    new PageInfo { Name = "Controls", Path = "/XAML_Controls", Icon = "\uE913", IconBrush = brush1 },
                                    new PageInfo { Name = "Data Controls", Path = "/Data_Controls", Icon = "\uF1D0", IconBrush = brush1 },
                                    new PageInfo { Name = "XAML Features", Path = "/XAML_Features", Icon = "\uE920", IconBrush = brush1 },
                                    new PageInfo { Name = "Layout", Path = "/XAML_Layout", Icon = "\uE66B", IconBrush = brush1 },
                                    new PageInfo { Name = "JS Libs", Path = "/JS_Libs", Icon = "\uEB7C", IconBrush = brush1 },
                                    new PageInfo { Name = "Charts", Path = "/Charts", Icon = "\uE24B", IconBrush = brush1 },
                                    new PageInfo { Name = "Icons", Path = "/Icons", Icon = "\uE3B6", IconBrush = brush1 },
                                }
                            },
                            new PageCategoryInfo
                            {
                                Name = "NON-UI",
                                Foreground = brush2,
                                Pages = new ObservableCollection<PageInfo>
                                {
                                    new PageInfo { Name = "Client / Server", Path = "/Client_Server", Icon = "\uE1E2", IconBrush = brush2 },
                                    new PageInfo { Name = ".NET Framework", Path = "/Net_Framework", Icon = "\uE1BD", IconBrush = brush2 },
                                    new PageInfo { Name = "Native APIs", Path = "/Maui_Hybrid", Icon = "\uE0D4", IconBrush = brush2 },
                                }
                            },
                            new PageCategoryInfo
                            {
                                Name = "OTHER",
                                Foreground = brush3,
                                Pages = new ObservableCollection<PageInfo>
                                {

                                    new PageInfo { Name = "Interop", Path = "/Interop_Samples", Icon = "\uEACD", IconBrush = brush3 },
                                    new PageInfo { Name = "Performance", Path = "/Performance", Icon = "\uEB9B", IconBrush = brush3 },
                                    new PageInfo { Name = "Third-Party", Path = "/Third_Party", Icon = "\uEBBB", IconBrush = brush3 },
                                    LandingPageInfo,
                                    SearchPageInfo
                                }
                            },
                            new PageCategoryInfo
                            {
                                Name = "BLAZOR COMPONENTS",
                                Foreground = brush4,
                                Pages = new ObservableCollection<PageInfo>
                                {
                                    new PageInfo { Name = "Radzen Components", Path = "/Blazor_Radzen", Icon = "\uEB9B", IconBrush = brush4 },
                                    new PageInfo { Name = "MudBlazor Components", Path = "/Blazor_MudBlazor", Icon = "\uEB9B", IconBrush = brush4 },
                                    new PageInfo {Name = "Blazorise Components", Path = "/Blazor_Blazorise", Icon = "\uEB9B", IconBrush = brush4 },
                                    new PageInfo {Name = "DevExpress Components", Path = "/Blazor_DevExpress", Icon = "\uEB9B", IconBrush = brush4 },
                                    new PageInfo {Name = "Syncfusion Components", Path = "/Blazor_Syncfusion", Icon = "\uEB9B", IconBrush = brush4 },
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
    }

}
