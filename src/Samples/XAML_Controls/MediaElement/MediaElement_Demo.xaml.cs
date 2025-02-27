using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("media", "video", "audio", "multimedia", "playback", "play")]
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
