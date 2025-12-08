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
    public partial class BlazoriseButton_Demo : UserControl
    {
        public BlazoriseButton_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new TestButtonClickClass();
        }
    }

    public class TestButtonClickClass : INotifyPropertyChanged
    {
        public Action ButtonClickDel;

        public TestButtonClickClass()
        {
            ButtonClickDel = ButtonClick;
        }

        public void ButtonClick()
        {
            MessageBox.Show("You clicked me!");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
