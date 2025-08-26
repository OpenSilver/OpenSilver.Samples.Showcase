using MudBlazor.Utilities;
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

namespace OpenSilver.Samples.Showcase
{
    public partial class MudBlazorColorPicker_Demo : UserControl
    {
        ColorPickerData _colorPickerData = new ColorPickerData();
        public MudBlazorColorPicker_Demo()
        {
            this.InitializeComponent();
            this.DataContext = _colorPickerData;
        }
    }


    public class ColorPickerData : INotifyPropertyChanged
    {
        private SolidColorBrush _colorBrush = new SolidColorBrush(Color.FromRgb(68, 58, 110));
        public SolidColorBrush ColorBrush
        {
            get { return _colorBrush; }
            set { _colorBrush = value; OnPropertyChanged(); }
        }


        private MudColor _mudColor = new MudColor();
        public MudColor TheMudColor
        {
            get { return _mudColor; }
            set
            {
                _mudColor = value;
                OnPropertyChanged();
                SetBrush(value);
            }
        }

        public void SetBrush(MudColor newColor)
        {
            ColorBrush = new SolidColorBrush(Color.FromArgb(newColor.A, newColor.R, newColor.G, newColor.B));
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
