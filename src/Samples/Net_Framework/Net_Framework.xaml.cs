using System.Windows.Controls;
using System.Windows;

namespace OpenSilver.Samples.Showcase
{
    public partial class Net_Framework : UserControl
    {
        public Net_Framework()
        {
            this.InitializeComponent();

#if OPENSILVER
            //File_OpenDemo.Visibility = Visibility.Collapsed;
            //File_SaveDemo.Visibility = Visibility.Collapsed;
            //ZipDemo.Visibility = Visibility.Collapsed;
            //IsolatedStorageFileDemo.Visibility = Visibility.Collapsed;
            //IsolatedStorageSettingsDemo.Visibility = Visibility.Collapsed;
            JSON_SerializerDemo.Visibility = Visibility.Collapsed;
            GetRessourceStreamDemo.Visibility = Visibility.Collapsed;
            //FullScreenDemo.Visibility = Visibility.Collapsed;
            ConsoleDemo.Visibility = Visibility.Collapsed;
#endif
            RESXDemo.Visibility = Visibility.Collapsed;
        }
    }
}
