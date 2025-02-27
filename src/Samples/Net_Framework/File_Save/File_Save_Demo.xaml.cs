using OpenSilver.Extensions.FileSystem;
using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("file", "save")]
    public partial class File_Save_Demo : UserControl
    {
        public File_Save_Demo()
        {
            this.InitializeComponent();
        }

        async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            await FileSaver.SaveTextToFile("Hello World!", "MyTestFile.txt");
        }
    }
}
