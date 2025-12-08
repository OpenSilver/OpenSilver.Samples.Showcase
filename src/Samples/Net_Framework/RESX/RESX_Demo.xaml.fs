namespace OpenSilver.Samples.Showcase

open System.Windows
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("resources", "localization", "RESX", "translation")>]
type RESX_Demo() as this =
    inherit RESX_DemoXaml()

    do this.InitializeComponent()

    member _.ButtonReadResource_Click(_sender: obj, _e: RoutedEventArgs) = []
        //MessageBox.Show($"SampleResourceFile.InfoMessage: {SampleResourceFileFs.InfoMessage}") |> ignore

    member _.Hyperlink_Click(_sender: obj, _e: RoutedEventArgs) = []
        //MainPage.Current.PageContainer.Navigate(Uri("/XAML_Features/MarkupExtensions", UriKind.Relative))
