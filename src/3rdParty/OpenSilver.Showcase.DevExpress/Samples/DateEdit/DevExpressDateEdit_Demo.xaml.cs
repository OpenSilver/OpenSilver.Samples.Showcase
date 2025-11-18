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


namespace OpenSilver.Showcase
{
    public partial class DevExpressDateEdit_Demo : UserControl
    {
        public DevExpressDateEdit_Demo()
        {
            this.InitializeComponent();

            this.DataContext = new TestDatePickerClass();
        }


        public class TestDatePickerClass : INotifyPropertyChanged
        {
            private string _textForTextBox;
            public string TextForTextBox
            {
                get { return _textForTextBox; }
                set { _textForTextBox = value; OnPropertyChanged(); }
            }

            private DateTime _date = DateTime.Now;
            public DateTime Date
            {
                get { return _date; }
                set { _date = value; OnDateChange(value); OnPropertyChanged(); }
            }

            public void OnDateChange(DateTime? newDate)
            {
                string newDateAsString = newDate != null ? newDate.ToString() : "null";
                Console.WriteLine(@$"new date: {newDateAsString}");
                if (newDate != null)
                {
                    DateTime dt = (DateTime)newDate;
                    TimeSpan ts = dt.Date - DateTime.Today;
                    var daysCount = (int)ts.TotalDays;
                    string difference = daysCount == 0 ? "Today" : daysCount > 0 ? $"is in {daysCount} days" : $"was {-daysCount} days ago";
                    TextForTextBox = $"The date you picked {difference}.";
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
