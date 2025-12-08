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

namespace OpenSilver.Showcase
{
    public partial class RadzenCheckBox_Demo : UserControl
    {
        public RadzenCheckBox_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new TestCheckBoxClass();
        }
    }
    public class TestCheckBoxClass : INotifyPropertyChanged
    {
        private bool? _isChecked = true;

        public bool? IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; OnPropertyChanged(); }
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
