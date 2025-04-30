using OpenSilver.Themes.Modern;
using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Markup;
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
            SizeChanged += MainPage_SizeChanged;
            MenuListBox.ItemsSource = PageInfo.Pages;
            UpdateThemeToggleFillColor();
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
        private void Logo_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Go to the homepage:
            MenuListBox.SelectedItem = PageInfo.LandingPageInfo;
            //NavigateToPage("");
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
                    PageContainer.Margin = new Thickness(30, 0, 0, 0);

                    // Set the translation of the frame to 0:
                    ((TranslateTransform)PageContainer.RenderTransform).X = 0;

                    // Increase the margin of the frame to the width of the menu
                    Thickness margin = PageContainer.Margin;
                    margin.Left += MenuBorder.Width;
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
                        ((TranslateTransform)PageContainer.RenderTransform).X = 240;
                        ((TranslateTransform)ButtonToHideOrShowMenu.RenderTransform).X = 240;
                        ((TranslateTransform)MenuBorder.RenderTransform).X = 0;
                    }
                    else
                    {
                        // Hide the menu:
                        ((TranslateTransform)PageContainer.RenderTransform).X = 0;
                        ((TranslateTransform)ButtonToHideOrShowMenu.RenderTransform).X = 0;
                        ((TranslateTransform)MenuBorder.RenderTransform).X = -240;
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

        #region Themes switch related code
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

        private void ThemeToggle_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            bool isDark = (DarkThemeRadioButton.IsChecked == true);
            if (Application.Current.Theme is ModernTheme theme)
            {
                if (isDark)
                {
                    NativeApiButtonBackgroundBrush.Color = darkColor;
                    theme.CurrentPalette = ModernTheme.Palettes.Dark;
                    LogoBackgroundDark.Opacity = 0.4;
                    LogoBackgroundLight.Opacity = 0;
                    LogoOpenSilverDark.Opacity = 1;
                    LogoOpenSilverLight.Opacity = 0;
                    BackgroundImageDark.Opacity = 1;
                    BackgroundImageLight.Opacity = 0;
                }
                else
                {
                    NativeApiButtonBackgroundBrush.Color = lightColor;
                    theme.CurrentPalette = ModernTheme.Palettes.Light;
                    LogoBackgroundLight.Opacity = 0.4;
                    LogoBackgroundDark.Opacity = 0;
                    LogoOpenSilverLight.Opacity = 1;
                    LogoOpenSilverDark.Opacity = 0;
                    BackgroundImageLight.Opacity = 1;
                    BackgroundImageDark.Opacity = 0;
                }

                if (SourceCodePane.Visibility == Visibility.Visible &&
                    PlaceWhereSourceCodeWillBeDisplayed.Child is TabControl tabControl &&
                    tabControl.SelectedItem is TabItem tabItem &&
                    tabItem.Content is ControlToDisplayCodeHostedOnGitHub gitHubControl)
                {
                    gitHubControl.Refresh();
                }
            }

            UpdateThemeToggleFillColor();
        }

        private void UpdateThemeToggleFillColor()
        {
            lightThemeImage.FillColor = darkThemeImage.FillColor = (DarkThemeRadioButton.Foreground as SolidColorBrush)?.Color;
        }

        #endregion

    }
}
