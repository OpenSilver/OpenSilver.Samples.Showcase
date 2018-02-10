using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CSHTML5.Samples.Showcase
{
    public partial class ExecuteJavascript_Demo : UserControl
    {
        public ExecuteJavascript_Demo()
        {
            this.InitializeComponent();
        }

        void SendJavascriptMessage(object sender, RoutedEventArgs e)
        {
            Interop.ExecuteJavaScript("alert($0);", TextBoxInput.Text);
        }
    }
}
