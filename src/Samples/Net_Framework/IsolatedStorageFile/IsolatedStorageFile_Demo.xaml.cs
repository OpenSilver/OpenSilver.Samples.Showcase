using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
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
    public partial class IsolatedStorageFile_Demo : UserControl
    {
        public IsolatedStorageFile_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonSaveToIsolatedStorageFile_Click(object sender, RoutedEventArgs e)
        {
            if (!DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer())
            {
                string fileName = "SampleFile.txt";
                string data = TextBoxFileStorageDemo.Text;

                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    IsolatedStorageFileStream fs = null;
                    using (fs = storage.CreateFile(fileName))
                    {
                        if (fs != null)
                        {
                            Encoding encoding = new UTF8Encoding();
                            byte[] bytes = encoding.GetBytes(data);
                            fs.Write(bytes, 0, bytes.Length);
                            fs.Close();
                            MessageBox.Show("A new file named SampleFile.txt was successfully saved to the storage.");
                        }
                        else
                            MessageBox.Show("Unable to save the file SampleFile.txt to the storage.");
                    }
                }
            }
        }

        private void ButtonLoadFromIsolatedStorageFile_Click(object sender, RoutedEventArgs e)
        {
            if (!DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer())
            {
                string fileName = "SampleFile.txt";

                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    if (storage.FileExists(fileName))
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile(fileName, System.IO.FileMode.Open))
                        {
                            if (fs != null)
                            {
                                using (StreamReader sr = new StreamReader(fs))
                                {
                                    string data = sr.ReadToEnd();
                                    MessageBox.Show("The following text was read from the file SampleFile.txt located in the storage: " + data);
                                }
                            }
                            else
                                MessageBox.Show("Unable to load the file SampleFile.txt from the storage.");
                        }
                    }
                    else
                        MessageBox.Show("No file named SampleFile.txt was found in the storage.");
                }
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
                    TabHeader = "IsolatedStorageFile_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/IsolatedStorageFile/IsolatedStorageFile_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "IsolatedStorageFile_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/IsolatedStorageFile/IsolatedStorageFile_Demo.xaml.cs"
                }
            });
        }

    }
}
