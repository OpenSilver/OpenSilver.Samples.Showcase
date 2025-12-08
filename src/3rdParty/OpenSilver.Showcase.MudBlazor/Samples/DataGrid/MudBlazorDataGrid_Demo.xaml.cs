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

namespace OpenSilver.Showcase
{
    public partial class MudBlazorDataGrid_Demo : UserControl
    {
        public MudBlazorDataGrid_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new MudBlazorModel.EmployeesData();
        }
    }
}
