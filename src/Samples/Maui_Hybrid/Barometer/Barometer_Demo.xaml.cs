using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;

namespace OpenSilver.Samples.Showcase
{
    public partial class Barometer_Demo : UserControl
    {
        public Barometer_Demo()
        {
            this.InitializeComponent();
        }


        private void BarometerToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
                    if (Barometer.Default.IsSupported)
                    {
                        if (!Barometer.Default.IsMonitoring)
                        {
                            // Turn on Barometer
                            Barometer.Default.ReadingChanged += Barometer_ReadingChanged;
                            Barometer.Default.Start(SensorSpeed.UI);
                            BarometerTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                        }
                        else
                        {
                            // Turn off Barometer
                            Barometer.Default.Stop();
                            Barometer.Default.ReadingChanged -= Barometer_ReadingChanged;
                            BarometerTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                        }
                    }
                    else
                    {
                        BarometerTextBlock.Text = $"The barometer is not supported on this device.";
                    }
                }
            });
        }

        private void Barometer_ReadingChanged(object sender, BarometerChangedEventArgs e)
        {
            BarometerTextBlock.Text = $"Barometer: {e.Reading}";
        }

    }
}
