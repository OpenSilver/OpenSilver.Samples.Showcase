using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    public partial class InitParams_Demo : UserControl
    {
        public InitParams_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonShowInitParams_Click(object sender, RoutedEventArgs e)
        {
            var parameters = Application.Current.Host.InitParams;
            string initParamsString = "I found this in init param:";

            foreach (var param in parameters)
            {
                initParamsString += $"\r\nkey: {param.Key}, value: {param.Value}";
            }

            MessageBox.Show(initParamsString);
        }
    }
}
