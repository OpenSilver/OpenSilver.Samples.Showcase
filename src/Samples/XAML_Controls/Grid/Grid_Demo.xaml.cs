using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

namespace OpenSilver.Samples.Showcase
{
    public partial class Grid_Demo : UserControl
    {
        public Grid_Demo()
        {
            this.InitializeComponent();
        }

        #region Manipulate Grid Rows/Columns
        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            MyGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
        }
        private void RemoveRowButton_Click(object sender, RoutedEventArgs e)
        {
            int count = MyGrid.RowDefinitions.Count;
            if (count > 0)
            {
                MyGrid.RowDefinitions.RemoveAt(MyGrid.RowDefinitions.Count - 1);
            }
        }

        private void AddColumnButton_Click(object sender, RoutedEventArgs e)
        {
            MyGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
        }
        private void RemoveColumnButton_Click(object sender, RoutedEventArgs e)
        {
            int count = MyGrid.ColumnDefinitions.Count;
            if (count > 0)
            {
                MyGrid.ColumnDefinitions.RemoveAt(MyGrid.ColumnDefinitions.Count - 1);
            }
        } 
        #endregion


        #region Manipulating the Border
        #region Moving the Border

        private void MoveBorderUpButton_Click(object sender, RoutedEventArgs e)
        {
            var previousRow = Grid.GetRow(MovableBorder);
            if (previousRow > 0)
            {
                Grid.SetRow(MovableBorder, previousRow - 1);
            }
        }

        private void MoveBorderDownButton_Click(object sender, RoutedEventArgs e)
        {
            var previousRow = Grid.GetRow(MovableBorder);
            Grid.SetRow(MovableBorder, previousRow + 1);
        }

        private void MoveBorderLeftButton_Click(object sender, RoutedEventArgs e)
        {
            var previousColumn = Grid.GetColumn(MovableBorder);
            if (previousColumn > 0)
            {
                Grid.SetColumn(MovableBorder, previousColumn - 1);
            }
        }

        private void MoveBorderRightButton_Click(object sender, RoutedEventArgs e)
        {
            var previousColumn = Grid.GetColumn(MovableBorder);
            Grid.SetColumn(MovableBorder, previousColumn + 1);
        }
        #endregion

        #region Changing the Row/ColumnSpan of the Border
        private void DecreaseColumnSpanButton_Click(object sender, RoutedEventArgs e)
        {
            var previousColumnSpan = Grid.GetColumnSpan(MovableBorder);
            if (previousColumnSpan > 0)
            {
                Grid.SetColumnSpan(MovableBorder, previousColumnSpan - 1);
            }
        }
        private void IncreaseColumnSpanButton_Click(object sender, RoutedEventArgs e)
        {
            var previousColumnSpan = Grid.GetColumnSpan(MovableBorder);
            Grid.SetColumnSpan(MovableBorder, previousColumnSpan + 1);
        }

        private void DecreaseRowSpanButton_Click(object sender, RoutedEventArgs e)
        {
            var previousRowSpan = Grid.GetRowSpan(MovableBorder);
            if (previousRowSpan > 0)
            {
                Grid.SetRowSpan(MovableBorder, previousRowSpan - 1);
            }
        }
        private void IncreaseRowSpanButton_Click(object sender, RoutedEventArgs e)
        {
            var previousRowSpan = Grid.GetRowSpan(MovableBorder);
            Grid.SetRowSpan(MovableBorder, previousRowSpan + 1);
        }  
        #endregion
        #endregion


        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Grid_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/Grid/Grid_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Grid_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/Grid/Grid_Demo.xaml.cs"
                }
            });
        }

    }
}
