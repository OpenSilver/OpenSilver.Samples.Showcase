Imports System.Text.Json
Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("JSON", "serialization", "deserialization", "serialize")>
    Partial Public Class JSON_Serializer_Demo
        Inherits UserControl

        Private ReadOnly _product As Product
        Private _json As String

        Public Sub New()
            InitializeComponent()

            _product = New Product() With {
                .Name = "TestProduct",
                .ProductType = ProductType.B2C,
                .Price = 12.5,
                .Count = 341,
                .IsAvailable = True,
                .Sizes = {"Small", "Medium", "Large"},
                .Features = New List(Of Feature) From {
                    New Feature With {.Name = "TestFeature1"},
                    New Feature With {.Name = "TestFeature2"},
                    New Feature With {.Name = "TestFeature3"}
                },
                .ReleaseDate = DateTime.Now
            }
        End Sub

        Private Sub Button_Click_Serialization(sender As Object, e As RoutedEventArgs)
            _json = JsonSerializer.Serialize(_product, New JsonSerializerOptions With {.WriteIndented = True})
            MessageBox.Show(_json)
        End Sub

        Private Sub Button_Click_StronglyTypedDeserialization(sender As Object, e As RoutedEventArgs)
            If Not String.IsNullOrEmpty(_json) Then
                Dim deserializedProduct As Product = JsonSerializer.Deserialize(Of Product)(_json)
                MessageBox.Show("Name of the second feature: " & deserializedProduct.Features(1).Name &
                                vbLf & "Name of the third available size: " & deserializedProduct.Sizes(2) &
                                vbLf & "Release date: " & deserializedProduct.ReleaseDate.ToString())
            Else
                MessageBox.Show("Please click on the Serialize button first.")
            End If
        End Sub

        Private Sub Button_Click_DynamicDeserialization(sender As Object, e As RoutedEventArgs)
            If Not String.IsNullOrEmpty(_json) Then
                Dim deserializedObject = JsonDocument.Parse(_json).RootElement
                MessageBox.Show("Product name: " & deserializedObject.GetProperty("Name").GetString() &
                                vbLf & "Name of the second feature: " & deserializedObject.GetProperty("Features")(1).GetProperty("Name").GetString() &
                                vbLf & "Name of the third available size: " & deserializedObject.GetProperty("Sizes")(2).GetString())
            Else
                MessageBox.Show("Please click on the Serialize button first.")
            End If
        End Sub

        Public Class Product
            Public Property Name As String
            Public Property ProductType As ProductType
            Public Property Price As Double
            Public Property Count As Integer
            Public Property IsAvailable As Boolean
            Public Property Sizes As String()
            Public Property Features As List(Of Feature)
            Public Property ReleaseDate As DateTime
        End Class

        Public Class Feature
            Public Property Name As String
        End Class

        Public Enum ProductType
            B2B
            B2C
        End Enum

    End Class

End Namespace
