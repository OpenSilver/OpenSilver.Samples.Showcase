using OpenSilver.Samples.Showcase.DevExpressModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    public partial class DevExpressCarousel_Demo : UserControl
    {
        TestCarouselClass _carouselClass = new TestCarouselClass();
        public DevExpressCarousel_Demo()
        {
            this.DataContext = _carouselClass;
            this.InitializeComponent();
        }
    }

    public class TestCarouselClass : INotifyPropertyChanged
    {

        private ObservableCollection<Planet> _planets = Planet.Planets;
        public ObservableCollection<Planet> Planets
        {
            get { return _planets; }
            set { _planets = value; OnPropertyChanged(); }
        }

        //Note: we have to use a Binding for LoopNavigationEnabled because if it is true before the data is properly initialized, we end up with an error.
        private bool _isLooping = true;
        public bool IsLooping
        {
            get { return _isLooping; }
            set { _isLooping = value; OnPropertyChanged(); }
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
