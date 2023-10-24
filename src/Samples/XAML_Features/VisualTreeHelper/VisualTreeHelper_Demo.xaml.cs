using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
    }
}
