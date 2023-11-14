using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using OpenSilver.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class OpenFileDialog_Demo : UserControl
    {
        private Controls.OpenFileDialog openFileDialog1;

        public OpenFileDialog_Demo()
        {
            this.InitializeComponent();
            openFileDialog1 = new Controls.OpenFileDialog()
            {
                Filter = "Text files (*.txt)|*.txt",
            };
        }

        private async void ButtonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (await openFileDialog1.ShowDialogAsync() == true)
            {
                try
                {
                    var file = openFileDialog1.File;
                    FileNameTextBlock.Text = file.Name;
                    using (StreamReader reader = new StreamReader(file.OpenRead(), Encoding.UTF8))
                    {
                        MessageBox.Show(reader.ReadToEnd());
                    }
                }
                catch (Exception ex)
                {
                    //fail silently
                }
            }
        }
    }
}
