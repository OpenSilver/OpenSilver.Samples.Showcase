namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Collections.ObjectModel
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.Foundation
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Navigation
#endif

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

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Binding3_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding3/Binding3_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Binding3_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding3/Binding3_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "Binding3_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding3/Binding3_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "Binding3_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Binding3/Binding3_Demo.xaml.fs")
        ])
