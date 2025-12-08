using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("initialization", "parameters", "startup", "configuration", "settings")]
    public partial class InitParams_Demo : UserControl
    {
        public InitParams_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonShowInitParams_Click(object sender, RoutedEventArgs e)
        {
            var parameters = Application.Current.Host.InitParams;
            string initParamsString = "I found this in init param:";

            foreach (var param in parameters)
            {
                initParamsString += $"\r\nkey: {param.Key}, value: {param.Value}";
            }

            MessageBox.Show(initParamsString);
        }
    }
}
