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
    public partial class Commanding_Demo : UserControl
    {
        ICommand _myICommand;

        public Commanding_Demo()
        {
            this.InitializeComponent();

            PrepareICommandTest();

        }

        private void PrepareICommandTest()
        {
            List<string> items = new List<string>();
            items.Add("MessageBox Yay!");
            items.Add("TextBox Boo!");
            items.Add("MessageBox Wow!");
            MyComboBoxForICommand.ItemsSource = items;
            MyComboBoxForICommand.SelectedIndex = 0;
            items = new List<string>();
            items.Add("Display in TextBlock");
            items.Add("Display in MessageBox");
            MyComboBoxForCommandTest.ItemsSource = items;
            MyComboBoxForCommandTest.SelectedIndex = 0;
            MyButtonForTestCommand.Command = new TestCommandInTextBlock(MessageTextBlock);
        }

        private void ButtonTestICommand_Click(object sender, RoutedEventArgs e)
        {
            if (_myICommand != null && _myICommand.CanExecute(MessageTextTextBox))
            {
                _myICommand.Execute(MessageTextTextBox);
            }
        }

        private void ComboBoxForCommandTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    MyButtonForTestCommand.Command = new TestCommandInTextBlock(MessageTextBlock);
                    break;
                case 1:
                default:
                    MyButtonForTestCommand.Command = new TestCommandInMessageBox();
                    break;
            }
        }


        private void MyComboBoxForICommand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    _myICommand = new TestICommandClass();
                    break;
                case 1:
                    _myICommand = new TestICommandClass2();
                    break;
                case 2:
                    _myICommand = new TestICommandClass3();
                    break;
                default:
                    _myICommand = new TestICommandClass();
                    break;
            }
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
                MessageBox.Show("Yay!");
            }
        }

        public class TestICommandClass2 : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return parameter is TextBox;
            }

            public void Execute(object parameter)
            {
                if (parameter is TextBox)
                {
                    ((TextBox)parameter).Text = "Boo!";
                }
            }
        }

        public class TestICommandClass3 : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                MessageBox.Show("Wow!");
            }
        }

        public class TestCommandInTextBlock : ICommand
        {
            TextBlock _messageTextTextBlock;

            public TestCommandInTextBlock(TextBlock messageTextTextBlock)
            {
                _messageTextTextBlock = messageTextTextBlock;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return (parameter != null && parameter is string);
            }

            public void Execute(object parameter)
            {
                _messageTextTextBlock.Text = (string)parameter;
            }
        }
        public class TestCommandInMessageBox : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return (parameter != null && parameter is string);
            }

            public void Execute(object parameter)
            {
                MessageBox.Show((string)parameter);
            }
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Commanding_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Commanding/Commanding_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Commanding_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Commanding/Commanding_Demo.xaml.cs"
                }
            });
        }

    }
}
