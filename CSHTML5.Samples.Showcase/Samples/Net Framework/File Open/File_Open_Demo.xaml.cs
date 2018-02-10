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
    public partial class File_Open_Demo : UserControl
    {
        public File_Open_Demo()
        {
            this.InitializeComponent();
        }

        async void OnFileOpened(object sender, CSHTML5.Extensions.FileOpenDialog.FileOpenedEventArgs e)
        {
            MessageBox.Show(e.Text);
        }
    }
}
