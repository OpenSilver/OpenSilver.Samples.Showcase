namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Runtime.Serialization
open System.Text
open System.Xml
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.Foundation
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Navigation
#endif

[<DataContract>]
type ClassToSerialize() =
    [<field: DataMember>]
    member val TextField = "" with get, set
    [<field: DataMember>]
    member val DateField = DateTime.Now with get, set
    [<field: DataMember>]
    member val GuidField = Guid.NewGuid() with get, set
    [<field: DataMember>]
    member val BooleanField = false with get, set

type DataContractSerializer_Demo() as this =
    inherit DataContractSerializer_DemoXaml()

    let mutable _classToSerialize : ClassToSerialize = ClassToSerialize()

    do
        this.InitializeComponent()

        _classToSerialize <- ClassToSerialize(
            TextField = "Some Text", 
            DateField = DateTime.Now, 
            GuidField = Guid.NewGuid(), 
            BooleanField = true
        )

        this.SerializationSourcePanel.DataContext <- _classToSerialize

    member this.ButtonSerializeDeserialize_Click (sender: obj, e: RoutedEventArgs) =
        // Serialize:
        let dataContractSerializer = new DataContractSerializer(typeof<ClassToSerialize>)
#if OPENSILVER
        let xml =
            use stream1 = new MemoryStream()
            dataContractSerializer.WriteObject(stream1, _classToSerialize)
            stream1.Seek(0L, SeekOrigin.Begin) |> ignore
            use streamReader = new StreamReader(stream1)
            streamReader.ReadToEnd()
#else
        let xml = _classToSerialize |> TypeSerializer.SerializeToString
#endif

        // Display the result of the serialization:
        MessageBox.Show(sprintf "Result of the serialization:%s%s%s" Environment.NewLine Environment.NewLine xml) |> ignore

        // Deserialize:
        let dataContractSerializer = new DataContractSerializer(typeof<ClassToSerialize>)
#if OPENSILVER
        let deserializedObject =
            use stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml))
            dataContractSerializer.ReadObject(stream) :?> ClassToSerialize
#else
        let deserializedObject = xml |> TypeSerializer.DeserializeFromString<ClassToSerialize>
#endif

        // Display the result of the deserialization:
        this.SerializationDestinationPanel.DataContext <- deserializedObject


    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "DataContractSerializer_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DataContractSerializer/DataContractSerializer_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "DataContractSerializer_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DataContractSerializer/DataContractSerializer_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "DataContractSerializer_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DataContractSerializer/DataContractSerializer_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "DataContractSerializer_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DataContractSerializer/DataContractSerializer_Demo.xaml.fs")
        ])
