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
    public partial class RadzenMask_Demo : UserControl
    {
        public RadzenMask_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new PhoneData();
        }

        public class PhoneData : INotifyPropertyChanged
        {
            public Action<string> PhoneChangeDel;
            private string _phone;

            public string Phone
            {
                get { return _phone; }
                set { _phone = value; OnPropertyChanged(); }
            }

            public PhoneData()
            {
                PhoneChangeDel = PhoneChange;
            }

            public void PhoneChange(string phone)
            {
                Phone = phone;
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
