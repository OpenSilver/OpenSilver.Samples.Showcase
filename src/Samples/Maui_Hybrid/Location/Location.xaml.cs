using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using System.Windows;
using System.Windows.Controls;
using OpenSilver.Samples.Showcase.Search;
using Microsoft.Maui.Devices;
using System.Security.Cryptography;
using System;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("maui", "hybrid", "device", "native", "geolocation", "coordinates", "longitude", "latitude", "information")]
    public partial class Location : UserControl
    {
        public Location()
        {
            this.InitializeComponent();
        }

        void GetButton_Click(object sender, RoutedEventArgs e)
        {
            if(DeviceInfo.Current.Platform == DevicePlatform.Unknown)
            {
                GetLocation_Browser();
            }
            else
            {
                GetLocation_Maui();
            }
        }

        private void GetLocation_Browser()
        {
            Interop.ExecuteJavaScriptAsync("navigator.geolocation.getCurrentPosition((pos) => {$0(\"lat: \"+ pos.coords.latitude + \"\\r\\nlong: \" + pos.coords.longitude)})", (Action<string>)ReceiveLocation).ToString();
            //LocationTextBlock.Text = result;
        }

        private void ReceiveLocation(string obj)
        {
            LocationTextBlock.Text = obj;
        }

        private void GetLocation_Maui()
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
                    try
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
                    catch (FeatureNotSupportedException fnsEx)
                    {
                        LocationTextBlock.Text = "This device does not support Geolocation.";
                    }
                    catch (FeatureNotEnabledException fneEx)
                    {
                        LocationTextBlock.Text = "The Geolocation feature is not enabled.";
                    }
                    catch (PermissionException pEx)
                    {
                        LocationTextBlock.Text = "This sample requires permission to use Geolocation.";
                    }
                    catch (Exception ex)
                    {
                        LocationTextBlock.Text = "Could not get location.";
                    }
                }
                else
                {
                    LocationTextBlock.Text = "This sample requires permission to use Geolocation.";
                }
            });
        }
    }
}
