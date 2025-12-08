namespace OpenSilver.Samples.Showcase

open System.IO.IsolatedStorage
open System.Windows
open System.Windows.Controls

type IsolatedStorageSettings_Demo() as this =
    inherit IsolatedStorageSettings_DemoXaml()

    let IsRunningFromLocalFileSystemOnInternetExplorer() =
        Convert.ToBoolean(Interop.ExecuteJavaScript(@"window.IE_VERSION && document.location.protocol === ""file:"""))

    let DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer() =
        // When running inside Internet Explorer or Edge, the HTML5
        // Storage API is available only if the URL starts with http
        // or https. This method will display a message to the user
        // to inform her about this.
        if CSharpXamlForHtml5.Environment.IsRunningInJavaScript then
            // Execute a piece of JavaScript code:
            if IsRunningFromLocalFileSystemOnInternetExplorer() then
                MessageBox.Show("The local storage - used to persist data - is not available on Internet Explorer or Edge when running the website from the local file system (i.e., the URL starts with 'c:\' or 'file:///'). To solve the problem, please run the website from a web server instead (i.e., the URL must start with 'http://' or 'https://') or test the local storage open a different browser.") |> ignore
                true
            else
                false
        else
            false
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
