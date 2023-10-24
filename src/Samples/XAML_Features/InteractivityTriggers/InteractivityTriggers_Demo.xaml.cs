using System;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

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
