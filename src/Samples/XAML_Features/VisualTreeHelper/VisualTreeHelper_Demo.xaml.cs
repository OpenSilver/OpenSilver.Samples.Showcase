using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

namespace OpenSilver.Samples.Showcase
{
    public partial class VisualTreeHelper_Demo : UserControl
    {
        public VisualTreeHelper_Demo()
        {
            this.InitializeComponent();
        }

        private void RevealTree_Click(object sender, RoutedEventArgs e)
        {
            string finalVisualTree = GetVisualTree(this);
            MessageBox.Show(finalVisualTree);
        }

        public static string GetVisualTree(DependencyObject parent)
        {
            StringBuilder visualTreeStringBuilder = new StringBuilder("Visual Tree : " + Environment.NewLine + Environment.NewLine);
            GetChildren(parent, visualTreeStringBuilder);
            return visualTreeStringBuilder.ToString();
        }

        private static void GetChildren(DependencyObject parent, StringBuilder visualTreeStringBuilder, int indentation = 0)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            if (childrenCount > 0)
            {
                for (int i = 0; i < childrenCount; i++)
                {
                    for (int e = 0; e < indentation; e++)
                    {
                        visualTreeStringBuilder.Append("    ");
                    }

                    var currentChild = VisualTreeHelper.GetChild(parent, i);
                    visualTreeStringBuilder.AppendLine(currentChild.ToString());
                    GetChildren(currentChild, visualTreeStringBuilder, indentation + 1);
                }
            }
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "VisualTreeHelper_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/VisualTreeHelper/VisualTreeHelper_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "VisualTreeHelper_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/VisualTreeHelper/VisualTreeHelper_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "VisualTreeHelper_Demo.xaml.vb",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/VisualTreeHelper/VisualTreeHelper_Demo.xaml.vb"
                }
            });
        }

    }
}
