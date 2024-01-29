namespace OpenSilver.Samples.Showcase

open System
open System.IO
open System.IO.IsolatedStorage
open System.Text
open System.Windows
open System.Windows.Controls
open OpenSilver

type IsolatedStorageFile_Demo() as this =
    inherit IsolatedStorageFile_DemoXaml()

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
            else false
        else false

    do
        this.InitializeComponent()

    member private this.ButtonSaveToIsolatedStorageFile_Click(sender: obj, e: RoutedEventArgs) =
        if not (DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer()) then
            let fileName = "SampleFile.txt"
            let data = this.TextBoxFileStorageDemo.Text

            use storage = IsolatedStorageFile.GetUserStoreForAssembly()
            let fs =
                storage.CreateFile(fileName)
                |> Option.ofObj

            match fs with
            | Some(fs) ->
                let encoding = UTF8Encoding()
                let bytes = encoding.GetBytes(data)
                fs.Write(bytes, 0, bytes.Length)
                fs.Close()
                MessageBox.Show("A new file named SampleFile.txt was successfully saved to the storage.") |> ignore
            | None ->
                MessageBox.Show("Unable to save the file SampleFile.txt to the storage.") |> ignore

    member private this.ButtonLoadFromIsolatedStorageFile_Click(sender: obj, e: RoutedEventArgs) =
        if not (DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer()) then
            let fileName = "SampleFile.txt"

            use storage = IsolatedStorageFile.GetUserStoreForAssembly()
            if storage.FileExists(fileName) then
                let fs =
                    storage.OpenFile(fileName, System.IO.FileMode.Open)
                    |> Option.ofObj

                match fs with
                | Some(fs) ->
                    let sr = new StreamReader(fs)
                    let data = sr.ReadToEnd()
                    MessageBox.Show("The following text was read from the file SampleFile.txt located in the storage: " + data) |> ignore
                | None ->
                    MessageBox.Show("Unable to load the file SampleFile.txt from the storage.") |> ignore
            else
                MessageBox.Show("No file named SampleFile.txt was found in the storage.") |> ignore
