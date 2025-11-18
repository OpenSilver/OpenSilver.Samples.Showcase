using Microsoft.Maui.Devices;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    public partial class Welcome : Page
    {
        public Welcome()
        {
            InitializeComponent();

            //Apple review team does not like the default description, so we decided to introduce a separate one
            if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
            {
                DescriptionDefault.Visibility = Visibility.Collapsed;
                DescriptionIos.Visibility = Visibility.Visible;
            }
        }
    }
}
