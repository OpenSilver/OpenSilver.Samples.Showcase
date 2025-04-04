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
using System.Collections.Generic;

namespace OpenSilver.Samples.Showcase
{
    public partial class RadzenCheckBoxList_Demo : UserControl
    {
        //todo: If we want a more in depth sample, we could add the populating from data part: https://blazor.radzen.com/checkboxlist?theme=material3#populate-items

        public RadzenCheckBoxList_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new TestCheckBoxListClass();
        }
    }

    public class TestCheckBoxListClass : INotifyPropertyChanged
    {
        Dictionary<int, string> valuesToNames = new Dictionary<int, string>()
            {
                {1, "Orders" },
                {2, "Employees" },
                {3, "Customers" }
            };

        public Action<IEnumerable<int>> CheckBoxListChangeDel;


        private string _checkedItems = "Orders";

        public string CheckedItems
        {
            get { return _checkedItems; }
            set { _checkedItems = value; OnPropertyChanged(); }
        }

        public TestCheckBoxListClass()
        {
            CheckBoxListChangeDel = CheckBoxListCheckChange;
        }

        public void CheckBoxListCheckChange(IEnumerable<int> newValue)
        {
            CheckedItems = "";
            foreach (int i in newValue)
            {
                CheckedItems += $"{valuesToNames[i]}, ";
            }
            if(!string.IsNullOrEmpty(CheckedItems))
            {
                CheckedItems = CheckedItems.Substring(0,CheckedItems.Length - 2);
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
