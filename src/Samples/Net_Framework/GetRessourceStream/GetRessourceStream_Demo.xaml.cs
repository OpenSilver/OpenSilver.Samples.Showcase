using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
    }
}