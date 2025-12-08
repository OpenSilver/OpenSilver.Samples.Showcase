Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.Devices
Imports OpenSilver.Samples.Showcase.Search
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native", "charge", "charging", "status", "information")>
    Partial Public Class Battery_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                ' If we are in a browser that doesn't support the battery status API, show a message instead of the Sample contents.
                Dim isBatteryUnsupported As Boolean = Interop.ExecuteJavaScriptGetResult(Of Boolean)("!navigator.getBattery")

                If isBatteryUnsupported Then
                    SampleContainer.Children.Clear()

                    Dim link As New Hyperlink() With {
                        .NavigateUri = New Uri("https://developer.mozilla.org/en-US/docs/Web/API/Battery_Status_API#browser_compatibility"),
                        .TargetName = "_blank"
                    }
                    link.Inlines.Add("here")

                    Dim tb As New TextBlock() With {
                        .TextWrapping = TextWrapping.Wrap
                    }
                    tb.Inlines.Add("The battery status API is not supported in this browser. Check ")
                    tb.Inlines.Add(link)
                    tb.Inlines.Add(" to see its compatibility table.")

                    SampleContainer.Children.Add(tb)
                End If
            End If
        End Sub

        Private Sub CheckBatteryStatusButton_Click(sender As Object, e As RoutedEventArgs)
            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                CheckBattery_Browser()
            Else
                CheckBattery_Maui()
            End If
        End Sub

        Private Sub CheckBattery_Browser()
            Interop.ExecuteJavaScriptAsync(
                "navigator.getBattery().then((battery) => { $0(""Battery is "" + (battery.charging ? """" : ""not "") + ""charging\r\nCharge level: "" + battery.level * 100 + ""%"", true) })",
                CType(AddressOf UpdateBatteryStatus_Browser, Action(Of String))
            )
        End Sub

        Private Sub UpdateBatteryStatus_Browser(text As String)
            BatteryStatus.Text = text
        End Sub

        Private Async Sub CheckBattery_Maui()
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         ' Check the current location permission status.
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.Battery)()

                                                         ' If permission is not granted, request it.
                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.Battery)()
                                                         End If

                                                         Try
                                                             ' If permission is granted, fetch the battery status.
                                                             If status = PermissionStatus.Granted Then
                                                                 Dim str As String = $"Battery is {Battery.Default.ChargeLevel * 100}% charged." & Environment.NewLine

                                                                 Select Case Battery.Default.State
                                                                     Case BatteryState.Charging
                                                                         str &= "Battery is currently charging"
                                                                     Case BatteryState.Discharging
                                                                         str &= "Charger is not connected and the battery is discharging"
                                                                     Case BatteryState.Full
                                                                         str &= "Battery is full"
                                                                     Case BatteryState.NotCharging
                                                                         str &= "The battery isn't charging"
                                                                     Case BatteryState.NotPresent
                                                                         str &= "Battery is not available"
                                                                     Case Else
                                                                         str &= "Battery is unknown"
                                                                 End Select

                                                                 BatteryStatus.Text = str
                                                             End If
                                                         Catch ex As FeatureNotSupportedException
                                                             BatteryStatus.Text = "The Battery status feature is not supported on this device."
                                                         Catch ex As PermissionException
                                                             BatteryStatus.Text = "This sample requires your permission to check the battery status."
                                                         Catch
                                                             BatteryStatus.Text = "Could not check battery status."
                                                         End Try
                                                     End Function)
        End Sub

    End Class
End Namespace
