using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CSHTML5.Samples.Showcase
{
    public partial class Binding_Demo : UserControl
    {
        ObservableCollection<Person> _listOfContacts;


        public Binding_Demo()
        {
            this.InitializeComponent();

            //-------------
            // First Demo
            //-------------
            ObservableCollection<Planet> listOfPlanets = Planet.GetListOfPlanets();
            ItemsControl1.ItemsSource = listOfPlanets;
            ContentControl1.Content = listOfPlanets[0];

            //-------------
            // Second Demo
            //-------------
            //SHOCASETODO : disabled because of non streaching from parent
            /*_listOfContacts = new ObservableCollection<Person>()
            {
                new Person() { FirstName = "Albert", LastName = "Einstein" },
                new Person() { FirstName = "Isaac", LastName = "Newton" },
                new Person() { FirstName = "Galileo", LastName = "Galilei" },
                new Person() { FirstName = "Marie", LastName = "Curie" },
            };
            ItemsControl2.ItemsSource = _listOfContacts;*/
        }

        //-------------
        // First Demo
        //-------------

        private void ButtonPlanet_Click(object sender, RoutedEventArgs e)
        {
            var planet = ((Button)sender).DataContext;
            ContentControl1.Content = planet;
        }

        //-------------
        // Second Demo
        //-------------

        class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        /*
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var person = (Person)((Button)sender).DataContext;
            _listOfContacts.Remove(person);
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            _listOfContacts.Add(new Person() { FirstName = FirstNameTextBox.Text, LastName = LastNameTextBox.Text });
        }*/

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Binding_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Features/Binding/Binding_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Binding_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Features/Binding/Binding_Demo.xaml.cs"
                }
            });
        }

    }
}
