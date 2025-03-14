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

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("maui", "hybrid", "device", "native", "photo", "image", "picture")]
    public partial class Camera_Demo : UserControl
    {
        public Camera_Demo()
        {
            this.InitializeComponent();
            if (DeviceInfo.Current.Platform == DevicePlatform.Unknown)
            {
                SampleContainer.Children.Clear();
                SampleContainer.Children.Add(new TextBlock() { Text = "The camera app is not supported in the browser.", TextWrapping = TextWrapping.Wrap });
            }
        }

        private async void TakePhotoButton_Click(object sender, RoutedEventArgs e)
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
