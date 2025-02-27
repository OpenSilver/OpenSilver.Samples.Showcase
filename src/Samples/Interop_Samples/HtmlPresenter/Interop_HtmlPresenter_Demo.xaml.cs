﻿using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("HTML", "interop", "rendering", "web", "UI")]
    public partial class Interop_HtmlPresenter_Demo : UserControl
    {
        public Interop_HtmlPresenter_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonClickHere_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The value is: " + NumericTextBox1.Value.ToString());
        }
    }
}
