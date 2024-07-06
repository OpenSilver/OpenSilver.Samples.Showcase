namespace OpenSilver.Samples.Showcase

open OpenSilver
open System
open System.Collections.Generic
open System.IO
open System.IO.IsolatedStorage
open System.Linq
open System.Windows
open System.Windows.Controls

type IsolatedStorageSettings_Demo() as this =
    inherit IsolatedStorageSettings_DemoXaml()

    let IsRunningFromLocalFileSystemOnInternetExplorer() =
        Convert.ToBoolean(Interop.ExecuteJavaScript(@"window.IE_VERSION && document.location.protocol === ""file:"""))

    let DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer() = false

    do
        this.InitializeComponent()

    member private this.ButtonSaveToIsolatedStorageSettings_Click(sender : obj, e : RoutedEventArgs) =
        if not (DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer()) then
            let key = "SampleKey"
            let value = this.TextBoxIsolatedStorageSettingsDemo.Text
            IsolatedStorageSettings.ApplicationSettings.[key] <- value
            MessageBox.Show("The text was successfully saved to the storage.") |> ignore

    member private this.ButtonLoadFromIsolatedStorageSettings_Click(sender : obj, e : RoutedEventArgs) =
        if not (DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer()) then
            let key = "SampleKey"
            match IsolatedStorageSettings.ApplicationSettings.TryGetValue(key) with
            | true, value -> MessageBox.Show("The following text was read from the storage: " + value) |> ignore
            | _ -> MessageBox.Show("No text was found in the storage.") |> ignore

    member private this.ButtonDeleteFromIsolatedStorageSettings_Click(sender : obj, e : RoutedEventArgs) =
        if not (DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer()) then
            let key = "SampleKey"
            IsolatedStorageSettings.ApplicationSettings.Remove(key) |> ignore
            MessageBox.Show("The text was successfully removed from the storage.") |> ignore
