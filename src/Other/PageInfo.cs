using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSilver.Samples.Showcase
{
    public class PageInfo
    {
        public PageInfo(string name, string path) => (Name, Path) = (name, path);

        static List<PageInfo> _pageInfos;
        static PageInfo _landingPageInfo;
        public static List<PageInfo> Pages
        {
            get
            {
                if (_pageInfos == null)
                {
                    _pageInfos = new List<PageInfo>();
                    _pageInfos.Add(new PageInfo("Panels & Controls", "/XAML_Controls"));
                    _pageInfos.Add(new PageInfo("Xaml Features", "/XAML_Features"));
                    _pageInfos.Add(new PageInfo(".NET Framework", "/Net_Framework"));
                    _pageInfos.Add(new PageInfo("Client / Server", "/Client_Server"));
                    _pageInfos.Add(new PageInfo("Interop", "/Interop_Samples"));
                    _pageInfos.Add(new PageInfo("Charts", "/Charts"));
                    _pageInfos.Add(new PageInfo("Performance", "/Performance"));
                    _pageInfos.Add(new PageInfo("Native APIs", "/Maui_Hybrid"));
                    _pageInfos.Add(new PageInfo("Third-Party", "/Third_Party"));
                    _pageInfos.Add(LandingPageInfo);
                    _pageInfos.Add(new PageInfo("Search", "/Search"));
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
                    _landingPageInfo = new PageInfo("Home", "/Welcome");
                }
                return _landingPageInfo;
            }
        }


        public string Name { get; set; }
        public string Path { get; set; }
    }
}
