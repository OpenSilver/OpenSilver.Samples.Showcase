using System.Windows;
using System.Windows.Controls;

namespace PreviewOnWinRT
{
    public partial class SmallChildWindow : ChildWindow
    {
        public SmallChildWindow()
        {
            InitializeComponent();
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

