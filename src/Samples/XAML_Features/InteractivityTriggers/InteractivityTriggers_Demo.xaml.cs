using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
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
    public partial class InteractivityTriggers_Demo : UserControl
    {
        public InteractivityTriggers_Demo()
        {
            this.InitializeComponent();

            this.DataContext = new TestViewModel();
        }

        public class TestViewModel
        {
            public TestViewModel()
            {
                TestCommand = new TestICommandClass();
            }

            public ICommand TestCommand { get; private set; }
        }

        public class TestICommandClass : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                MessageBox.Show("The command was successfully executed.");
            }
        }
    }
}
