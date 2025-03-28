using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OpenSilver.Samples.Showcase
{
    public partial class SearchControl : UserControl
    {
        public SearchControl()
        {
            InitializeComponent();

            SearchField.AddHandler(KeyDownEvent, new KeyEventHandler(SearchField_KeyDown), true);
        }

        public void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            StartSearch(SearchField.Text);
        }

        private void SearchField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
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
