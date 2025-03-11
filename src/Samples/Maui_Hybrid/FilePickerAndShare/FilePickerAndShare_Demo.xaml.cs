using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Storage;
using System.Windows.Media.Imaging;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("maui", "hybrid", "device", "native", "storage", "access")]
    public partial class FilePickerAndShare_Demo : UserControl
    {
        string _pickedFileFullPath;
        public FilePickerAndShare_Demo()
        {
            this.InitializeComponent();
        }

        private void PickImageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // Check the current location permission status.
                var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

                // If permission is not granted, request it.
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.StorageRead>();
                }

                // If permission is granted, fetch the location.
                if (status == PermissionStatus.Granted)
                {
                    try
                    {
                        var options = PickOptions.Images;
                        var result = await FilePicker.Default.PickAsync(options);
                        if (result != null)
                        {
                            _pickedFileFullPath = result.FullPath;
                            ShareButton.Visibility = Visibility.Visible;
                            PickedImageControl.Visibility = Visibility.Visible;
                            FeatureNotAllowedTextBlock.Visibility = Visibility.Collapsed;
                            if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                                result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                            {
                                using (var stream = await result.OpenReadAsync())
                                {
                                    BitmapImage bitmapImage = new BitmapImage();

                                    bitmapImage.SetSource(stream);
                                    PickedImageControl.Source = bitmapImage;
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
                        // The user canceled or something went wrong
                    }
                }
            });
        }

        private void ShareImageButton_Click(object sender, RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (_pickedFileFullPath != null)
                {
                    try
                    {
                        await Share.Default.RequestAsync(new ShareFileRequest
                        {
                            Title = "Share the picked image",
                            File = new ShareFile(_pickedFileFullPath)
                        });
                    }
                    catch (Exception ex)
                    {
                        // The user canceled or something went wrong
                    }
                }
                else
                {
                    MessageBox.Show("Pick an image first before attempting to share it.");
                }

            });
        }
    }
}
