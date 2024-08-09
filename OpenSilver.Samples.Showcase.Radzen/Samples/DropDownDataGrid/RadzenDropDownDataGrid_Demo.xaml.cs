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
using System.Collections.ObjectModel;

namespace OpenSilver.Samples.Showcase
{
    public partial class RadzenDropDownDataGrid_Demo : UserControl
    {
        public RadzenDropDownDataGrid_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new DropDownData();
        }

        public class DropDownData : INotifyPropertyChanged
        {
            public Action<object> DropDownChangeDel;
            private string _selectedPlanetName;

            public string SelectedPlanetName
            {
                get { return _selectedPlanetName; }
                set { _selectedPlanetName = value; OnPropertyChanged(); }
            }

            public ObservableCollection<Planet> Planets = Planet.Planets;


            public DropDownData()
            {
                DropDownChangeDel = DropDownChange;
            }

            public void DropDownChange(object newValue)
            {
                SelectedPlanetName = newValue.ToString();
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
