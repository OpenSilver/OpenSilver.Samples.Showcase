using OpenSilver.ControlsKit;
using OpenSilver.Samples.Showcase.Search;
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

namespace OpenSilver.Samples.Showcase.Samples
{
    public partial class Search : UserControl
    {
        public Search()
        {
            this.InitializeComponent();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchField.Text;
            PerformSearch(searchText);
        }


        private void SearchField_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                string searchText = SearchField.Text;
                PerformSearch(searchText);
            }
        }

        internal void PerformSearch(string searchText)
        {
            //todo: if multiple searches one after the other, increase efficiency by only looking at the changes between the current search and the previous search
            //for now, we just clear everything.
            SamplesContainer.Children.Clear();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var searchResult = ControlSearch.Search(searchText);
                foreach (var res in searchResult)
                {
                    Type sampleType = SamplesInfoLoader.GetControlTypeByName(res.Name);
                    if (sampleType != null)
                    {
                        object controlInstance = Activator.CreateInstance(sampleType);

                        if (controlInstance is UIElement uiElement)
                        {
                            SamplesContainer.Children.Add(uiElement);
                        }
                    }
                }
            }
        }

    }
}
