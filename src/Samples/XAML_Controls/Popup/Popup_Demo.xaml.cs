﻿using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("popup", "modal", "window", "overlay", "UI")]
    public partial class Popup_Demo : UserControl
    {
        public Popup_Demo()
        {
            this.InitializeComponent();
        }

        private void OpenPopupButton_Click(object sender, RoutedEventArgs e)
        {
            MyPopup.IsOpen = true;
        }

        private void PopupButtonClose_Click(object sender, RoutedEventArgs e)
        {
            MyPopup.IsOpen = false;
        }
    }
}
