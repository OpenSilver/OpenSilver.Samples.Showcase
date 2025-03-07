using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("maui", "hybrid", "device", "native")]
    public partial class Flashlight_Demo : UserControl
    {
        public Flashlight_Demo()
        {
            this.InitializeComponent();
        }

        void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // Check the current location permission status.
                var status = await Permissions.CheckStatusAsync<Permissions.Flashlight>();

                // If permission is not granted, request it.
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Flashlight>();
                }

                // If permission is granted, fetch the location.
                if (status == PermissionStatus.Granted)
                {
                    try
                    {
                        if (FlashlightButton.IsChecked == true)
                            await Flashlight.Default.TurnOnAsync();
                        else
                            await Flashlight.Default.TurnOffAsync();
                    }
                    catch (FeatureNotSupportedException ex)
                    {
                        UnsupportedTextBlock.Text = "The flashlight is not supported on this device.";
                        UnsupportedTextBlock.Visibility = Visibility.Visible;
                    }
                    catch (PermissionException ex)
                    {
                        UnsupportedTextBlock.Text = "This sample requires your permission to use the flashlight.";
                        UnsupportedTextBlock.Visibility = Visibility.Visible;
                    }
                    catch (Exception ex)
                    {
                        // Unable to turn on/off flashlight
                    }
                }
            });
        }

    }
}
