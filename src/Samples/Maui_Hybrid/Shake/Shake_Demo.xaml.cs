using System;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;

namespace OpenSilver.Samples.Showcase
{
    public partial class Shake_Demo : UserControl
    {
        public Shake_Demo()
        {
            this.InitializeComponent();
        }

    
        int shakes = 0;
        private void ShakeToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // Check the current location permission status.
                var status = await Permissions.CheckStatusAsync<Permissions.Sensors>();

                // If permission is not granted, request it.
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Sensors>();
                }

                // If permission is granted, fetch the location.
                if (status == PermissionStatus.Granted)
                {
                    if (Accelerometer.Default.IsSupported)
                    {
                        if (!Accelerometer.Default.IsMonitoring)
                        {
                            // Turn on ShakeSensor
                            Accelerometer.Default.ShakeDetected += ShakeSensor_ReadingChanged;
                            Accelerometer.Default.Start(SensorSpeed.Game);
                            ShakeTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                        }
                        else
                        {
                            // Turn off ShakeSensor
                            Accelerometer.Default.Stop();
                            Accelerometer.Default.ShakeDetected -= ShakeSensor_ReadingChanged;
                            ShakeTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                        }
                    }
                    else
                    {
                        ShakeTextBlock.Text = $"The ShakeSensor is not supported on this device.";
                    }
                }
            });
        }

        private void ShakeSensor_ReadingChanged(object sender, EventArgs e)
        {
            ShakeTextBlock.Text = $"{++shakes} shakes detected.";
        }
    }
}
