using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Devices;
using OpenSilver.Samples.Showcase.Search;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CSHTML5.Native.Html.Controls;
using System.ComponentModel;
using Microsoft.Maui.Devices.Sensors;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("maui", "hybrid", "device", "native", "photo", "image", "picture")]
    public partial class Camera_Demo : UserControl
    {
        public Camera_Demo()
        {
            this.InitializeComponent();
            //if (DeviceInfo.Current.Platform == DevicePlatform.Unknown)
            //{
            //    SampleContainer.Children.Clear();
            //    SampleContainer.Children.Add(new TextBlock() { Text = "The camera app is not supported in the browser.", TextWrapping = TextWrapping.Wrap });
            //}
        }

        private async void TakePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (DeviceInfo.Current.Platform == DevicePlatform.Unknown)
            {
                GeneralContainer.Width = 500;
                GetPhoto_Browser();
            }
            else
            {
                GetPhoto_Maui();
            }

        }

        #region Browser version
        Grid _videoGrid;
        bool _isWatching = false;


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            StopWatchingCamera();
            GetVideoGrid.Visibility = Visibility.Collapsed;
        }

        private void GetPhoto_Browser()
        {
            GetVideoGrid.Visibility = Visibility.Visible;
            StartWatchingCamera();
        }

        /// <summary>
        /// Creates or gets the elements to show the current camera view, and to take a picture from it.
        /// </summary>
        private Grid GetVideoGrid
        {
            get
            {
                if (_videoGrid == null)
                {
                    _videoGrid = new Grid();

                    //Create a htmlPresenter to hold the video element that will later show the feed from the camera:
                    HtmlPresenter htmlPresenter = new HtmlPresenter();
                    htmlPresenter.Width = 480;
                    htmlPresenter.Html = "<video id=\"video\" width=\"480\" height=\"270\" style=\"width: 480px;\">Video stream not available.</video>";



                    //Add the buttons to take a still picture from the video element and to close the camera:
                    StackPanel stack = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Bottom,
                    };
                    Button acceptButton = new Button()
                    {
                        Content = "Accept",
                        Margin = new Thickness(10)
                    };
                    acceptButton.Click += AcceptButton_Click;

                    Button cancelButton = new Button()
                    {
                        Content = "Cancel",
                        Margin = new Thickness(10)
                    };
                    cancelButton.Click += CancelButton_Click;

                    stack.Children.Add(acceptButton);
                    stack.Children.Add(cancelButton);

                    _videoGrid.Children.Add(htmlPresenter);
                    _videoGrid.Children.Add(stack);

                    SampleContainer.Children.Add(_videoGrid);
                }
                return _videoGrid;
            }
        }

        /// <summary>
        /// This method uses navigator.mediaDevices.getUserMedia to retrieve the feed from the camera and set it as the source of the video element.
        /// </summary>
        private void StartWatchingCamera()
        {
            if (!_isWatching)
            {
                _isWatching = true;
                Dispatcher.BeginInvoke(() =>
                {
                    Interop.ExecuteJavaScriptAsync(@"
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
}) ())", (Action)OnFeedGet, (Action)OnFeedError);
                });
            }
        }

        private void OnFeedError()
        {
            _isWatching = false;
            FeatureNotAllowedTextBlock.Text = "Unable to access the camera. Please ensure that a camera is available and that you have granted the necessary permissions to this app.";
            FeatureNotAllowedTextBlock.Visibility = Visibility.Visible;
        }

        private void OnFeedGet()
        {
            _isWatching = true;
            FeatureNotAllowedTextBlock.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// This method simply replaces the video feed with null.
        /// </summary>
        private void StopWatchingCamera()
        {
            Interop.ExecuteJavaScriptAsync(@"
let v = document.getElementById(""video"");
    v.srcObject = null;");
            _isWatching = false;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            // Use a html canvas to draw a picture of the video, then translate it to a base64 string.
            var imgData = Interop.ExecuteJavaScriptGetResult<string>(@"
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
}) ())"
);
            //Use the base64 string as source for the image:
            PhotoTakenImage.Source = LoadImageFromBase64(imgData);
            PhotoTakenImage.Visibility = Visibility.Visible;

            //close the camera
            StopWatchingCamera();
            GetVideoGrid.Visibility = Visibility.Collapsed;
        }

        public static BitmapImage LoadImageFromBase64(string base64String)
        {
            try
            {
                // Remove the prefix if present
                string cleanBase64 = base64String.Contains(",") ? base64String.Split(',')[1] : base64String;

                byte[] imageBytes = Convert.FromBase64String(cleanBase64);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.SetSource(ms);
                    return bitmap;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");
                return null;
            }
        }


        #endregion
        private void GetPhoto_Maui()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // Check the current location permission status.
                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

                // If permission is not granted, request it.
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Camera>();
                }

                // If permission is granted, fetch the location.
                if (status == PermissionStatus.Granted)
                {
                    try
                    {
                        if (MediaPicker.Default.IsCaptureSupported)
                        {
                            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                            if (photo != null)
                            {
                                FeatureNotAllowedTextBlock.Visibility = Visibility.Collapsed;
                                using (var stream = await photo.OpenReadAsync())
                                {
                                    BitmapImage bitmapImage = new BitmapImage();
                                    bitmapImage.SetSource(stream);
                                    PhotoTakenImage.Source = bitmapImage;
                                    PhotoTakenImage.Visibility = Visibility.Visible;
                                }

                            }
                        }
                    }
                    catch (PermissionException ex)
                    {
                        FeatureNotAllowedTextBlock.Visibility = Visibility.Visible;
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            });
        }
    }
}
