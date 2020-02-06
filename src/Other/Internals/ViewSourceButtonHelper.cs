using System;
using System.Collections.Generic;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

namespace CSHTML5.Samples.Showcase
{
    static class ViewSourceButtonHelper
    {
        public static void ViewSource(List<ViewSourceButtonInfo> sourcePaths)
        {
            if (sourcePaths.Count > 0)
            {
                var tabControl = new TabControl();

                foreach (ViewSourceButtonInfo viewSourceButtonInfo in sourcePaths)
                {
                    var tabItem = new TabItem()
                    {
                        Header = viewSourceButtonInfo.TabHeader,
                        Content = new ControlToDisplayCodeHostedOnGitHub()
                        {
                            FilePathOnGitHub = viewSourceButtonInfo.FilePathOnGitHub
                        }
                    };

                    tabControl.Items.Add(tabItem);
                }

                ((TabItem)tabControl.Items[0]).IsSelected = true;

                MainPage.Current.ViewSourceCode(tabControl);
            }
        }
    }
}