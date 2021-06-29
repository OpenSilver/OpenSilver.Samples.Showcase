using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows.Controls;
using System.Windows;
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
