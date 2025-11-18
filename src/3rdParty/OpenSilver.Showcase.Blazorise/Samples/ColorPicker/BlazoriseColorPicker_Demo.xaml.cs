using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Globalization;
using System.Windows.Media;

namespace OpenSilver.Showcase
{
    public partial class BlazoriseColorPicker_Demo : UserControl
    {
        ColorPickerData _colorPickerData = new ColorPickerData();
        public BlazoriseColorPicker_Demo()
        {
            this.InitializeComponent();
            this.DataContext = _colorPickerData;
        }
    }


    public class ColorPickerData : INotifyPropertyChanged
    {
        public Action<string> SetBrushDel;

        private SolidColorBrush _colorBrush = new SolidColorBrush(Color.FromRgb(68, 58, 110));
        public SolidColorBrush ColorBrush
        {
            get { return _colorBrush; }
            set { _colorBrush = value; OnPropertyChanged(); }
        }

        public ColorPickerData()
        {
            SetBrushDel = SetBrush;
        }

        public void SetBrush(string colorString)
        {
            Console.WriteLine(colorString);
            ColorBrush = FromHex(colorString);
        }

        public static SolidColorBrush FromHex(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                throw new ArgumentException("Invalid color code", nameof(hex));

            hex = hex.TrimStart('#');

            byte a = 255, r, g, b;

            if (hex.Length == 6)
            {
                r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            }
            else if (hex.Length == 8)
            {
                r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
                b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
                a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
            }
            else
            {
                throw new ArgumentException("Hex color must be in #RRGGBB or #RRGGBBAA format.", nameof(hex));
            }

            return new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
