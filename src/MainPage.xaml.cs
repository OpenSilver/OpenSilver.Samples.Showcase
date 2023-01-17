using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Browser;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#endif

namespace OpenSilver.Samples.Showcase
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

#if OPENSILVER
            ThirdPartyButton.Visibility = Visibility.Collapsed;
            ThirdPartyHomeButton.Visibility = Visibility.Visible;
#endif

            MainPage.Current = this;
            this.Loaded += MainPage_Loaded;
            Window.Current.SizeChanged += Window_SizeChanged;

#if OPENSILVER
            TitleImage.Visibility = Visibility.Collapsed;
            TitleTextBlock.Text = @"OPENSILVER
SHOWCASE";
            TitleTextBlock.TextAlignment = TextAlignment.Center;
            TitleTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
#endif
        }

        public static MainPage Current { get; private set; }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Navigate to the "Welcome" page by default:
            if (!HtmlPage.Document.DocumentUri.OriginalString.Contains("#"))
            {
                NavigateToPage("/Welcome");
            }

            UpdateMenuDispositionBasedOnDisplaySize();
        }

        void ButtonXamlControls_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/XAML_Controls");
        }

        void ButtonXamlFeatures_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/XAML_Features");
        }

        void ButtonNetFramework_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Net_Framework");
        }

        void ButtonClientServer_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Client_Server");
        }

        void ButtonInterop_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Interop_Samples");
        }

        void ButtonPerformance_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Performance");
        }

        void ButtonMaterialDesign_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Material_Design");
        }

        void ButtonPlotlyCharts_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Third_Party/Plotly_Charts/Plotly_Charts_Demo");
        }

        void ButtonSyncfusionEssentialJS1Spreadsheet_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Third_Party/Syncfusion_EssentialJS1/Spreadsheet/Spreadsheet_Demo");
        }

        void ButtonSyncfusionEssentialJS1RichTextEditor_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Third_Party/Syncfusion_EssentialJS1/RichTextEditor/RichTextEditor_Demo");
        }

        void ButtonTelerikKendoUIGrid_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Third_Party/Telerik_KendoUI/Grid/Grid_Demo");
        }

        void ButtonTelerikKendoUIEditor_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Third_Party/Telerik_KendoUI/Editor/Editor_Demo");
        }

        void ButtonDevExtremeDataGrid_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Third_Party/DevExtreme/DataGrid/DataGrid_Demo");
        }

        void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Welcome");
        }

        void ThirdParty_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Third_Party");
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

#region Show/hide source code

        public void ViewSourceCode(UIElement controlThatDisplaysTheSourceCode)
        {
            // Open the Source Code Pane, which is the place where the source code will be displayed:
            if (SourceCodePane.Visibility == Visibility.Collapsed)
            {
                RowThatContainsThePage.Height = new GridLength(0.5d, GridUnitType.Star);
                RowThatContainsTheGridSplitter.Height = new GridLength(5d);
                RowThatContainsTheSourceCodePane.Height = new GridLength(0.5d, GridUnitType.Star);
                //GridSplitter1.ResizeDirection = GridSplitter.GridResizeDirection.Rows;
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

#if SLMIGRATION
        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs e)
#else
        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
#endif
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
    }
}
