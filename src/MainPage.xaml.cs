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

            //#if OPENSILVER
            //            ThirdPartyButton.Visibility = Visibility.Collapsed;
            //            ThirdPartyHomeButton.Visibility = Visibility.Visible;
            //#endif

            Current = this;
            Loaded += MainPage_Loaded;
            SizeChanged += MainPage_SizeChanged;
            Theming.Theme th = new Themes.Modern.ModernTheme();

#if OPENSILVER
            TitleImage.Visibility = Visibility.Collapsed;
            TitleTextBlock.Text = "OPENSILVER SHOWCASE";
            TitleTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
#endif

            MenuListBox.ItemsSource = PageInfo.Pages;

            //if (DeviceInfo.Current.Platform == DevicePlatform.Unknown)
            //{
            //    MauiHybridButton.Visibility = Visibility.Collapsed;
            //}
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
            //Hide the menu:
            if (_currentState == CurrentState.SmallResolution_ShowMenu)
                GoToState(CurrentState.SmallResolution_HideMenu);

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

        #region States management

        //This region contains all that we use to make the menu on the left disappear when the screen is too small.

        enum CurrentState
        {
            Unset, // Initial value
            LargeResolution_SeeBothMenuAndPage, // This corresponds to tablets and other devices with high resolution. In this case we see both the menu and the page.
            SmallResolution_ShowMenu, // This corresponds to smartphones and other devices with low resolution. In this case we see the menu.
            SmallResolution_HideMenu // This corresponds to smartphones and other devices with low resolution. In this case we do not see the menu.
        }

        CurrentState _currentState;

        void GoToState(CurrentState newState)
        {
            if (newState != _currentState)
            {
                if (newState == CurrentState.LargeResolution_SeeBothMenuAndPage)
                {
                    // Hide the button to hide/show the menu:
                    ButtonToHideOrShowMenu.Visibility = Visibility.Collapsed;
                    PageContainer.Margin = new Thickness(0, 0, 0, 30);

                    // Set the translation of the frame to 0:
                    ((TranslateTransform)PageContainer.RenderTransform).X = 0;

                    // Set the margin of the frame to 180 (which is the size of the menu):
                    Thickness margin = PageContainer.Margin;
                    margin.Left = 180;
                    PageContainer.Margin = margin;

                    // Set the translation of the border to 0:
                    ((TranslateTransform)MenuBorder.RenderTransform).X = 0;
                }
                else
                {
                    // Revert the changes that are specific to the CurrentState.LargeResolution_SeeBothMenuAndPage state.

                    // Show the button to hide/show the menu:
                    ButtonToHideOrShowMenu.Visibility = Visibility.Visible;
                    PageContainer.Margin = new Thickness(0, 50, 0, 30);

                    Thickness margin = PageContainer.Margin;
                    margin.Left = 0;
                    PageContainer.Margin = margin;

                    if (newState == CurrentState.SmallResolution_ShowMenu)
                    {
                        // Show the menu:
                        ((TranslateTransform)PageContainer.RenderTransform).X = 180;
                        ((TranslateTransform)ButtonToHideOrShowMenu.RenderTransform).X = 180;
                        ((TranslateTransform)MenuBorder.RenderTransform).X = 0;
                    }
                    else
                    {
                        // Hide the menu:
                        ((TranslateTransform)PageContainer.RenderTransform).X = 0;
                        ((TranslateTransform)ButtonToHideOrShowMenu.RenderTransform).X = 0;
                        ((TranslateTransform)MenuBorder.RenderTransform).X = -180;
                    }
                }
                _currentState = newState;
            }
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateMenuDispositionBasedOnDisplaySize();
        }

        private void UpdateMenuDispositionBasedOnDisplaySize()
        {
            //note: another way to get the display width is commented below:
            //Rect windowBounds = Window.Current.Bounds;
            //double displayWidth = windowBounds.Width;

            double actualWidth = this.ActualWidth;
            if (!double.IsNaN(actualWidth) && actualWidth > 560d)
            {
                GoToState(CurrentState.LargeResolution_SeeBothMenuAndPage);
            }
            else if (_currentState == CurrentState.LargeResolution_SeeBothMenuAndPage
                || _currentState == CurrentState.Unset)
            {
                GoToState(CurrentState.SmallResolution_HideMenu);
            }
        }

        void ButtonToHideOrShowMenu_Click(object sender, RoutedEventArgs e)
        {
            if (_currentState == CurrentState.SmallResolution_ShowMenu)
            {
                GoToState(CurrentState.SmallResolution_HideMenu);
            }
            else if (_currentState == CurrentState.SmallResolution_HideMenu)
            {
                GoToState(CurrentState.SmallResolution_ShowMenu);
            }
            else
            {
                // Not supposed to happen because the button is not visible when in large resolution mode.
            }
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
