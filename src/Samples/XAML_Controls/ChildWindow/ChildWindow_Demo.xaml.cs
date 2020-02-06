using PreviewOnWinRT;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CSHTML5.Samples.Showcase
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

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "ChildWindow_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Controls/ChildWindow/ChildWindow_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "ChildWindow_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Controls/ChildWindow/ChildWindow_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "LoginWindow.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Controls/ChildWindow/LoginWindow.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "LoginWindow.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Controls/ChildWindow/LoginWindow.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "DefaultChildWindowStyle.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Controls/ChildWindow/Styles/DefaultChildWindowStyle.xaml"
                }
            });
        }

    }
}
