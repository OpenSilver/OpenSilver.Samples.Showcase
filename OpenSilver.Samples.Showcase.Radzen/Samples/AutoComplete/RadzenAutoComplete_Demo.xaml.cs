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
    public partial class RadzenAutoComplete_Demo : UserControl
    {
        public RadzenAutoComplete_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new AutoCompleteData();
        }
    }

    public class AutoCompleteData : INotifyPropertyChanged
    {
        private string _currentWord;

        public string CurrentWord
        {
            get { return _currentWord; }
            set { _currentWord = value; OnPropertyChanged(); }
        }


        private HashSet<string> _words;
        public HashSet<string> Words
        {
            get
            {
                if (_words == null)
                {
                    _words = new HashSet<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                }
                return _words;
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
