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
    public partial class RadzenRadioButtonList_Demo : UserControl
    {
        public RadzenRadioButtonList_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new TestRadioButtonListClass();
        }


        public class TestRadioButtonListClass : INotifyPropertyChanged
        {
            Dictionary<int, string> _valuesToNames = new Dictionary<int, string>() { { 1, "Orders" }, { 2, "Employees" }, { 3, "Customers" } };
            public Action<int> RadioButtonListChangeDel;

            private string _value = "";
            public string Value
            {
                get { return _value; }
                set { _value = value; OnPropertyChanged(); }
            }


            public TestRadioButtonListClass()
            {
                RadioButtonListChangeDel = RadioButtonListChange;
            }

            public void RadioButtonListChange(int newValue)
            {
                if (_valuesToNames.ContainsKey(newValue))
                {
                    Value = _valuesToNames[newValue];
                }
                else
                {
                    Value = "";
                }
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
}
