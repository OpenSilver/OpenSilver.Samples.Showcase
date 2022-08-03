using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
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
            //-----------------------------------------------------------
            // When running inside Internet Explorer or Edge, the HTML5
            // Storage API is available only if the URL starts with http
            // or https. This method will display a message to the user
            // to inform her about this.
            //-----------------------------------------------------------
            if (CSharpXamlForHtml5.Environment.IsRunningInJavaScript)
            {
                //Execute a piece of JavaScript code:
                if (IsRunningFromLocalFileSystemOnInternetExplorer())
                {
                    MessageBox.Show("The local storage - used to persist data - is not available on Internet Explorer or Edge when running the website from the local file system (ie. the URL starts with 'c:\' or 'file:///'). To solve the problem, please run the website from a web server instead (ie. the URL must start with 'http://' or 'https://') or test the local storage using a different browser.");
                    return true;
                }
            }
            return false;
        }

        bool IsRunningFromLocalFileSystemOnInternetExplorer()
        {
            return Convert.ToBoolean(Interop.ExecuteJavaScript(@"window.IE_VERSION && document.location.protocol === ""file:"""));
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "IsolatedStorageSettings_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/IsolatedStorageSettings/IsolatedStorageSettings _Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "IsolatedStorageSettings_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/IsolatedStorageSettings/IsolatedStorageSettings _Demo.xaml.cs"
                }
            });
        }

    }
}
