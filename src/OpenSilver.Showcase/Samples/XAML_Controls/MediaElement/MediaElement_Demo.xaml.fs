namespace OpenSilver.Showcase

open System.Windows
open OpenSilver.Showcase.Search

[<SearchKeywords("media", "video", "audio", "multimedia", "playback", "play")>]
type MediaElement_Demo() as this =
    inherit MediaElement_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.ButtonToPlayAudio_Click(sender : obj, e : RoutedEventArgs) =
        this.MediaElementForAudio.Play()

    member this.ButtonToPauseAudio_Click(sender : obj, e : RoutedEventArgs) =
        this.MediaElementForAudio.Pause()

    member this.ButtonToPlayVideo_Click(sender : obj, e : RoutedEventArgs) =
        this.MediaElementForVideo.Play()

    member this.ButtonToPauseVideo_Click(sender : obj, e : RoutedEventArgs) =
        this.MediaElementForVideo.Pause()
