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

namespace OpenSilver.Samples.Showcase
{
    public partial class Window_SizeChanged_Demo : UserControl
    {
        public Window_SizeChanged_Demo()
        {
            this.InitializeComponent();

            this.Loaded += Window_SizeChanged_Demo_Loaded;
            this.Unloaded += Window_SizeChanged_Demo_Unloaded;

            SetWindowSize();
        }

        #region Size Changed Events

        //When the window is loaded, we add the event Current_SizeChanged
        void Window_SizeChanged_Demo_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        //When the window is unloaded, we remove the event Current_SizeChanged
        void Window_SizeChanged_Demo_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Current_SizeChanged;
        }
        #endregion

#if SLMIGRATION
        void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
#else
        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e) 
#endif
        {

            TextBlockValueX.Text = (double.IsNaN(e.Size.Width) ? "NaN" : e.Size.Width.ToString());
            TextBlockValueY.Text = (double.IsNaN(e.Size.Height) ? "NaN" : e.Size.Height.ToString());
        }

        void SetWindowSize()
        {
            TextBlockValueX.Text = Window.Current.Bounds.Width.ToString();
            TextBlockValueY.Text = Window.Current.Bounds.Height.ToString();
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Window_SizeChanged_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Window_SizeChanged/Window_SizeChanged_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Window_SizeChanged_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Window_SizeChanged/Window_SizeChanged_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Window_SizeChanged_Demo.xaml.vb",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Window_SizeChanged/Window_SizeChanged_Demo.xaml.vb"
                }
            });
        }

    }
}
