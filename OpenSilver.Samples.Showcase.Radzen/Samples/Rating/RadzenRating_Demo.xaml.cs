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
    public partial class RadzenRating_Demo : UserControl
    {
        public RadzenRating_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new TestRatingClass();
        }


        public class TestRatingClass : INotifyPropertyChanged
        {
            public Action<int> RatingChangeDel;

            private int _value;

            public int Value
            {
                get { return _value; }
                set { _value = value; OnPropertyChanged(); }
            }


            public TestRatingClass()
            {
                RatingChangeDel = OnRatingChange;
            }

            public void OnRatingChange(int newValue)
            {
                Value = newValue;
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
