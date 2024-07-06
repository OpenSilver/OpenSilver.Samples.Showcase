using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class IsolatedStorageSettings_Demo : UserControl
    {
        public IsolatedStorageSettings_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonSaveToIsolatedStorageSettings_Click(object sender, RoutedEventArgs e)
        {
            if (!DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer())
            {
                string key = "SampleKey";
                string value = TextBoxIsolatedStorageSettingsDemo.Text;

                IsolatedStorageSettings.ApplicationSettings[key] = value;
                MessageBox.Show("The text was successfully saved to the storage.");
            }
        }

        private void ButtonLoadFromIsolatedStorageSettings_Click(object sender, RoutedEventArgs e)
        {
            if (!DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer())
            {
                string key = "SampleKey";

                string value;
                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out value))
                    MessageBox.Show("The following text was read from the storage: " + value);
                else
                    MessageBox.Show("No text was found in the storage.");
            }
        }

        private void ButtonDeleteFromIsolatedStorageSettings_Click(object sender, RoutedEventArgs e)
        {
            if (!DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer())
            {
                string key = "SampleKey";
                IsolatedStorageSettings.ApplicationSettings.Remove(key);
                MessageBox.Show("The text was successfully removed from the storage.");
            }
        }

        bool DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer()
        {
            return false;
        }

        bool IsRunningFromLocalFileSystemOnInternetExplorer()
        {
            return Convert.ToBoolean(Interop.ExecuteJavaScript(@"window.IE_VERSION && document.location.protocol === ""file:"""));
        }
    }
}
