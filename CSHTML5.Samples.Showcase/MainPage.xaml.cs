using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CSHTML5.Samples.Showcase
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            Window.Current.SizeChanged += Window_SizeChanged;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMenuDispositionBasedOnDisplaySize();
        }



        void ButtonXamlControls_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/XAML_Controls/XAML_Controls");
        }

        void ButtonXamlFeatures_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/XAML_Features/XAML_Features");
        }

        void ButtonNetFramework_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Net_Framework/Net_Framework");
        }

        void ButtonClientServer_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Client-Server/Client_Server");
        }

        void ButtonInterop_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Interop_Samples/Interop_Samples");
        }

        void ButtonExtensions_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Extensions/Extensions");
        }

        void ButtonPerformance_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage("/Performance/Performance");
        }

        void NavigateToPage(string targetUri)
        {
            isSourceSet = true;
            //Hide the menu:
            UpdateMenuDispositionBasedOnDisplaySize();

            // Display the "Loading..." text:
            LoadingMessage.Visibility = Visibility.Visible;

            // We use the Dispatcher to give enough time to the browser to refresh and show the "Loading..." text:
            Dispatcher.BeginInvoke((Action)(() =>
                {
                    // Navigate to the target page:
                    Uri uri = new Uri(targetUri, UriKind.Relative);
                    PageContainer.Source = uri;

                    // Scroll to top:
                    ScrollViewer1.ScrollToVerticalOffset(0d);

                    // Hide the "Loading..." text:
                    LoadingMessage.Visibility = Visibility.Collapsed;
                }));
        }


        #region states management
        //this region contains all that we use to make the menu on the left disappears when the screen is too small.
        enum CurrentState
        {
            LargeResolution_SeeBothMenuAndPage, // This corresponds to tablets and other devices with high resolution. In this case we see both the menu and the page.
            SmallResolution_ShowMenu, // This corresponds to smartphones and other devices with low resolution. In this case we see only the menu.
            SmallResolution_HideMenu // This corresponds to smartphones and other devices with low resolution. In this case we see only the page.
        }


        bool isSourceSet = false;
        CurrentState currentState = CurrentState.LargeResolution_SeeBothMenuAndPage; //we start with that since there is no page to show anyway. 

        void GoToState(CurrentState newState)
        {
            if(newState == CurrentState.LargeResolution_SeeBothMenuAndPage || !isSourceSet)
            {
                    //hide the button to show/display the menu:
                    ButtonToHideOrShowMenu.Visibility = Visibility.Collapsed;
                    //set the translation of the frame to 0:
                    ((TranslateTransform)PageContainer.RenderTransform).X = 0;
                    //set the margin of the frame to 180 (which is the size of the menu):
                    Thickness margin = PageContainer.Margin;
                    margin.Left = 180;
                    PageContainer.Margin = margin;
                    //set the translation of the border to 0:
                    ((TranslateTransform)MenuBorder.RenderTransform).X = 0;
                    currentState = CurrentState.LargeResolution_SeeBothMenuAndPage;
            }
            else
            {
                //revert the changes that are specific to the CurrentState.LargeResolution_SeeBothMenuAndPage state:
                ButtonToHideOrShowMenu.Visibility = Visibility.Visible;
                Thickness margin = PageContainer.Margin;
                margin.Left = 0;
                PageContainer.Margin = margin;

                if(newState == CurrentState.SmallResolution_ShowMenu)
                {
                    ShowMenu();
                }
                else
                {
                    HideMenu();
                }
            }
        }

        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            UpdateMenuDispositionBasedOnDisplaySize();
        }

        private void UpdateMenuDispositionBasedOnDisplaySize()
        {
            //note: another way to get the display width commented below:
            //Rect windowBounds = Window.Current.Bounds;
            //double displayWidth = windowBounds.Width;
            if (this.ActualWidth > 560)
            {
                GoToState(CurrentState.LargeResolution_SeeBothMenuAndPage);
            }
            else if (currentState == CurrentState.SmallResolution_ShowMenu)
            {
                GoToState(CurrentState.SmallResolution_ShowMenu);
            }
            else
            {
                GoToState(CurrentState.SmallResolution_HideMenu);

            }
        }

        void ShowMenu()
        {
            ((TranslateTransform)PageContainer.RenderTransform).X = 180;
            ((TranslateTransform)ButtonToHideOrShowMenu.RenderTransform).X = 180;
            ((TranslateTransform)MenuBorder.RenderTransform).X = 0;
            currentState = CurrentState.SmallResolution_ShowMenu;
        }

        void HideMenu()
        {
            ((TranslateTransform)PageContainer.RenderTransform).X = 0;
            ((TranslateTransform)ButtonToHideOrShowMenu.RenderTransform).X = 0;
            ((TranslateTransform)MenuBorder.RenderTransform).X = -180;
            currentState = CurrentState.SmallResolution_HideMenu;
        }

        void ButtonHideOrShowMenu_Click(object sender, RoutedEventArgs e)
        {
            if (currentState == CurrentState.SmallResolution_ShowMenu)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }

        #endregion
    }
}
