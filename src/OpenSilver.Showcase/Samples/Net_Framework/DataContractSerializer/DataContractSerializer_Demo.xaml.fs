namespace OpenSilver.Showcase

open System
open System.IO
open System.Runtime.Serialization
open System.Windows
open OpenSilver.Showcase.Search

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

[<SearchKeywords("serialization", "deserialization", "data contract", "XML", "data exchange")>]
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

        let xml =
            use stream1 = new MemoryStream()
            dataContractSerializer.WriteObject(stream1, _classToSerialize)
            stream1.Seek(0L, SeekOrigin.Begin) |> ignore
            use streamReader = new StreamReader(stream1)
            streamReader.ReadToEnd()

        // Display the result of the serialization:
        MessageBox.Show(sprintf "Result of the serialization:%s%s%s" Environment.NewLine Environment.NewLine xml) |> ignore

        // Deserialize:
        let dataContractSerializer = new DataContractSerializer(typeof<ClassToSerialize>)

        let deserializedObject =
            use stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml))
            dataContractSerializer.ReadObject(stream) :?> ClassToSerialize

        // Display the result of the deserialization:
        this.SerializationDestinationPanel.DataContext <- deserializedObject
