using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    public partial class CodeSnippetChildWindow : ChildWindow
    {
        public CodeSnippetChildWindow()
        {
            InitializeComponent();

            this.Loaded += CodeSnippetChildWindow_Loaded;
        }

        public CodeSnippetChildWindow(string snippetOfCode, bool isMaterialIcons = false)
            : this()
        {
            SnippetTextBox.Text = snippetOfCode;

            if (isMaterialIcons)
            {
                MaterialIconsInstructionsContainer.Visibility = Visibility.Visible;
                GenericInstructionsContainer.Visibility = Visibility.Collapsed;
            }
        }

        private async void CodeSnippetChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500); // Workaround to ensure that the content of the TextBox has finishedloading.

            // Select all text in the TextBox
            SnippetTextBox.SelectAll();

            // Focus the TextBox, just in case that the other TextBox gets focused when the ChildWindow animation competes.
            await Task.Delay(1000);
            SnippetTextBox.Focus();
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

