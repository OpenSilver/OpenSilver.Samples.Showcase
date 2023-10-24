using PreviewOnWinRT;
using System;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class ChildWindow_Demo : UserControl
    {
        public ChildWindow_Demo()
        {
            this.InitializeComponent();
        }


        private void ButtonTestChildWindow_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWnd = new LoginWindow();
            loginWnd.Closed += new EventHandler(loginWnd_Closed);
            loginWnd.Show();
        }

        void loginWnd_Closed(object sender, EventArgs e)
        {
            LoginWindow lw = (LoginWindow)sender;
            if (lw.DialogResult.HasValue && lw.DialogResult.Value == true && lw.NameBox.Text != string.Empty)
            {
                this.TextBlockForTestingChildWindow.Text = "Hello " + lw.NameBox.Text;
            }
            else
            {
                this.TextBlockForTestingChildWindow.Text = "Login cancelled.";
            }
        }
    }
}
