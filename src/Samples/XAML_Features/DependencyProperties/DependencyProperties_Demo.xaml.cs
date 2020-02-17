using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
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

namespace CSHTML5.Samples.Showcase
{
    public partial class DependencyProperties_Demo : UserControl
    {
        public DependencyProperties_Demo()
        {
            this.InitializeComponent();
        }

        public int MySampleDependencyProperty
        {
            get { return (int)GetValue(MySampleDependencyPropertyProperty); }
            set { SetValue(MySampleDependencyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySampleDependencyProperty. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySampleDependencyPropertyProperty =
            DependencyProperty.Register(
                "MySampleDependencyProperty", // This is the name of the DependencyProperty
                typeof(int), // This is the type of the DependencyProperty
                typeof(DependencyProperties_Demo), // This is the type of the class that declares the DependencyProperty
                new PropertyMetadata(0, DependencyProperty_Changed) // This is the default value of the DependencyProperty and the callback when the value changes
                {
                    CallPropertyChangedWhenLoadedIntoVisualTree = WhenToCallPropertyChangedEnum.Never
                }); 

        private static void DependencyProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show("The DependencyProperty value has changed!");
        }

        public void Decrementation_Click(object sender, RoutedEventArgs e)
        {
            MySampleDependencyProperty--;
        }

        public void Incrementation_Click(object sender, RoutedEventArgs e)
        {
            MySampleDependencyProperty++;
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DependencyProperties_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Features/DependencyProperties/DependencyProperties_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DependencyProperties_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Features/DependencyProperties/DependencyProperties_Demo.xaml.cs"
                }
            });
        }
    }
}
