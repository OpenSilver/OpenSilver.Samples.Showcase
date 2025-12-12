using Microsoft.Maui.Devices;
using OpenSilver.Animations;
using OpenSilver.Themes.Modern;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace OpenSilver.Showcase
{
    public partial class MainPage : Page
    {
        bool _isMenuHidden;

        public MainPage()
        {
            InitializeComponent();

            Current = this;

            // Register some events:
            Loaded += MainPage_Loaded;
            SizeChanged += MainPage_SizeChanged;

            // Load the left menu:
            MenuTreeView.ItemsSource = Pages.AllPagesAndCategories;

            // Listen to clicks anywhere on the page, so as to collapse the left menu on mobile when user clicks outside of it:
            PageContainer.AddHandler(MouseDownEvent, new MouseButtonEventHandler(PageContainer_MouseDown), handledEventsToo: true);

            // Fix the color of the Light/Dark toggle:
            UpdateThemeToggleFillColor();

            // Improve the virtual keyboard experience on Android by ensuring that the focused element is translated up to remain into view when the virtual keyboard appears:
            PreventVirtualKeyboardOverlap();

            // Show the App Store & Google Play icons ONLY if we are running on the Web, not if we are running as a mobile app:
            if (DeviceInfo.Current.Platform == DevicePlatform.Android
                || DeviceInfo.Current.Platform == DevicePlatform.iOS)
            {
                GooglePlayBadgeButton.Visibility = Visibility.Collapsed;
                AppStoreBadgeButton.Visibility = Visibility.Collapsed;
            }

            // Uncomment the following lines to debug the animations:
            //Animations.Animation.SlowDownAnimationsForDebugging = 10.0; // slow down factor
            //Animations.Animation.LogAnimationsForDebugging = true;

#if !FULLBLAZOR
            PrepareBlazorSamples();
#endif
        }

#if !FULLBLAZOR
        private async void PrepareBlazorSamples()
        {
            var grid = PageScrollViewer.Content as Grid;
            var webBrowser = new WebBrowser { Visibility = Visibility.Collapsed };
            grid.Children.Insert(1, webBrowser); // after the Frame control to overlap it when visible
            //BlazorHelper.Initialize(webBrowser, () => DarkThemeRadioButton.IsChecked == true, "https://userware-test-deployment.azurewebsites.net/full/#/");
            BlazorHelper.Initialize(webBrowser, () => DarkThemeRadioButton.IsChecked == true);

            // preload files for the first Blazor sample, after menu animation
            await Task.Delay(7000);
            BlazorHelper.NavigateTo(nameof(Blazor_Radzen), hidden: true);
        }
#endif

        public static MainPage Current { get; private set; }

        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var docUri = System.Windows.Browser.HtmlPage.Document.DocumentUri.OriginalString;
            _isMenuHidden = docUri.Contains("menu=hidden");
            if (_isMenuHidden)
            {
                MenuContainer.Visibility = Visibility.Collapsed;
                ButtonToHideOrShowMenu.Visibility = Visibility.Collapsed;
                ForkOnGitHubButton.Visibility = Visibility.Collapsed;
                SuggestSamplesButton.Visibility = Visibility.Collapsed;
            }
            // Navigate to the "Welcome" page by default:
            if (!HtmlPage.Document.DocumentUri.OriginalString.Contains("#"))
            {
                await TreeViewHelpers.SelectItemInTreeViewAsync(MenuTreeView, Pages.LandingPageInfo);
            }
        }

        private void PreventVirtualKeyboardOverlap()
        {
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                PageScrollViewer.SizeChanged += async (s, e) =>
                {
                    if (e.HeightChanged)
                    {
                        var heightDelta = e.PreviousSize.Height - e.NewSize.Height;
                        if (heightDelta > 100 && FocusManager.GetFocusedElement() is FrameworkElement element) // most likely virtual keyboard has appeared
                        {
                            await Task.Delay(1);
                            PageScrollViewer.ScrollIntoView(element, 0, 10, new Duration(TimeSpan.FromMilliseconds(100)));
                        }
                    }
                };
            }
        }

        #region Navigation

        private void MenuTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!_skipMenu_SelectionChanged
                && e.NewValue != e.OldValue
                && e.NewValue is PageInfo page)
            {
                NavigateToPage(page.Path);
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
            PageScrollViewer.ScrollToVerticalOffset(0d);
        }

        private async void PageContainer_Navigated(object sender, NavigationEventArgs e)
        {
            _skipMenu_SelectionChanged = true;
            var selectedPage = MenuTreeView.SelectedItem as PageInfo;
            var navigatedPage = Pages.AllPages.FirstOrDefault(x => x.Path == e.Uri.OriginalString);
            if (navigatedPage != selectedPage)
            {
                await TreeViewHelpers.SelectItemInTreeViewAsync(MenuTreeView, navigatedPage);
            }

#if FULLBLAZOR
            if (e.Uri.OriginalString.Contains("theme=dark"))
            {
                DarkThemeRadioButton.IsChecked = true;
            }
            else if (DarkThemeRadioButton.IsChecked == true)
            {
                LightThemeRadioButton.IsChecked = true;
            }
#endif
            _skipMenu_SelectionChanged = false;
        }

        private async void Logo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Go to the homepage:
            await TreeViewHelpers.SelectItemInTreeViewAsync(MenuTreeView, Pages.LandingPageInfo);
            //NavigateToPage("");
        }

        bool _skipMenu_SelectionChanged = false;
        internal async Task StartSearch(string searchTerms)
        {
            _skipMenu_SelectionChanged = true;
            await TreeViewHelpers.SelectItemInTreeViewAsync(MenuTreeView, Pages.SearchPageInfo);

            NavigateToPage($"/Search/{Uri.EscapeUriString(searchTerms)}");
            _skipMenu_SelectionChanged = false;
        }

        #endregion

        #region Show/hide source code

        public void ViewSourceCode(UIElement controlThatDisplaysTheSourceCode)
        {
            // Open the Source Code Pane, which is the place where the source code will be displayed:
            if (SourceCodePane.Visibility == Visibility.Collapsed)
            {
                // Make the pane and grid splitter visible
                GridSplitter1.Visibility = Visibility.Visible;
                SourceCodePane.Visibility = Visibility.Visible;

                // Animate the appearance of the Source Code Pane and the Grid Splitter:
                var easing = new CubicEase { EasingMode = EasingMode.EaseOut };
                var animatorForGridSplitter = new PropertyAnimator(
                                    RowThatContainsTheGridSplitter,
                                    RowDefinition.HeightProperty,
                                    progress => new GridLength(progress * 5d, GridUnitType.Pixel))
                {
                    Duration = TimeSpan.FromMilliseconds(500),
                    EasingFunction = easing
                };
                animatorForGridSplitter.Begin();
                var animatorForSourceCodePane = new PropertyAnimator(
                                    RowThatContainsTheSourceCodePane,
                                    RowDefinition.HeightProperty,
                                    progress => new GridLength(progress * 1d, GridUnitType.Star))
                {
                    Duration = TimeSpan.FromMilliseconds(500),
                    EasingFunction = easing
                };
                animatorForSourceCodePane.Begin();
            }

            // Display the source code:
            PlaceWhereSourceCodeWillBeDisplayed.Child = controlThatDisplaysTheSourceCode;
        }

        private void ButtonToCloseSourceCode_Click(object sender, RoutedEventArgs e)
        {
            // Close the Source Code Pane, which is the place where the source code is displayed.

            // Note: we animate only the source code pane to 0, not the page, so that, if the user drags the splitter, it will not be reset to 0.5*:
            double initialStarHeightForRowThatContainsTheSourceCodePane = 0.5d;
            if (RowThatContainsTheSourceCodePane.Height.GridUnitType == GridUnitType.Star)
            {
                initialStarHeightForRowThatContainsTheSourceCodePane = RowThatContainsTheSourceCodePane.Height.Value;
            }

            // Create animations with easing
            var easing = new CubicEase { EasingMode = EasingMode.EaseIn };

            var animatorForGridSplitter = new PropertyAnimator(
                                RowThatContainsTheGridSplitter,
                                RowDefinition.HeightProperty,
                                progress => new GridLength((1d - progress) * 5d, GridUnitType.Pixel))
            {
                Duration = TimeSpan.FromMilliseconds(300), // Note: This animation is faster than the one for the source code pane, so that the "Completed" event of the the one for the source code pane is executed after this one completes.
                EasingFunction = easing
            };
            animatorForGridSplitter.Begin();

            var animatorForSourceCodePane = new PropertyAnimator(
                                RowThatContainsTheSourceCodePane,
                                RowDefinition.HeightProperty,
                                progress => new GridLength(initialStarHeightForRowThatContainsTheSourceCodePane - (progress * initialStarHeightForRowThatContainsTheSourceCodePane), GridUnitType.Star))
            {
                Duration = TimeSpan.FromMilliseconds(500),
                EasingFunction = easing
            };
            animatorForSourceCodePane.Begin();

            // Set up completion handler
            animatorForSourceCodePane.Completed += async (s, args) =>
            {
                await Task.Delay(300); // Let's wait a bit to make that the other animation is completed too.

                // Clean up when animation completes
                PlaceWhereSourceCodeWillBeDisplayed.Child = null;
                GridSplitter1.Visibility = Visibility.Collapsed;
                SourceCodePane.Visibility = Visibility.Collapsed;

                // Reset the row heights to their original values
                RowThatContainsThePage.Height = new GridLength(1d, GridUnitType.Star);
                RowThatContainsTheGridSplitter.Height = new GridLength(0d, GridUnitType.Pixel);
                RowThatContainsTheSourceCodePane.Height = new GridLength(0d, GridUnitType.Star);

                // Dispose of the animators to be extra safe
                //animator1.Dispose();
                animatorForGridSplitter.Dispose();
                animatorForSourceCodePane.Dispose();
            };
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

                    // Remove the top margin of the page (it's for the menu button on mobile):
                    PageContainer.Margin = new Thickness(0, 0, 0, 0);

                    // Ensure the page stays in the second column, to the right of the menu:
                    Grid.SetColumn(PageScrollViewer, 1);
                    Grid.SetColumnSpan(PageScrollViewer, 1);

                    // Show the menu:
                    if (!_isMenuHidden)
                    {
                        MenuContainer.Visibility = Visibility.Visible;
                    }

                    // Set the translation of the frame to 0:
                    ((TranslateTransform)PageScrollViewer.RenderTransform).X = 0;

                    // Set the translation of the border to 0:
                    ((TranslateTransform)MenuBorder.RenderTransform).X = 0;
                }
                else
                {
                    // Revert the changes that are specific to the CurrentState.LargeResolution_SeeBothMenuAndPage state.

                    // Show the button to hide/show the menu:
                    if (!_isMenuHidden)
                    {
                        ButtonToHideOrShowMenu.Visibility = Visibility.Visible;
                    }

                    // Add some top margin to the page for the menu button:
                    PageContainer.Margin = new Thickness(0, 50, 0, 0);

                    // Ensure the page is shown full-screen, not the right of the menu:
                    Grid.SetColumn(PageScrollViewer, 0);
                    Grid.SetColumnSpan(PageScrollViewer, 2);

                    // Adjust the menu animation:
                    Animation.SetOnAppear(MenuContainer, (IAnimationType)this.Resources["MenuAnimation_OnAppear_Faster"]);

                    if (newState == CurrentState.SmallResolution_ShowMenu)
                    {
                        // Show the menu:
                        if (!_isMenuHidden)
                        {
                            MenuContainer.Visibility = Visibility.Visible;
                        }

                        // Translate the page to the right, for a nicer effect:
                        ((TranslateTransform)PageScrollViewer.RenderTransform).X = 240;
                    }
                    else
                    {
                        // Hide the menu:
                        MenuContainer.Visibility = Visibility.Collapsed;

                        // Translate the page back to its original position:
                        ((TranslateTransform)PageScrollViewer.RenderTransform).X = 0;
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
            if (!double.IsNaN(actualWidth) && actualWidth >= 768d)
            {
                GoToState(CurrentState.LargeResolution_SeeBothMenuAndPage);
            }
            else if (_currentState == CurrentState.LargeResolution_SeeBothMenuAndPage
                || _currentState == CurrentState.Unset)
            {
                GoToState(CurrentState.SmallResolution_HideMenu);
            }

            double actualHeight = this.ActualHeight;
            OpenSilverLogoButton.Visibility = (actualHeight >= 460 ? Visibility.Visible : Visibility.Collapsed);
            HorizontalSeparatorThinBar.Visibility = (actualHeight < 920 ? Visibility.Visible : Visibility.Collapsed);
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

        private void PageContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Close the menu when the user clicks outside of it (on mobile):
            if (_currentState == CurrentState.SmallResolution_ShowMenu)
            {
                GoToState(CurrentState.SmallResolution_HideMenu);
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
                    LogoOpenSilverDark.Opacity = 1;
                    LogoOpenSilverLight.Opacity = 0;
                    LogoShowcaseDark.Opacity = 1;
                    LogoShowcaseLight.Opacity = 0;
                    BackgroundImageDark.Opacity = 1;
                    BackgroundImageLight.Opacity = 0;
                    LogoGitHubDark.Opacity = 1;
                    LogoGitHubLight.Opacity = 0;
                    GooglePlayBadgeDark.Opacity = 1;
                    GooglePlayBadgeLight.Opacity = 0;
                    AppStoreBadgeDark.Opacity = 1;
                    AppStoreBadgeLight.Opacity = 0;
                }
                else
                {
                    NativeApiButtonBackgroundBrush.Color = lightColor;
                    theme.CurrentPalette = ModernTheme.Palettes.Light;
                    LogoOpenSilverLight.Opacity = 1;
                    LogoOpenSilverDark.Opacity = 0;
                    LogoShowcaseLight.Opacity = 1;
                    LogoShowcaseDark.Opacity = 0;
                    BackgroundImageLight.Opacity = 1;
                    BackgroundImageDark.Opacity = 0;
                    LogoGitHubLight.Opacity = 1;
                    LogoGitHubDark.Opacity = 0;
                    GooglePlayBadgeLight.Opacity = 1;
                    GooglePlayBadgeDark.Opacity = 0;
                    AppStoreBadgeLight.Opacity = 1;
                    AppStoreBadgeDark.Opacity = 0;
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
#if !FULLBLAZOR
            BlazorHelper.UpdateTheme();
#endif
        }

        private void UpdateThemeToggleFillColor()
        {
            lightThemeImage.FillColor = darkThemeImage.FillColor = (DarkThemeRadioButton.Foreground as SolidColorBrush)?.Color;
        }

        #endregion
    }
}
