using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class Location : UserControl
    {
        public Location()
        {
            this.InitializeComponent();
        }

        void GetButton_Click(object sender, RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // Check the current location permission status.
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                // If permission is not granted, request it.
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                // If permission is granted, fetch the location.
                if (status == PermissionStatus.Granted)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.Default.GetLocationAsync(request);

                    if (location != null)
                    {
                        // Switch back to the OpenSilver thread to update the UI.
                        Dispatcher.BeginInvoke(() =>
                        {
                            LocationTextBlock.Text = location.ToString();
                        });
                    }
                }
                else
                {
                    // Inform the user if permission was denied.
                    Dispatcher.BeginInvoke(() =>
                    {
                        MessageBox.Show("Location permission denied.");
                    });
                }
            });
        }
    }
}
