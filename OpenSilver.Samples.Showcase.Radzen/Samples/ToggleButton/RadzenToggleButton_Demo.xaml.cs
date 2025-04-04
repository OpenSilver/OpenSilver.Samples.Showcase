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
        public Action<bool> ButtonClickDel;

        private string _toggleButtonText = "Check";
        public string ToggleButtonText
        {
            get { return _toggleButtonText; }
            set { _toggleButtonText = value; OnPropertyChanged(); }
        }


        public TestToggleButtonClass()
        {
            ButtonClickDel = ButtonClick;
        }

        public void ButtonClick(bool newValue)
        {
            ToggleButtonText = newValue ? "Uncheck" : "Check";
            string message = newValue ? $"You Checked me!" : $"You Unchecked me!";
            //MessageBox.Show(message);
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
