﻿Imports Newtonsoft.Json
Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("JSON", "serialization", "deserialization", "serialize")>
    Partial Public Class JSON_Serializer_Demo
        Inherits UserControl
        Private _json As String
        Private _product As Product

        Public Sub New()
            Me.InitializeComponent()

            _product = New Product() With {
    .Name = "TestProduct",
    .ProductType = ProductType.B2C,
    .Price = 12.5,
    .Count = 341,
    .IsAvailable = True,
    .Sizes = New String() {"Small", "Medium", "Large"},
                    .Features = New List(Of Feature)() From {
        New Feature() With {
            .Name = "TestFeature1"
        },
        New Feature() With {
            .Name = "TestFeature2"
        },
        New Feature() With {
            .Name = "TestFeature3"
        }
    },
    .ReleaseDate = Date.Now
}
        End Sub

        Private Sub Button_Click_Serialization(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ' Serialize:
            _json = JsonConvert.SerializeObject(_product)

            ' Indent:
            Dim indentedJson = _json.Replace(",", "," & Microsoft.VisualBasic.Constants.vbLf)

            ' Display the result:
            MessageBox.Show(indentedJson)

            ' 
            ' // Expected Result:
            ' {  
            ' "Name":"TestProduct",
            ' "ProductType":"B2C",
            ' "Price":12.5,
            ' "Count":341,
            ' "IsAvailable":true,
            ' "Sizes":[  
            ' "Small",
            ' "Medium",
            ' "Large"
            ' ],
            ' "Features":[  
            ' {  
            ' "Name":"TestFeature1"
            ' },
            ' {  
            ' "Name":"TestFeature2"
            ' },
            ' {  
            ' "Name":"TestFeature3"
            ' }
            ' ],
            ' "ReleaseDate":"2017-04-10T16:26:41.754Z"
            ' }
            ' 
        End Sub

        Private Async Sub Button_Click_StronglyTypedDeserialization(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not String.IsNullOrEmpty(_json) Then
                Dim deserializedProduct = Await JsonConvert.DeserializeObject(Of Product)(_json)

                MessageBox.Show("Name of the second feature: " & deserializedProduct.Features(1).Name)
                MessageBox.Show("Name of the third available size: " & deserializedProduct.Sizes(2))

                ' Expected Result: "Name of the second feature: TestFeature2"
                ' Expected Result: "Name of the third available size: Large"
                Call MessageBox.Show("Release date: " & deserializedProduct.ReleaseDate.ToString())
            Else
                MessageBox.Show("Please click on the Serialize button first.")
            End If
        End Sub

        Private Async Sub Button_Click_DynamicDeserialization(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not String.IsNullOrEmpty(_json) Then
                Dim deserializedObject = Await DeserializeObject(_json)

                Call MessageBox.Show("Product name: " & deserializedObject.Item("Name").Value.ToString())
                Call MessageBox.Show("Name of the second feature: " & deserializedObject.Item("Features").Item(1).Item("Name").Value.ToString())

                ' Expected Result: "Product name: TestProduct"
                ' Expected Result: "Name of the second feature: TestFeature2"
                ' Expected Result: "Name of the third available size: Large"
                Call MessageBox.Show("Name of the third available size: " & deserializedObject.Item("Sizes").Item(2).Value.ToString())
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
            Public Property ReleaseDate As Date
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
