using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OpenSilver.Showcase
{
    public partial class SearchControl : UserControl
    {
        public SearchControl()
        {
            InitializeComponent();

            SearchField.PreviewKeyDown += SearchField_KeyDown;
        }

        public async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await StartSearch(SearchField.Text);
            SearchField.Focus();
        }

        private async void SearchField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await StartSearch(SearchField.Text);
            }
        }

        public async Task StartSearch(string searchTerms)
        {
            if (!string.IsNullOrWhiteSpace(searchTerms))
            {
                //get the MainPage and start the search:
                if (Application.Current.RootVisual is MainPage mainPage)
                {
                    await mainPage.StartSearch(searchTerms);
                }
            }
        }
    }
}
