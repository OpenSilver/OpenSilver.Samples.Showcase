using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class CodeSnippetChildWindow : ChildWindow
    {
        public CodeSnippetChildWindow()
        {
            InitializeComponent();

            this.Loaded += CodeSnippetChildWindow_Loaded;
        }

        public CodeSnippetChildWindow(string snippetOfCode)
            : this()
        {

            SnippetTextBox.Text = snippetOfCode;
        }

        private async void CodeSnippetChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500); // Workaround to ensure that the content of the TextBox has finishedloading.

            SnippetTextBox.SelectAll();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

