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

        private void CheckBatteryStatusButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DeviceInfo.Current.Platform == DevicePlatform.Unknown)
            {
                CheckBattery_Browser();
            }
            else
            {
                CheckBattery_Maui();
            }
        }

        private void CheckBattery_Browser()
        {
            Interop.ExecuteJavaScriptAsync("if (navigator.getBattery) { navigator.getBattery().then((battery) => { $0(\"Battery is \" + (battery.charging ? \"\" : \"not \") + \"charging\\r\\nCharge level: \" + battery.level * 100 + \"%\") })} else { $0(\"Battery status is not available in this browser.\") }", (Action<string>)UpdateBatteryStatus_Browser) ;
        }

        private void UpdateBatteryStatus_Browser(string text)
        {
            BatteryStatus.Text = text;
        }

        private void CheckBattery_Maui()
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

                try
                {
                    // If permission is granted, fetch the location.
                    if (status == PermissionStatus.Granted)
                    {
                        string str = $"Battery is {Battery.Default.ChargeLevel * 100}% charged." + Environment.NewLine;
                        switch (Battery.Default.State)
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
                catch (FeatureNotSupportedException ex)
                {
                    BatteryStatus.Text = "The Battery status feature is not supported on this device.";
                }
                catch (PermissionException ex)
                {
                    BatteryStatus.Text = "This sample requires your permission to check the battery status.";
                }
                catch
                {
                    BatteryStatus.Text = "Could not check battery status.";
                }
            });
        }
    }
}
