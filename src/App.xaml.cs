using MaterialDesign_Styles_Kit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Media;
#else
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#endif

namespace OpenSilver.Samples.Showcase
{
    public sealed partial class App : Application
    {
        public App()
        {
            this.Resources.Add("AccentColorConverter", new AccentColorConverter());
            this.Resources.Add("DoubleToCornerRadiusConverter", new DoubleToCornerRadiusConverter());
            this.Resources.Add("TextToPlaceholderTextVisibilityConverter", new TextToPlaceholderTextVisibilityConverter());
            this.Resources.Add("MaterialDesign_DefaultAccentColor", new SolidColorBrush(Color.FromArgb(255, 0, 105, 236)));

            this.InitializeComponent();

            // Enter construction logic here...

            var mainPage = new MainPage();
            Window.Current.Content = mainPage;
        }
    }
}
