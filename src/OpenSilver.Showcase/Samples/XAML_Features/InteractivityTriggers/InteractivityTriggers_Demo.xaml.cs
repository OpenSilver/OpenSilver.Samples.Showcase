using OpenSilver.Showcase.Search;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OpenSilver.Showcase
{
    [SearchKeywords("interaction", "triggers", "behavior", "events", "UI", "invokecommandaction", "command")]
    public partial class InteractivityTriggers_Demo : UserControl
    {
        public InteractivityTriggers_Demo()
        {
            InitializeComponent();

            DataContext = new TestViewModel();
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
