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
    public partial class RadzenDatePicker_Demo : UserControl
    {
        public RadzenDatePicker_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new TestDatePickerClass();
        }


        public class TestDatePickerClass : INotifyPropertyChanged
        {
            public Action<DateTime?> DateChangeDel;

            private string _textForTextBox;

            public string TextForTextBox
            {
                get { return _textForTextBox; }
                set { _textForTextBox = value; OnPropertyChanged(); }
            }


            public TestDatePickerClass()
            {
                DateChangeDel = OnDateChange;
            }

            public void OnDateChange(DateTime? newDate)
            {
                if (newDate != null)
                {
                    DateTime dt = (DateTime)newDate;
                    TimeSpan ts = dt.Date - DateTime.Today;
                    var daysCount = (int)ts.TotalDays;
                    string difference = daysCount == 0 ? "Today" : daysCount > 0 ? $"in {daysCount} days" : $"{-daysCount} days ago";
                    bool isPast = ts.TotalDays < 0;
                    TextForTextBox = $"The date you picked is {difference}.";
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
