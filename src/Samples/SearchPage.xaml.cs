using OpenSilver.Samples.Showcase.Search;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    public partial class SearchPage : Page
    {
        private const string SearchArgName = "SearchTerms";

        public SearchPage()
        {
            InitializeComponent();

            SearchField.Loaded += OnSearchFieldLoaded;
            SearchField.AddHandler(KeyDownEvent, new KeyEventHandler(SearchField_KeyDown), true);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.TryGetValue(SearchArgName, out var searchTerms) && !string.IsNullOrWhiteSpace(searchTerms))
            {
                SearchField.Text = searchTerms;
                PerformSearch(searchTerms);
            }
        }

        private void OnSearchFieldLoaded(object sender, RoutedEventArgs e)
        {
            SearchField.Focus();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            NavigateToSearch();
        }

        private void SearchField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NavigateToSearch();
            }
        }

        private void NavigateToSearch()
        {
            string searchText = SearchField.Text;
            NavigationService.Navigate(new Uri($"/Search/{searchText}", UriKind.Relative));
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
