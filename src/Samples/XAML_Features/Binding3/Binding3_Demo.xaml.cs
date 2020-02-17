using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CSHTML5.Samples.Showcase
{
    public partial class Binding3_Demo : UserControl
    {
        ObservableCollection<Person> _listOfContacts;

        public Binding3_Demo()
        {
            this.InitializeComponent();

            _listOfContacts = new ObservableCollection<Person>()
            {
                new Person() { FirstName = "Albert", LastName = "Einstein" },
                new Person() { FirstName = "Isaac", LastName = "Newton" },
                new Person() { FirstName = "Galileo", LastName = "Galilei" },
                new Person() { FirstName = "Marie", LastName = "Curie" },
            };
            ItemsControl1.ItemsSource = _listOfContacts;
        }

        class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var person = (Person)((Button)sender).DataContext;
            _listOfContacts.Remove(person);
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            _listOfContacts.Add(new Person() { FirstName = FirstNameTextBox.Text, LastName = LastNameTextBox.Text });
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Binding3_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding3/Binding3_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Binding3_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding3/Binding3_Demo.xaml.cs"
                }
            });
        }

    }
}
