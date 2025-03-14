using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Networking;
using Microsoft.Maui.Devices;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("maui", "hybrid", "device", "native", "connexion", "status")]
    public partial class Connectivity_Demo : UserControl
    {
        List<ConnectionProfile> _allConnectionProfiles = new List<ConnectionProfile>() {
                                                                    ConnectionProfile.Cellular,
                                                                    ConnectionProfile.Bluetooth,
                                                                    ConnectionProfile.Ethernet,
                                                                    ConnectionProfile.WiFi,
                                                                    ConnectionProfile.Unknown };

        public Connectivity_Demo()
        {
            this.InitializeComponent();

            if (DeviceInfo.Current.Platform == DevicePlatform.Unknown)
            {
                SampleContainer.Children.Clear();
                SampleContainer.Children.Add(new TextBlock() { Text = "The connectivity sample is not supported in the browser.", TextWrapping = TextWrapping.Wrap });
            }
        }
        void CheckConnectivityButton_Click(object sender, RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {

                IEnumerable<ConnectionProfile> profiles = Connectivity.Current.ConnectionProfiles;

                string connectivitiesText = "Connections active:";
                foreach (ConnectionProfile profile in _allConnectionProfiles)
                {
                    connectivitiesText += $"\r\n - {profile}: " + (profiles.Contains(profile) ? "Yes" : "No");
                }

                ConnectivityTextBlock.Text = connectivitiesText;
            });
        }
    }
}
