Imports OpenSilver.Samples.Showcase.Search
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports Microsoft.Maui.Devices

Namespace OpenSilver.Samples.Showcase.Search
    Public Module SamplesInfoLoader
        ' Keep a mapping of the name to the type, so we can later instantiate the type after a search.
        Private ReadOnly controlTypeMap As New Dictionary(Of String, Type)()

        Public Function GetAllControls() As List(Of SearchableItem)
            Dim controls As New List(Of SearchableItem)()

            Dim types = Assembly.GetExecutingAssembly().
                        GetTypes().
                        Where(Function(t) t.IsClass AndAlso t.GetCustomAttribute(Of SearchKeywordsAttribute)() IsNot Nothing)

            For Each type In types
                Dim attribute = type.GetCustomAttribute(Of SearchKeywordsAttribute)()
                Dim name As String = type.Name
                Dim ignoreType As Boolean = DeviceInfo.Current.Platform = DevicePlatform.Unknown AndAlso
                                            attribute?.Keywords.Contains("maui") = True

                If Not ignoreType Then
                    controls.Add(New SearchableItem With {
                        .Name = name,
                        .Keywords = If(attribute?.Keywords.ToList(), New List(Of String)())
                    })
                End If

                controlTypeMap(name) = type
            Next

            Return controls
        End Function

        Public Function GetControlTypeByName(name As String) As Type
            Dim type As Type = Nothing
            Return If(controlTypeMap.TryGetValue(name, type), type, Nothing)
        End Function
    End Module
End Namespace
