using OpenSilver.Themes.Modern;
using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace OpenSilver.Samples.Showcase
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            Current = this;
            Loaded += MainPage_Loaded;
            MenuListBox.ItemsSource = PageInfo.Pages;
        }

        public static MainPage Current { get; private set; }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Navigate to the "Welcome" page by default:
            if (!HtmlPage.Document.DocumentUri.OriginalString.Contains("#"))
            {
                MenuListBox.SelectedItem = PageInfo.LandingPageInfo;
            }
        }

        #region Navigation

        private void MenuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(_skipMenuListBox_SelectionChanged && (e.AddedItems?.Count == 0)))
            {
                PageInfo page = e.AddedItems[0] as PageInfo;
                if (page != null)
                {
                    NavigateToPage(page.Path);
                }
            }
        }

        void NavigateToPage(string targetUri)
        {
            // Collapse the menu if on mobile:
            responsiveSidePanel.CollapseIfMobile();

            // Navigate to the target page:
            Uri uri = new Uri(targetUri, UriKind.Relative);
            PageContainer.Source = uri;

            // Scroll to top:
            ScrollViewer1.ScrollToVerticalOffset(0d);
        }

        private void ButtonBackwards_Click(object sender, RoutedEventArgs e)
        {
            if (PageContainer.CanGoBack)
            {
                PageContainer.GoBack();
            }
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            if (PageContainer.CanGoForward)
            {
                PageContainer.GoForward();
            }
        }

        private void PageContainer_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ButtonBackwards.IsEnabled = PageContainer.CanGoBack;
            ButtonForward.IsEnabled = PageContainer.CanGoForward;
        }

        bool _skipMenuListBox_SelectionChanged = false;
        internal void StartSearch(string searchTerms)
        {
            _skipMenuListBox_SelectionChanged = true;
            MenuListBox.SelectedItem = PageInfo.SearchPageInfo;

            NavigateToPage($"/Search/{Uri.EscapeUriString(searchTerms)}");
            _skipMenuListBox_SelectionChanged = false;
        }

        #endregion

        #region Show/hide source code

        public void ViewSourceCode(UIElement controlThatDisplaysTheSourceCode)
        {
            // Open the Source Code Pane, which is the place where the source code will be displayed:
            if (SourceCodePane.Visibility == Visibility.Collapsed)
            {
                RowThatContainsThePage.Height = new GridLength(0.5d, GridUnitType.Star);
                RowThatContainsTheGridSplitter.Height = new GridLength(5d);
                RowThatContainsTheSourceCodePane.Height = new GridLength(0.5d, GridUnitType.Star);
                GridSplitter1.Visibility = Visibility.Visible;
                SourceCodePane.Visibility = Visibility.Visible;
            }

            // Display the source code:
            PlaceWhereSourceCodeWillBeDisplayed.Child = controlThatDisplaysTheSourceCode;
        }

        private void ButtonToCloseSourceCode_Click(object sender, RoutedEventArgs e)
        {
            // Close the Source Code Pane, which is the place where the source code is displayed:
            PlaceWhereSourceCodeWillBeDisplayed.Child = null;
            GridSplitter1.Visibility = Visibility.Collapsed;
            SourceCodePane.Visibility = Visibility.Collapsed;
            RowThatContainsThePage.Height = new GridLength(1d, GridUnitType.Star);
            RowThatContainsTheGridSplitter.Height = new GridLength(0d);
            RowThatContainsTheSourceCodePane.Height = new GridLength(0d);
        }

        #endregion

        #region themes switch related code
        SolidColorBrush _nativeApiButtonBackgroundBrush;
        public SolidColorBrush NativeApiButtonBackgroundBrush
        {
            get
            {
                if (_nativeApiButtonBackgroundBrush == null)
                {
                    _nativeApiButtonBackgroundBrush = this.Resources["NativeApiButtonBackground"] as SolidColorBrush;
                }
                return _nativeApiButtonBackgroundBrush;

            }
        }
        Color lightColor = Color.FromRgb(221, 221, 221);
        Color darkColor = Color.FromRgb(60, 60, 60);

        private void ToggleThemeButton_Click(object sender, RoutedEventArgs e)
        {
            bool isDark = (sender as ToggleButton)?.IsChecked == true;
            if (Application.Current.Theme is ModernTheme theme)
            {
                if (isDark)
                {
                    NativeApiButtonBackgroundBrush.Color = darkColor;
                    theme.CurrentPalette = ModernTheme.Palettes.Dark;
                }
                else
                {
                    NativeApiButtonBackgroundBrush.Color = lightColor;
                    theme.CurrentPalette = ModernTheme.Palettes.Light;
                }

                if (SourceCodePane.Visibility == Visibility.Visible &&
                    PlaceWhereSourceCodeWillBeDisplayed.Child is TabControl tabControl &&
                    tabControl.SelectedItem is TabItem tabItem &&
                    tabItem.Content is ControlToDisplayCodeHostedOnGitHub gitHubControl)
                {
                    gitHubControl.Refresh();
                }
            }
        }
        #endregion
    }
}
