using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class Net_Framework : UserControl
    {
        public Net_Framework()
        {
            InitializeComponent();

            // todo:
            JSON_SerializerDemo.Visibility = Visibility.Collapsed;
            GetRessourceStreamDemo.Visibility = Visibility.Collapsed;
            ConsoleDemo.Visibility = Visibility.Collapsed;
            RESXDemo.Visibility = Visibility.Collapsed;
        }
    }
}
