using MudBlazor;
using OpenSilver.Blazor;
using System;
using System.Collections.Generic;
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

namespace OpenSilver.Showcase
{
    public partial class MudBlazorButton_Demo : UserControl
    {
        public MudBlazorButton_Demo()
        {
            this.InitializeComponent();
            this.DataContext = new TestButtonClickClass();
        }
    }

    public class TestButtonClickClass : INotifyPropertyChanged
    {
        public Action ButtonClickDel;

        public TestButtonClickClass()
        {
            ButtonClickDel = ButtonClick;
        }

        private List<Variant> _variants;
        public List<Variant> Variants
        {
            get
            {
                if (_variants == null)
                {
                    _variants = new List<Variant>() { Variant.Filled, Variant.Text, Variant.Outlined };
                }
                return _variants;
            }
        }


        private Variant _currentVariant = Variant.Text;
        public Variant CurrentVariant
        {
            get { return _currentVariant; }
            set { _currentVariant = value; Console.WriteLine("CurrentVariant changed to " + value.ToString()); OnPropertyChanged(); }
        }



        public void ButtonClick()
        {
            CurrentVariant = CurrentVariant switch
            {
                Variant.Filled => Variant.Outlined,
                Variant.Outlined => Variant.Text,
                _ => Variant.Filled
            };
            //MessageBox.Show("You clicked me!");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
