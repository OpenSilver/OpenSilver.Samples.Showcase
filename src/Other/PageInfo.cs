using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSilver.Samples.Showcase
{
    public class PageInfo
    {
        public PageInfo(string name, string path, bool isVisibleInMenu) => (Name, Path, IsVisibleInMenu) = (name, path, isVisibleInMenu);

        static List<PageInfo> _pageInfos;
        static PageInfo _landingPageInfo;
        static PageInfo _searchPageInfo;
        public static List<PageInfo> Pages
        {
            get
            {
                if (_pageInfos == null)
                {
                    _pageInfos = new List<PageInfo>();
                    _pageInfos.Add(new PageInfo("Panels & Controls", "/XAML_Controls", true));
                    _pageInfos.Add(new PageInfo("Xaml Features", "/XAML_Features", true));
                    _pageInfos.Add(new PageInfo(".NET Framework", "/Net_Framework", true));
                    _pageInfos.Add(new PageInfo("Client / Server", "/Client_Server", true));
                    _pageInfos.Add(new PageInfo("Interop", "/Interop_Samples", true));
                    _pageInfos.Add(new PageInfo("JS Libs", "/JS_Libs", true));
                    _pageInfos.Add(new PageInfo("Charts", "/Charts", true));
                    _pageInfos.Add(new PageInfo("Performance", "/Performance", true));
                    _pageInfos.Add(new PageInfo("Native APIs", "/Maui_Hybrid", true));
                    _pageInfos.Add(new PageInfo("Third-Party", "/Third_Party", true));
                    _pageInfos.Add(LandingPageInfo);
                    _pageInfos.Add(SearchPageInfo);
                }

                return _pageInfos;
            }
        }

        public static PageInfo LandingPageInfo
        {
            get
            {
                if (_landingPageInfo == null)
                {
                    _landingPageInfo = new PageInfo("Home", "/Welcome", false);
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
                    _searchPageInfo = new PageInfo("Search", "/Search", false);
                }
                return _searchPageInfo;
            }
        }


        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsVisibleInMenu { get; set; }
    }
}
