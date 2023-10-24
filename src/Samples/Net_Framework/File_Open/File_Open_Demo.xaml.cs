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
    public partial class File_Open_Demo : UserControl
    {
        public File_Open_Demo()
        {
            this.InitializeComponent();
        }

        void OnFileOpened(object sender, OpenSilver.Extensions.FileOpenDialog.FileOpenedEventArgs e)
        {
            MessageBox.Show(e.Text);
        }
    }
}
