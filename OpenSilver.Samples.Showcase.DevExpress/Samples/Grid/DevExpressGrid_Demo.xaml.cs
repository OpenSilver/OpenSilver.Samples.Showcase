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
    public partial class DevExpressGrid_Demo : UserControl
    {
        public DevExpressGrid_Demo()
        {
            this.InitializeComponent();

            this.DataContext = new DevExpressModel.EmployeesData();
        }
    }
}
