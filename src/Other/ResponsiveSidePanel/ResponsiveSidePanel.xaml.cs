using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    [ContentProperty("ChildElement")]
    public partial class ResponsiveSidePanel : UserControl
    {
        //This class contains all that we use to make the menu on the left disappear when the screen is too small.

        const double MinimumResolutionForMenu = 750d;

        CurrentState _currentState;

        enum CurrentState
        {
            Unset, // Initial value
            LargeResolution_SeeBothMenuAndPage, // This corresponds to tablets and other devices with high resolution. In this case we see both the menu and the page.
            SmallResolution_ShowMenu, // This corresponds to smartphones and other devices with low resolution. In this case we see the menu.
            SmallResolution_HideMenu // This corresponds to smartphones and other devices with low resolution. In this case we do not see the menu.
        }

        public ResponsiveSidePanel()
        {
            this.InitializeComponent();

            this.Loaded += ResponsiveSidePanel_Loaded;
            this.Unloaded += ResponsiveSidePanel_Unloaded;
        }

        private void ResponsiveSidePanel_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Window_SizeChanged;
            Window.Current.SizeChanged += Window_SizeChanged;

            UpdateMenuDispositionBasedOnDisplaySize();
        }

        private void ResponsiveSidePanel_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Window_SizeChanged;
        }


        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            UpdateMenuDispositionBasedOnDisplaySize();
        }

        void GoToState(CurrentState newState)
        {
            if (newState != _currentState)
            {
                if (newState == CurrentState.LargeResolution_SeeBothMenuAndPage)
                {
                    // Show the menu:
                    MenuBorder.Visibility = Visibility.Visible;

                    // Hide the button to hide/show the menu:
                    ButtonToHideOrShowMenu.Visibility = Visibility.Collapsed;
                }
                else
                {
                    // Revert the changes that are specific to the CurrentState.LargeResolution_SeeBothMenuAndPage state.

                    // Show the button to hide/show the menu:
                    ButtonToHideOrShowMenu.Visibility = Visibility.Visible;

                    if (newState == CurrentState.SmallResolution_ShowMenu)
                    {
                        // Show the menu:
                        MenuBorder.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        // Hide the menu:
                        MenuBorder.Visibility = Visibility.Collapsed;
                    }
                }
                _currentState = newState;
            }
        }

        private void UpdateMenuDispositionBasedOnDisplaySize()
        {
            Rect windowBounds = Window.Current.Bounds;
            double displayWidth = windowBounds.Width;

            if (!double.IsNaN(displayWidth) && displayWidth > MinimumResolutionForMenu)
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

        public void CollapseIfMobile()
        {
            if (_currentState == CurrentState.SmallResolution_ShowMenu)
                GoToState(CurrentState.SmallResolution_HideMenu);
        }

        public object ChildElement
        {
            get { return (object)GetValue(ChildElementProperty); }
            set { SetValue(ChildElementProperty, value); }
        }
        public static readonly DependencyProperty ChildElementProperty =
            DependencyProperty.Register("ChildElement", typeof(object), typeof(ResponsiveSidePanel), new PropertyMetadata(null));
    }
}
