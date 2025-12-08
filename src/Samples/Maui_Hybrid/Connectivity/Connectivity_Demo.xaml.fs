namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Linq
open System.Windows
open System.Windows.Controls
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Networking
open Microsoft.Maui.Devices
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("maui", "hybrid", "device", "native", "connexion", "status")>]
type public Connectivity_Demo() as this =
    inherit Connectivity_DemoXaml()

    let _allConnectionProfiles =
        [
            ConnectionProfile.Cellular
            ConnectionProfile.Bluetooth
            ConnectionProfile.Ethernet
            ConnectionProfile.WiFi
            ConnectionProfile.Unknown
        ]

    do
        this.InitializeComponent()

        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            this.SampleContainer.Children.Clear()
            let tb = TextBlock(Text = "The connectivity sample is not supported in the browser.", TextWrapping = TextWrapping.Wrap)
            this.SampleContainer.Children.Add(tb)

    member private this.CheckConnectivityButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            Async.StartImmediate(
                async {
                let profiles = Connectivity.Current.ConnectionProfiles

                let mutable connectivitiesText = "Connections active:"
                for profile in _allConnectionProfiles do
                    let status = if profiles.Contains(profile) then "Yes" else "No"
                    connectivitiesText <- connectivitiesText + $"\r\n - {profile}: {status}"

                this.ConnectivityTextBlock.Text <- connectivitiesText
            })
        )
