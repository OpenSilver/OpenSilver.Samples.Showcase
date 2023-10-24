using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class Xaml_Features : UserControl
    {
        public Xaml_Features()
        {
            this.InitializeComponent();
#if OPENSILVER
            //Binding1Demo.Visibility = Visibility.Collapsed;
#endif
            MarkupExtensionsDemo.Visibility = Visibility.Collapsed;
        }
    }
}
