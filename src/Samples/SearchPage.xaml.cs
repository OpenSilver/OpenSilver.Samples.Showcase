using OpenSilver.Samples.Showcase.Search;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    public partial class SearchPage : Page
    {
        private const string SearchArgName = "SearchTerms";

        public SearchPage()
        {
            InitializeComponent();

            //SearchField.Loaded += OnSearchFieldLoaded;
            //SearchField.AddHandler(KeyDownEvent, new KeyEventHandler(SearchField_KeyDown), true);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.TryGetValue(SearchArgName, out var searchTerms) && !string.IsNullOrWhiteSpace(searchTerms))
            {
                //SearchField.Text = searchTerms;
                PerformSearch(searchTerms);
            }
        }

        //private void OnSearchFieldLoaded(object sender, RoutedEventArgs e)
        //{
        //    SearchField.Focus();
        //}

        //private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        //{
        //    NavigateToSearch();
        //}

        //private void SearchField_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        NavigateToSearch();
        //    }
        //}

        //private void NavigateToSearch()
        //{
        //    string searchText = SearchField.Text;
        //    NavigationService.Navigate(new Uri($"/Search/{searchText}", UriKind.Relative));
        //}

        internal void PerformSearch(string searchText)
        {
            //todo: if multiple searches one after the other, increase efficiency by only looking at the changes between the current search and the previous search
            //for now, we just clear everything.
            SamplesPanel.ItemsSource = null;
            var samples = new List<UIElement>();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var searchResult = ControlSearch.Search(searchText);
                foreach (var res in searchResult)
                {
                    Type sampleType = SamplesInfoLoader.GetControlTypeByName(res.Name);
                    if (sampleType != null)
                    {
                        object controlInstance = Activator.CreateInstance(sampleType);

                        if (controlInstance is FrameworkElement element)
                        {
                            element.HorizontalAlignment = HorizontalAlignment.Center;
                            samples.Add(element);
                        }
                    }
                }
            }

            SamplesPanel.ItemsSource = samples;
        }
    }
}
