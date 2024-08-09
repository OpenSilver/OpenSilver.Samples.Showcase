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
    public partial class RadzenDropDown_Demo : UserControl
    {
        public RadzenDropDown_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new DropDownData();
        }


        public class DropDownData : INotifyPropertyChanged
        {
            public Action<object> DropDownChangeDel;
            private string _favoriteDay;

            public string FavoriteDay
            {
                get { return _favoriteDay; }
                set { _favoriteDay = value; OnPropertyChanged(); }
            }


            private HashSet<string> _days;
            public HashSet<string> Days
            {
                get
                {
                    if (_days == null)
                    {
                        _days = new HashSet<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                    }
                    return _days;
                }
            }


            public DropDownData()
            {
                DropDownChangeDel = DropDownChange;
            }

            public void DropDownChange(object newValue)
            {
                FavoriteDay = newValue.ToString();
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
