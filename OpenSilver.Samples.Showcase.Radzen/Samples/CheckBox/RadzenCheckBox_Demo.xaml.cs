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

namespace OpenSilver.Samples.Showcase
{
    public partial class RadzenCheckBox_Demo : UserControl
    {
        public RadzenCheckBox_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new TestCheckBoxClass();
        }
    }
    public class TestCheckBoxClass
    {
        public Action<bool> CheckBoxClickDel;

        public TestCheckBoxClass()
        {
            CheckBoxClickDel = CheckBoxCheck;
        }

        public void CheckBoxCheck(bool newValue)
        {
            MessageBox.Show(newValue? "You checked me!" : "You unchecked me!");
        }
    }
}
