using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Samples.Showcase
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        void ButtonXamlControls_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/XAML_Controls/XAML_Controls", UriKind.Relative);
            PageContainer.Source = uri;
        }

        void ButtonXamlFeatures_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/XAML_Features/XAML_Features", UriKind.Relative);
            PageContainer.Source = uri;
        }

        void ButtonNetFramework_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/Net_Framework/Net_Framework", UriKind.Relative);
            PageContainer.Source = uri;
        }

        void ButtonClientServer_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/Client-Server/Client_Server", UriKind.Relative);
            PageContainer.Source = uri;
        }

        void ButtonInterop_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/Interop_Samples/Interop_Samples", UriKind.Relative);
            PageContainer.Source = uri;
        }

        void ButtonExtensions_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/Extensions/Extensions", UriKind.Relative);
            PageContainer.Source = uri;
        }

        void ButtonPerformance_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/Performance/Performance", UriKind.Relative);
            PageContainer.Source = uri;
        }
    }
}
