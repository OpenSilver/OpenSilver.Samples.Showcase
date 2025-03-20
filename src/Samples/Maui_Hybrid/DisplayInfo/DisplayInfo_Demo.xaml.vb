Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.Devices
Imports OpenSilver.Samples.Showcase.Search
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native", "screen", "information", "pixel", "display")>
    Partial Public Class DisplayInfo_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "The display info sample is not supported in the browser.",
                    .TextWrapping = TextWrapping.Wrap
                })
            End If
        End Sub

        Private Sub ButtonGetDisplayInfo_Click(sender As Object, e As RoutedEventArgs)
            MainThread.InvokeOnMainThreadAsync(Sub()
                                                   Try
                                                       Dim sb As New StringBuilder()

                                                       sb.AppendLine($"Pixel width: {DeviceDisplay.Current.MainDisplayInfo.Width} / Pixel Height: {DeviceDisplay.Current.MainDisplayInfo.Height}")
                                                       sb.AppendLine($"Density: {DeviceDisplay.Current.MainDisplayInfo.Density}")
                                                       sb.AppendLine($"Orientation: {DeviceDisplay.Current.MainDisplayInfo.Orientation}")
                                                       sb.AppendLine($"Rotation: {DeviceDisplay.Current.MainDisplayInfo.Rotation}")
                                                       sb.AppendLine($"Refresh Rate: {DeviceDisplay.Current.MainDisplayInfo.RefreshRate}")

                                                       DisplayInfo.Text = sb.ToString()
                                                   Catch
                                                   End Try
                                               End Sub)
        End Sub
    End Class
End Namespace
