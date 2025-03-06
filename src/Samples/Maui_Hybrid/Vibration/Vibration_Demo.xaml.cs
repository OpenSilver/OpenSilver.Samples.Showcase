using System;
using Microsoft.Maui.ApplicationModel;
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
using Microsoft.Maui.Devices;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("maui", "hybrid", "device", "native", "haptic", "feedback")]
    public partial class Vibration_Demo : UserControl
    {
        public Vibration_Demo()
        {
            this.InitializeComponent();
        }

        void VibrateButton_Click(object sender, RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // Check the current location permission status.
                var status = await Permissions.CheckStatusAsync<Permissions.Vibrate>();

                // If permission is not granted, request it.
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Vibrate>();
                }

                // If permission is granted, fetch the location.
                if (status == PermissionStatus.Granted)
                {
                    try
                    {
                        Vibration.Default.Vibrate(new TimeSpan(0,0,1));
                    }
                    catch (FeatureNotSupportedException ex)
                    {
                        // Handle not supported on device exception
                    }
                    catch (PermissionException ex)
                    {
                        // Handle permission exception
                    }
                    catch (Exception ex)
                    {
                        // Unable to turn on/off flashlight
                    }
                }
            });
        }

        void HapticFeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // Check the current location permission status.
                var status = await Permissions.CheckStatusAsync<Permissions.Vibrate>();

                // If permission is not granted, request it.
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Vibrate>();
                }

                // If permission is granted, fetch the location.
                if (status == PermissionStatus.Granted)
                {
                    try
                    {
                        HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                    }
                    catch (FeatureNotSupportedException ex)
                    {
                        // Handle not supported on device exception
                        MessageBox.Show("Haptic feedback is not supported on this device.");
                    }
                    catch (PermissionException ex)
                    {
                        // Handle permission exception
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
