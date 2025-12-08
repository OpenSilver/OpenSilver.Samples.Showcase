using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("file", "open", "dialog", "filesystem", "load")]
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
