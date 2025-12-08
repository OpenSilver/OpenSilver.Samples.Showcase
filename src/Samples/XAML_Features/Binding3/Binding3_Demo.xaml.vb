Imports OpenSilver.Samples.Showcase.Search
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("data", "MVVM", "binding", "UI")>
    Partial Public Class Binding3_Demo
        Inherits UserControl
        Private _listOfContacts As ObservableCollection(Of Person)

        Public Sub New()
            Me.InitializeComponent()

            Me.Title.Content = "Binding (3 of 3)"

            _listOfContacts = New ObservableCollection(Of Person)() From {
    New Person() With {
        .FirstName = "Albert",
        .LastName = "Einstein"
    },
    New Person() With {
        .FirstName = "Isaac",
        .LastName = "Newton"
    },
    New Person() With {
        .FirstName = "Galileo",
        .LastName = "Galilei"
    },
    New Person() With {
        .FirstName = "Marie",
        .LastName = "Curie"
    }
}
            Me.ItemsControl1.ItemsSource = _listOfContacts
        End Sub

        Friend Class Person
            Public Property FirstName As String
            Public Property LastName As String
        End Class

        Private Sub ButtonDelete_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim person = CType(CType(sender, Button).DataContext, Person)
            _listOfContacts.Remove(person)
        End Sub

        Private Sub ButtonAdd_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            _listOfContacts.Add(New Person() With {
                .FirstName = Me.FirstNameTextBox.Text,
                .LastName = Me.LastNameTextBox.Text
            })
        End Sub
    End Class
End Namespace
