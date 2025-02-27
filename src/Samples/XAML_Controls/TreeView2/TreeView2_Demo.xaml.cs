using OpenSilver.Samples.Showcase.Search;
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
    [SearchKeywords("tree", "hierarchy", "nodes", "expand", "UI")]
    public partial class TreeView2_Demo : UserControl
    {
        public TreeView2_Demo()
        {
            this.InitializeComponent();
            this.DataContext = League.Leagues;
        }
    }
}
