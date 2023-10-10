Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Text
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
#Else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#End If

Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class DataContractSerializer_Demo
        Inherits UserControl
        Private _classToSerialize As ClassToSerialize

        Public Sub New()
            Me.InitializeComponent()

            _classToSerialize = New ClassToSerialize() With {
    .TextField = "Some Text",
    .DateField = Date.Now,
    .GuidField = Guid.NewGuid(),
    .BooleanField = True
}
            Me.SerializationSourcePanel.DataContext = _classToSerialize
        End Sub


        <DataContract>
        Public Class ClassToSerialize
            <DataMember>
            Public Property TextField As String
            <DataMember>
            Public Property DateField As Date
            <DataMember>
            Public Property GuidField As Guid
            <DataMember>
            Public Property BooleanField As Boolean
        End Class

        Private Sub ButtonSerializeDeserialize_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ' Serialize:
            Dim dataContractSerializer = New DataContractSerializer(GetType(ClassToSerialize))
#If OPENSILVER
            Dim xml As String = Nothing
            Dim stream1 As MemoryStream = New MemoryStream()
            dataContractSerializer.WriteObject(stream1, _classToSerialize)

            stream1.Seek(0, SeekOrigin.Begin)
            Using streamReader = New StreamReader(stream1)
                xml = streamReader.ReadToEnd()
            End Using
#Else
            var xml = dataContractSerializer.SerializeToString(_classToSerialize);
#End If

            ' Display the result of the serialization:
            MessageBox.Show("Result of the serialization:" & Environment.NewLine & Environment.NewLine & xml)

            ' Deserialize:
            dataContractSerializer = New DataContractSerializer(GetType(ClassToSerialize))
#If OPENSILVER
            Dim deserializedObject As ClassToSerialize = CType(dataContractSerializer.ReadObject(New MemoryStream(Encoding.UTF8.GetBytes(xml))), ClassToSerialize)
#Else
            ClassToSerialize deserializedObject = (ClassToSerialize)dataContractSerializer.DeserializeFromString(xml); 
#End If

            ' Display the result of the deserialization:
            Me.SerializationDestinationPanel.DataContext = deserializedObject
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DataContractSerializer_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DataContractSerializer/DataContractSerializer_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DataContractSerializer_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DataContractSerializer/DataContractSerializer_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DataContractSerializer_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DataContractSerializer/DataContractSerializer_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace
