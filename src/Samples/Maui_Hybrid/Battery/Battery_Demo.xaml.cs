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
    [SearchKeywords("maui", "hybrid", "device", "native", "charge", "charging", "status", "information")]
    public partial class Battery_Demo : UserControl
    {
        public Battery_Demo()
        {
            this.InitializeComponent();
        }

        private void WatchBatteryToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // Check the current location permission status.
                var status = await Permissions.CheckStatusAsync<Permissions.Battery>();

                // If permission is not granted, request it.
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Battery>();
                }

                // If permission is granted, fetch the location.
                if (status == PermissionStatus.Granted)
                {
                    if (WatchBatteryToggleButton.IsChecked == true)
                    {
                        Battery.Default.BatteryInfoChanged += Battery_BatteryInfoChanged;
                    }
                    else
                    {
                        Battery.Default.BatteryInfoChanged -= Battery_BatteryInfoChanged;
                    }

                }
            });
        }

        private void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            string str = $"Battery is {e.ChargeLevel * 100}% charged." + Environment.NewLine;
            switch (e.State)
            {
                case BatteryState.Charging:
                    str += "Battery is currently charging";
                    break;
                case BatteryState.Discharging:
                    str += "Charger is not connected and the battery is discharging";
                    break;
                case BatteryState.Full:
                    str += "Battery is full";
                    break;
                case BatteryState.NotCharging:
                    str += "The battery isn't charging";
                    break;
                case BatteryState.NotPresent:
                    str += "Battery is not available";
                    break;
                default:
                    str += "Battery is unknown";
                    break;
            }

            BatteryStatus.Text = str;

        }
    }
}
