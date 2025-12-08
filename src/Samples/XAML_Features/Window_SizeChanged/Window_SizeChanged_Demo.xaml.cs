using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("window", "resize", "event", "UI")]
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

        void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
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
