using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
    public partial class GetRessourceStream_Demo : UserControl
    {
        Uri currentUri;


        public GetRessourceStream_Demo()
        {
            this.InitializeComponent();
        }

        private async void ViewFile_Click(object sender, RoutedEventArgs e)
        {
            currentUri = new Uri("ms-appx:/Other/SampleText.txt");
            Task<string> GetFileContent = RetrieveFileContent(currentUri);

            string currentFileContent = await GetFileContent;
            MessageBox.Show("Contains: " + currentFileContent);

        }

        private async Task<string> RetrieveFileContent(Uri uri)
        {
            var resourceStream = await Application.GetResourceStream(uri);
            StreamReader currentReader = new StreamReader(resourceStream.Stream);

            using (currentReader)
            {                                                                                                                               
                MessageBox.Show("Opening : " +  uri.AbsoluteUri);
                string result = await currentReader.ReadToEndAsync();
                return result;
            }

        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "GetRessourceStream_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/GetRessourceStream/GetRessourceStream_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "GetRessourceStream_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/GetRessourceStream/GetRessourceStream_Demo.xaml.cs"
                }
            });
        }
    }
}