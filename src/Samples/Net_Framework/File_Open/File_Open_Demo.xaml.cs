using System.Windows;
using System.Windows.Controls;

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
