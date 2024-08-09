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
    public partial class RadzenColorPicker_Demo : UserControl
    {
        TestClass _testClass = new TestClass();
        public RadzenColorPicker_Demo()
        {
            this.InitializeComponent();
            this.DataContext = _testClass;
        }

        public void OnColorChange2(string value)
        {
            System.Windows.MessageBox.Show(value);
        }
    }

    public class TestClass : INotifyPropertyChanged
    {
        public Action<string> SetBrushDel;

        private SolidColorBrush _colorBrush = new SolidColorBrush(Color.FromRgb(68, 58, 110));
        public SolidColorBrush ColorBrush
        {
            get { return _colorBrush; }
            set { _colorBrush = value; OnPropertyChanged(); }
        }

        public TestClass()
        {
            SetBrushDel = SetBrush;
        }

        public void SetBrush(string colorString)
        {
            byte a = 255, r = 255, g = 255, b = 255;
            int indexOfParenthesis = colorString.IndexOf('(');
            int indexOfClosingParenthesis = colorString.IndexOf(')');
            string shortenedString = colorString.Substring(indexOfParenthesis + 1, indexOfClosingParenthesis - indexOfParenthesis - 1);
            string[] split = shortenedString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length >= 3)
            {
                //we get r,g,b:
                r = byte.Parse(split[0]);
                g = byte.Parse(split[1]);
                b = byte.Parse(split[2]);

                if (split.Length == 4)
                {
                    double d = double.Parse(split[3]);

                    a = (byte)(d * 255);
                }
                ColorBrush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            }

        }
        //ParseStringToColor

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
