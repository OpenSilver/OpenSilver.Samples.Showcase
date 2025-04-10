using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class Xaml_Features : UserControl
    {
        public Xaml_Features()
        {
            InitializeComponent();

            // todo:
            MarkupExtensionsDemo.Visibility = Visibility.Collapsed;
        }
    }
}
