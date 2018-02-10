using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CSHTML5.Samples.Showcase
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

        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            TextBlockValueX.Text = (double.IsNaN(e.Size.Width) ? "NaN" : e.Size.Width.ToString());
            TextBlockValueY.Text = (double.IsNaN(e.Size.Height) ? "NaN" : e.Size.Height.ToString());
        }

        void SetWindowSize()
        {
            TextBlockValueX.Text = Window.Current.Bounds.Width.ToString();
            TextBlockValueY.Text = Window.Current.Bounds.Height.ToString();
        }
    }
}
