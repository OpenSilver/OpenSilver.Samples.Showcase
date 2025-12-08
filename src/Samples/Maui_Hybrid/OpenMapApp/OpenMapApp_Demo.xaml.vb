Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.Devices
Imports OpenSilver.Samples.Showcase.Search
Imports System
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native", "location")>
    Partial Public Class OpenMapApp_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "The map app is not supported in the browser.",
                    .TextWrapping = TextWrapping.Wrap
                })
            End If
        End Sub

        Private Async Sub OpenMapAppButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         Dim location As New Microsoft.Maui.Devices.Sensors.Location(48.8584, 2.2945)
                                                         Dim options As New MapLaunchOptions With {.Name = "Eiffel tower"}

                                                         Try
                                                             Await Map.Default.OpenAsync(location, options)
                                                         Catch ex As Exception
                                                             ' No map application available to open
                                                         End Try
                                                     End Function)
        End Sub
    End Class
End Namespace
