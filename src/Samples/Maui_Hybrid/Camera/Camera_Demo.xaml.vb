Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.Media
Imports Microsoft.Maui.Storage
Imports Microsoft.Maui.Devices
Imports OpenSilver.Samples.Showcase.Search
Imports System
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media.Imaging
Imports CSHTML5.Native.Html.Controls
Imports System.ComponentModel
Imports Microsoft.Maui.Devices.Sensors

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native", "photo", "image", "picture")>
    Partial Public Class Camera_Demo
        Inherits UserControl

        Private _videoGrid As Grid
        Private _isWatching As Boolean = False

        Public Sub New()
            InitializeComponent()
            ' If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
            '     SampleContainer.Children.Clear()
            '     SampleContainer.Children.Add(New TextBlock() With {.Text = "The camera app is not supported in the browser.", .TextWrapping = TextWrapping.Wrap})
            ' End If
        End Sub

        Private Async Sub TakePhotoButton_Click(sender As Object, e As RoutedEventArgs)
            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                GeneralContainer.Width = 500
                GetPhoto_Browser()
            Else
                GetPhoto_Maui()
            End If
        End Sub

#Region "Browser Version"

        Private Sub CancelButton_Click(sender As Object, e As RoutedEventArgs)
            StopWatchingCamera()
            GetVideoGrid.Visibility = Visibility.Collapsed
        End Sub

        Private Sub GetPhoto_Browser()
            GetVideoGrid.Visibility = Visibility.Visible
            StartWatchingCamera()
        End Sub

        Private ReadOnly Property GetVideoGrid As Grid
            Get
                If _videoGrid Is Nothing Then
                    _videoGrid = New Grid()

                    ' Create a HtmlPresenter to hold the video element
                    Dim htmlPresenter As New HtmlPresenter() With {
                        .Width = 480,
                        .Html = "<video id=""video"" width=""480"" height=""270"" style=""width: 480px;"">Video stream not available.</video>"
                    }

                    ' Add buttons for capturing and canceling
                    Dim stack As New StackPanel() With {
                        .Orientation = Orientation.Horizontal,
                        .HorizontalAlignment = HorizontalAlignment.Right,
                        .VerticalAlignment = VerticalAlignment.Bottom
                    }

                    Dim acceptButton As New Button() With {
                        .Content = "Accept",
                        .Margin = New Thickness(10)
                    }
                    AddHandler acceptButton.Click, AddressOf AcceptButton_Click

                    Dim cancelButton As New Button() With {
                        .Content = "Cancel",
                        .Margin = New Thickness(10)
                    }
                    AddHandler cancelButton.Click, AddressOf CancelButton_Click

                    stack.Children.Add(acceptButton)
                    stack.Children.Add(cancelButton)

                    _videoGrid.Children.Add(htmlPresenter)
                    _videoGrid.Children.Add(stack)

                    SampleContainer.Children.Add(_videoGrid)
                End If
                Return _videoGrid
            End Get
        End Property

        Private Sub StartWatchingCamera()
            If Not _isWatching Then
                _isWatching = True
                Dispatcher.BeginInvoke(Sub()
                                           Interop.ExecuteJavaScriptAsync("
((function () {
navigator.mediaDevices
  .getUserMedia({ video: true, audio: false })
  .then((stream) => {
let v = document.getElementById(""video"");
    v.srcObject = stream;
    v.play();
    $0();
  })
  .catch((err) => {
    console.error(`An error occurred while getting camera feed: ${err}`);
    $1();
  });
}) ())", CType(AddressOf OnFeedGet, Action), CType(AddressOf OnFeedError, Action))
                                       End Sub)
            End If
        End Sub

        Private Sub OnFeedError()
            _isWatching = False
            FeatureNotAllowedTextBlock.Text = "Unable to access the camera. Please ensure that a camera is available and that you have granted the necessary permissions to this app."
            FeatureNotAllowedTextBlock.Visibility = Visibility.Visible
        End Sub

        Private Sub OnFeedGet()
            _isWatching = True
            FeatureNotAllowedTextBlock.Visibility = Visibility.Collapsed
        End Sub

        Private Sub StopWatchingCamera()
            Interop.ExecuteJavaScriptAsync("
let v = document.getElementById(""video"");
    v.srcObject = null;")
            _isWatching = False
        End Sub

        Private Sub AcceptButton_Click(sender As Object, e As RoutedEventArgs)
            Dim imgData As String = Interop.ExecuteJavaScriptGetResult(Of String)("
((function () {
var canvas = document.createElement(""canvas"");
var v = document.getElementById(""video"");
var width = v.videoWidth;
var height = v.videoHeight;
canvas.setAttribute(""width"", width);
canvas.setAttribute(""height"", height);
var data = """";
const context = canvas.getContext(""2d"");

  if (width && height) {
    canvas.width = width;
    canvas.height = height;
    context.drawImage(video, 0, 0, width, height);

    data = canvas.toDataURL(""image/png"");
}
return data;
}) ())")

            PhotoTakenImage.Source = LoadImageFromBase64(imgData)
            PhotoTakenImage.Visibility = Visibility.Visible

            StopWatchingCamera()
            GetVideoGrid.Visibility = Visibility.Collapsed
        End Sub

        Public Shared Function LoadImageFromBase64(base64String As String) As BitmapImage
            Try
                Dim cleanBase64 As String = If(base64String.Contains(","), base64String.Split(","c)(1), base64String)

                Dim imageBytes As Byte() = Convert.FromBase64String(cleanBase64)
                Using ms As New MemoryStream(imageBytes)
                    Dim bitmap As New BitmapImage()
                    bitmap.SetSource(ms)
                    Return bitmap
                End Using
            Catch ex As Exception
                Console.WriteLine($"Error loading image: {ex.Message}")
                Return Nothing
            End Try
        End Function

#End Region

        Private Async Sub GetPhoto_Maui()
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.Camera)()

                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.Camera)()
                                                         End If

                                                         If status = PermissionStatus.Granted Then
                                                             Try
                                                                 If MediaPicker.Default.IsCaptureSupported Then
                                                                     Dim photo As FileResult = Await MediaPicker.Default.CapturePhotoAsync()

                                                                     If photo IsNot Nothing Then
                                                                         FeatureNotAllowedTextBlock.Visibility = Visibility.Collapsed
                                                                         Using stream As Stream = Await photo.OpenReadAsync()
                                                                             Dim bitmapImage As New BitmapImage()
                                                                             bitmapImage.SetSource(stream)
                                                                             PhotoTakenImage.Source = bitmapImage
                                                                             PhotoTakenImage.Visibility = Visibility.Visible
                                                                         End Using
                                                                     End If
                                                                 End If
                                                             Catch ex As PermissionException
                                                                 FeatureNotAllowedTextBlock.Visibility = Visibility.Visible
                                                             Catch ex As Exception
                                                                 MessageBox.Show(ex.Message)
                                                             End Try
                                                         End If
                                                     End Function)
        End Sub
    End Class
End Namespace
