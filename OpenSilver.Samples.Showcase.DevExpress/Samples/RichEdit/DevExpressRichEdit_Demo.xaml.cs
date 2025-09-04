using global::DevExpress.Blazor.RichEdit;
using OpenSilver.IO;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using OpenFileDialog = OpenSilver.Controls.OpenFileDialog;
using SaveFileDialog = OpenSilver.Controls.SaveFileDialog;

namespace OpenSilver.Samples.Showcase
{
    public partial class DevExpressRichEdit_Demo : UserControl
    {
        RTEDataContext _docInfo = new RTEDataContext();
        public DevExpressRichEdit_Demo()
        {
            this.InitializeComponent();
            this.DataContext = _docInfo;
        }


        public async void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "OpenXml (*.docx)|*.docx|Rich text Format (*.rtf)|*.rtf|Plain text (*.txt)|*.txt|Html|*.html";
            if (await ofd.ShowDialogAsync() == true)
            {
                MemoryFileInfo file = ofd.File;
                switch (file.Extension)
                {
                    case ".docx":
                        _docInfo.FileFormat = DocumentFormat.OpenXml;
                        break;
                    case ".rtf":
                        _docInfo.FileFormat = DocumentFormat.Rtf;
                        break;
                    case ".txt":
                        _docInfo.FileFormat = DocumentFormat.PlainText;
                        break;
                    case ".html":
                        _docInfo.FileFormat = DocumentFormat.Html;
                        break;
                    default:
                        break;
                }
                MemoryStream str = file.OpenRead() as MemoryStream;
                if (str != null)
                {
                    _docInfo.FileData = str.ToArray();
                }
            }
        }

        public async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            await TriggerComponentSave();

            string fileName = FileNameTextBox.Text.Trim();
            fileName = string.IsNullOrWhiteSpace(fileName) ? "MyFile" : fileName;
            string extension;
            switch (_docInfo.FileFormat)
            {
                case DocumentFormat.OpenXml:
                    extension = "docx";
                    break;
                case DocumentFormat.Rtf:
                    extension = "rtf";
                    break;
                case DocumentFormat.PlainText:
                    extension = "txt";
                    break;
                case DocumentFormat.Html:
                    extension = "html";
                    break;
                default:
                    extension = "txt";
                    break;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = extension;
            sfd.DefaultFileName = fileName;
            //sfd.Filter = "OpenXml (*.docx)|*.docx|Rich text Format (*.rtf)|*.rtf|Plain text (*.txt)|*.txt|Html|*.html";
            if (await sfd.ShowDialogAsync() == true)
            {
                byte[] data = _docInfo.FileData;
                var v = await sfd.OpenFileAsync();
                await v.WriteAsync(data, 0, data.Length);
                v.Dispose();
            }
        }

        public async Task TriggerComponentSave()
        {
            dynamic richEdit = ((dynamic)RichEditComponentHolder).Instance.myRichEdit;
            await richEdit.SaveDocumentAsync();
        }
    }

    internal class RTEDataContext : INotifyPropertyChanged
    {
        private Action<byte[]> _documentContentChangedDel;
        public Action<byte[]> DocumentContentChangedDel
        {
            get { return _documentContentChangedDel; }
            set { _documentContentChangedDel = value; OnPropertyChanged(); }
        }

        private DocumentFormat _fileFormat;
        public DocumentFormat FileFormat
        {
            get { return _fileFormat; }
            set { _fileFormat = value; OnPropertyChanged(); }
        }


        private byte[] _fileData;
        public byte[] FileData
        {
            get { return _fileData; }
            set { _fileData = value; OnPropertyChanged(); }
        }

        private bool _readOnly;

        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
