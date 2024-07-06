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

    let DisplayWarningIfRunningFromLocalFileSystemOnInternetExplorer() = false

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
