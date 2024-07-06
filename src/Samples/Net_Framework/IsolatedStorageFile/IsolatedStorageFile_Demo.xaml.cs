using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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
            return false;
        }
    }
}
