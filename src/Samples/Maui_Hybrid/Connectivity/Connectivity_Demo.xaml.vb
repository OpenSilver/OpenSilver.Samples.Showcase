Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.Networking
Imports Microsoft.Maui.Devices
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native", "connexion", "status")>
    Partial Public Class Connectivity_Demo
        Inherits UserControl

        Private ReadOnly _allConnectionProfiles As New List(Of ConnectionProfile) From {
            ConnectionProfile.Cellular,
            ConnectionProfile.Bluetooth,
            ConnectionProfile.Ethernet,
            ConnectionProfile.WiFi,
            ConnectionProfile.Unknown
        }

        Public Sub New()
            InitializeComponent()

            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "The connectivity sample is not supported in the browser.",
                    .TextWrapping = TextWrapping.Wrap
                })
            End If
        End Sub

        Private Async Sub CheckConnectivityButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         Dim profiles As IEnumerable(Of ConnectionProfile) = Connectivity.Current.ConnectionProfiles

                                                         Dim connectivitiesText As String = "Connections active:"
                                                         For Each profile As ConnectionProfile In _allConnectionProfiles
                                                             connectivitiesText &= vbCrLf & " - " & profile.ToString() & ": " & If(profiles.Contains(profile), "Yes", "No")
                                                         Next

                                                         ConnectivityTextBlock.Text = connectivitiesText
                                                     End Function)
        End Sub
    End Class
End Namespace
