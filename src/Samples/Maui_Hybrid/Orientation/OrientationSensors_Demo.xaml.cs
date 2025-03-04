using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;

namespace OpenSilver.Samples.Showcase
{
    public partial class OrientationSensors_Demo : UserControl
    {
        public OrientationSensors_Demo()
        {
            this.InitializeComponent();
        }

        #region Compass
        private void CompassToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
                    if (Compass.Default.IsSupported)
                    {
                        if (!Compass.Default.IsMonitoring)
                        {
                            // Turn on compass
                            Compass.Default.ReadingChanged += Compass_ReadingChanged;
                            Compass.Default.Start(SensorSpeed.Default);
                            CompassPath.Stroke = new SolidColorBrush(Colors.Green);
                        }
                        else
                        {
                            // Turn off compass
                            Compass.Default.Stop();
                            Compass.Default.ReadingChanged -= Compass_ReadingChanged;
                            CompassPath.Stroke = new SolidColorBrush(Colors.Black);
                        }
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
                }
            });
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var acceleration = e.Reading.Acceleration;
            AccelX.Text = $"X: {acceleration.X}G";
            AccelY.Text = $"Y: {acceleration.Y}G";
            AccelZ.Text = $"Z: {acceleration.Z}G";
        }
        #endregion

        #region Gyroscope
        private void GyroscopeToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
                }
            });
        }

        private void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            var velocity = e.Reading.AngularVelocity;
            GyroX.Text = $"X: {velocity.X}rad/s";
            GyroY.Text = $"Y: {velocity.Y}rad/s";
            GyroZ.Text = $"Z: {velocity.Z}rad/s";
        }
        #endregion

        #region Magnetometer
        private void MagnetometerToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
                }
            });
        }

        private void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e)
        {
            var velocity = e.Reading.MagneticField;
            MagnX.Text = $"X: {velocity.X}µT";
            MagnY.Text = $"Y: {velocity.Y}µT";
            MagnZ.Text = $"Z: {velocity.Z}µT";
        }
        #endregion

        #region OrientationSensor
        private void OrientationSensorToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
                }
            });
        }

        private void OrientationSensor_ReadingChanged(object sender, OrientationSensorChangedEventArgs e)
        {
            var velocity = e.Reading.Orientation;
            OriW.Text = $"W: {velocity.W}";
            OriX.Text = $"X: {velocity.X}";
            OriY.Text = $"Y: {velocity.Y}";
            OriZ.Text = $"Z: {velocity.Z}";
        }
        #endregion
    }
}
