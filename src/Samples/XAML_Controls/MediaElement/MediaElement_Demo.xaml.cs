using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

namespace OpenSilver.Samples.Showcase
{
    public partial class MediaElement_Demo : UserControl
    {
        public MediaElement_Demo()
        {
            this.InitializeComponent();
        }

        void ButtonToPlayAudio_Click(object sender, RoutedEventArgs e)
        {
            MediaElementForAudio.Play();
        }

        void ButtonToPauseAudio_Click(object sender, RoutedEventArgs e)
        {
            MediaElementForAudio.Pause();
        }

        void ButtonToPlayVideo_Click(object sender, RoutedEventArgs e)
        {
            MediaElementForVideo.Play();
        }

        void ButtonToPauseVideo_Click(object sender, RoutedEventArgs e)
        {
            MediaElementForVideo.Pause();
        }
    }
}
