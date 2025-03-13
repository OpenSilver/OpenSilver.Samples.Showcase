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
    public partial class SearchControl : UserControl
    {
        public SearchControl()
        {
            this.InitializeComponent();
        }

        public void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            StartSearch(SearchField.Text);
        }

        private void SearchField_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                StartSearch(SearchField.Text);
            }
        }
        public void StartSearch(string searchTerms)
        {
            if (!string.IsNullOrWhiteSpace(searchTerms))
            {
                //get the MainPage and start the search:
                if (Application.Current.RootVisual is MainPage mainPage)
                {
                    mainPage.StartSearch(searchTerms);
                }
            }
        }
    }
}
