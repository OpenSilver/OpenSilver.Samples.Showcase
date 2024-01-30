namespace OpenSilver.Samples.Showcase

open System.Collections.ObjectModel
open System.Windows
open System.Windows.Controls

type Person3() =
    member val FirstName = "" with get, set
    member val LastName = "" with get, set

type Binding3_Demo() as this =
    inherit Binding3_DemoXaml()

    let mutable _listOfContacts : ObservableCollection<Person3> = ObservableCollection<Person3>()

    do
        this.InitializeComponent()
        this.Title.Content <- "Binding (3 of 3)"

        _listOfContacts <- ObservableCollection<Person3>
            [
                Person3 ( FirstName = "Albert", LastName = "Einstein" );
                Person3 ( FirstName = "Isaac", LastName = "Newton" );
                Person3 ( FirstName = "Galileo", LastName = "Galilei" );
                Person3 ( FirstName = "Marie", LastName = "Curie" )
            ]

        this.ItemsControl1.ItemsSource <- _listOfContacts

    member private this.ButtonDelete_Click(sender : obj, e : RoutedEventArgs) =
        let person = (sender :?> Button).DataContext :?> Person3
        _listOfContacts.Remove(person) |> ignore

    member private this.ButtonAdd_Click(sender : obj, e : RoutedEventArgs) =
        _listOfContacts.Add(Person3(FirstName = this.FirstNameTextBox.Text, LastName = this.LastNameTextBox.Text)) |> ignore
