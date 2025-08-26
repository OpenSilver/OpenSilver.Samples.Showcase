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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OpenSilver.Samples.Showcase
{
    public partial class RadzenToggleButton_Demo : UserControl
    {
        public RadzenToggleButton_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new TestToggleButtonClass();
        }
    }
    public class TestToggleButtonClass : INotifyPropertyChanged
    {

        private string _toggledText = "Untoggled";
        public string ToggledText
        {
            get { return _toggledText; }
            set { _toggledText = value; OnPropertyChanged(); }
        }

        private bool _toggled;
        public bool Toggled
        {
            get { return _toggled; }
            set { _toggled = value; ToggledText = value ? "Toggled" : "Untoggled"; OnPropertyChanged(); }
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
