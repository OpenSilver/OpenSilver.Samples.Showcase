using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using OpenSilver.Samples.Showcase.Search;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("maui", "hybrid", "device", "native", "charge", "charging", "status", "information")]
    public partial class Battery_Demo : UserControl
    {
        public Battery_Demo()
        {
            this.InitializeComponent();

            if (DeviceInfo.Current.Platform == DevicePlatform.Unknown)
            {
                //if we are in a browser which doesn't support the battery status API, show a message instead of the Sample contents.

                bool isBatteryUnsupported = Interop.ExecuteJavaScriptGetResult<bool>("!navigator.getBattery");
                if (isBatteryUnsupported)
                {
                    SampleContainer.Children.Clear();

                    Hyperlink link = new Hyperlink() { NavigateUri = new Uri("https://developer.mozilla.org/en-US/docs/Web/API/Battery_Status_API#browser_compatibility"), TargetName = "_blank" };
                    link.Inlines.Add("here");
                    TextBlock tb = new TextBlock();
                    tb.TextWrapping = TextWrapping.Wrap;
                    tb.Inlines.Add("The battery status API is not supported in this browser. Check ");
                    tb.Inlines.Add(link);
                    tb.Inlines.Add(" to see its compatibility table.");
                    SampleContainer.Children.Add(tb);
                }
            }
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
            Interop.ExecuteJavaScriptAsync("navigator.getBattery().then((battery) => { $0(\"Battery is \" + (battery.charging ? \"\" : \"not \") + \"charging\\r\\nCharge level: \" + battery.level * 100 + \"%\", true) })", (Action<string>)UpdateBatteryStatus_Browser);
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
