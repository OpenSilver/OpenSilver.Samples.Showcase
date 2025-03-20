Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.Devices.Sensors
Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search
Imports Microsoft.Maui.Devices
Imports System

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native", "coordinates", "longitude", "latitude", "information")>
    Partial Public Class Geolocation
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub GetButton_Click(sender As Object, e As RoutedEventArgs)
            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                GetLocation_Browser()
            Else
                GetLocation_Maui()
            End If
        End Sub

        Private Sub GetLocation_Browser()
            Interop.ExecuteJavaScriptAsync(
                "navigator.geolocation.getCurrentPosition((pos) => {$0(""lat: ""+ pos.coords.latitude + ""\r\nlong: "" + pos.coords.longitude)})",
                CType(AddressOf ReceiveLocation, Action(Of String))
            ).ToString()
        End Sub

        Private Sub ReceiveLocation(obj As String)
            LocationTextBlock.Text = obj
        End Sub

        Private Async Sub GetLocation_Maui()
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         ' Check the current location permission status.
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.LocationWhenInUse)()

                                                         ' If permission is not granted, request it.
                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.LocationWhenInUse)()
                                                         End If

                                                         ' If permission is granted, fetch the location.
                                                         If status = PermissionStatus.Granted Then
                                                             Try
                                                                 Dim request As New GeolocationRequest(GeolocationAccuracy.Medium)
                                                                 Dim location As Location = Await Microsoft.Maui.Devices.Sensors.Geolocation.Default.GetLocationAsync(request)

                                                                 If location IsNot Nothing Then
                                                                     ' Switch back to the OpenSilver thread to update the UI.
                                                                     Dispatcher.BeginInvoke(Sub()
                                                                                                LocationTextBlock.Text = location.ToString()
                                                                                            End Sub)
                                                                 End If
                                                             Catch fnsEx As FeatureNotSupportedException
                                                                 LocationTextBlock.Text = "This device does not support Geolocation."
                                                             Catch fneEx As FeatureNotEnabledException
                                                                 LocationTextBlock.Text = "The Geolocation feature is not enabled."
                                                             Catch pEx As PermissionException
                                                                 LocationTextBlock.Text = "This sample requires permission to use Geolocation."
                                                             Catch ex As Exception
                                                                 LocationTextBlock.Text = "Could not get location."
                                                             End Try
                                                         Else
                                                             LocationTextBlock.Text = "This sample requires permission to use Geolocation."
                                                         End If
                                                     End Function)
        End Sub
    End Class
End Namespace
