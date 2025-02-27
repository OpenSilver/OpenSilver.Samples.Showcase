using OpenSilver.Samples.Showcase.Search;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("data", "MVVM", "binding", "UI")]
    public partial class Binding3_Demo : UserControl
    {
        ObservableCollection<Person> _listOfContacts;

        public Binding3_Demo()
        {
            this.InitializeComponent();

            Title.Content = "Binding (3 of 3)";

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
    }
}
