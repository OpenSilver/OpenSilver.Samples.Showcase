using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using CSHTML5.Internal;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Devices.Sensors;
using OpenSilver.Samples.Showcase.Search;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("maui", "hybrid", "device", "native", "sensor", "information", "compass", "accelerometer", "gyroscope", "magnetometer", "orientation")]
    public partial class OrientationSensors_Demo : UserControl
    {
        INTERNAL_DispatcherQueueHandler queueHandler = new INTERNAL_DispatcherQueueHandler();
        INTERNAL_DispatcherQueueHandler queueHandler2 = new INTERNAL_DispatcherQueueHandler();
        INTERNAL_DispatcherQueueHandler queueHandler3 = new INTERNAL_DispatcherQueueHandler();
        INTERNAL_DispatcherQueueHandler queueHandler4 = new INTERNAL_DispatcherQueueHandler();

        public OrientationSensors_Demo()
        {
            this.InitializeComponent();
            if (DeviceInfo.Current.Platform == DevicePlatform.Unknown)
            {
                SampleContainer.Children.Clear();
                SampleContainer.Children.Add(new TextBlock() { Text = "These samples are not supported in the browser.", TextWrapping = TextWrapping.Wrap });
            }
            else
            {
                List<string> unsupportedFeatures = new List<string>();
                if (!Compass.Default.IsSupported)
                {
                    CompassContainer.Visibility = Visibility.Collapsed;
                    unsupportedFeatures.Add("Compass");
                }
                if (!Accelerometer.Default.IsSupported)
                {
                    AccelerometerContainer.Visibility = Visibility.Collapsed;
                    unsupportedFeatures.Add("Accelerometer");
                }
                if (!Gyroscope.Default.IsSupported)
                {
                    GyroscopeContainer.Visibility = Visibility.Collapsed;
                    unsupportedFeatures.Add("Gyroscope");
                }
                if (!Magnetometer.Default.IsSupported)
                {
                    MagnetometerContainer.Visibility = Visibility.Collapsed;
                    unsupportedFeatures.Add("Magnetometer");
                }
                if (!OrientationSensor.Default.IsSupported)
                {
                    OrientationContainer.Visibility = Visibility.Collapsed;
                    unsupportedFeatures.Add("Orientation sensor");
                }

                if (unsupportedFeatures.Any())
                {
                    UnsupportedTextBlock.Text = $"The following sections of this sample have been hidden because this device does not support them: \r\n - {string.Join(",\r\n - ", unsupportedFeatures)}.";
                    UnsupportedTextBlock.Visibility = Visibility.Visible;
                }
            }
        }

        #region Compass
        private void CompassToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (Compass.Default.IsSupported)
                {
                    if (!Compass.Default.IsMonitoring)
                    {
                        // Turn on compass
                        Compass.Default.ReadingChanged += Compass_ReadingChanged;
                        Compass.Default.Start(SensorSpeed.UI);
                        //CompassPath.Stroke = new SolidColorBrush(Colors.Green);
                        TextBlock.SetForeground(CompassPath, new SolidColorBrush(Colors.Green));
                    }
                    else
                    {
                        // Turn off compass
                        Compass.Default.Stop();
                        Compass.Default.ReadingChanged -= Compass_ReadingChanged;
                        //CompassPath.SetValue(Path.StrokeProperty, DependencyProperty.UnsetValue);

                        CompassPath.SetValue(TextBlock.ForegroundProperty, DependencyProperty.UnsetValue);
                    }
                }
            });
        }

        private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            //turn the arrow towards North:
            CompassTransform.Angle = -e.Reading.HeadingMagneticNorth;
        }
        #endregion

        #region Accelerometer
        private void AccelerometerToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (Accelerometer.Default.IsSupported)
                {
                    if (!Accelerometer.Default.IsMonitoring)
                    {
                        // Turn on Accelerometer
                        Accelerometer.Default.ReadingChanged += Accelerometer_ReadingChanged;
                        Accelerometer.Default.Start(SensorSpeed.UI);
                    }
                    else
                    {
                        // Turn off Accelerometer
                        Accelerometer.Default.Stop();
                        Accelerometer.Default.ReadingChanged -= Accelerometer_ReadingChanged;
                    }
                }
            });
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            queueHandler.QueueActionIfQueueIsEmpty(() =>
            {
                var acceleration = e.Reading.Acceleration;
                Accel.Text = $@"X: {acceleration.X}G
Y: {acceleration.Y}G
Z: {acceleration.Z}G";
            });
        }
        #endregion

        #region Gyroscope
        private void GyroscopeToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (Gyroscope.Default.IsSupported)
                {
                    if (!Gyroscope.Default.IsMonitoring)
                    {
                        // Turn on Gyroscope
                        Gyroscope.Default.ReadingChanged += Gyroscope_ReadingChanged;
                        Gyroscope.Default.Start(SensorSpeed.UI);
                    }
                    else
                    {
                        // Turn off Gyroscope
                        Gyroscope.Default.Stop();
                        Gyroscope.Default.ReadingChanged -= Gyroscope_ReadingChanged;
                    }
                }
            });
        }

        private void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            queueHandler2.QueueActionIfQueueIsEmpty(() =>
            {
                var velocity = e.Reading.AngularVelocity;
                Gyro.Text = @$"X: {velocity.X}rad/s
Y: {velocity.Y}rad/s
Z: {velocity.Z}rad/s";
            });
        }
        #endregion

        #region Magnetometer
        private void MagnetometerToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (Magnetometer.Default.IsSupported)
                {
                    if (!Magnetometer.Default.IsMonitoring)
                    {
                        // Turn on Magnetometer
                        Magnetometer.Default.ReadingChanged += Magnetometer_ReadingChanged;
                        Magnetometer.Default.Start(SensorSpeed.Default);
                    }
                    else
                    {
                        // Turn off Magnetometer
                        Magnetometer.Default.Stop();
                        Magnetometer.Default.ReadingChanged -= Magnetometer_ReadingChanged;
                    }
                }
            });
        }

        private void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e)
        {
            queueHandler3.QueueActionIfQueueIsEmpty(() =>
            {
                var velocity = e.Reading.MagneticField;
                Magn.Text = @$"X: {velocity.X}µT
Y: {velocity.Y}µT
Z: {velocity.Z}µT";
            });
        }
        #endregion

        #region OrientationSensor
        private void OrientationSensorToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (OrientationSensor.Default.IsSupported)
                {
                    if (!OrientationSensor.Default.IsMonitoring)
                    {
                        // Turn on OrientationSensor
                        OrientationSensor.Default.ReadingChanged += OrientationSensor_ReadingChanged;
                        OrientationSensor.Default.Start(SensorSpeed.Default);
                    }
                    else
                    {
                        // Turn off OrientationSensor
                        OrientationSensor.Default.Stop();
                        OrientationSensor.Default.ReadingChanged -= OrientationSensor_ReadingChanged;
                    }
                }
            });
        }

        private void OrientationSensor_ReadingChanged(object sender, OrientationSensorChangedEventArgs e)
        {
            queueHandler4.QueueActionIfQueueIsEmpty(() =>
            {
                var quaternion = e.Reading.Orientation;
                Ori.Text = @$"W: {quaternion.W}
X: {quaternion.X}
Y: {quaternion.Y}
Z: {quaternion.Z}
";
            });
        }
        #endregion
    }
}
