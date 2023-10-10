Imports System
Imports System.Collections.Generic
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
#Else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#End If

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class Validation_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
            Me.ValidationBorder.DataContext = New Person()
        End Sub

        Private Sub ValidationBorder_BindingValidationError(ByVal sender As Object, ByVal e As ValidationErrorEventArgs)
            ' Disable the "OK" button if there are validation errors. This method is called when validation
            ' errors appear or disappear.
            If Validation.GetHasError(Me.NameTextBoxForValidation) OrElse Validation.GetHasError(Me.AgeTextBoxForValidation) Then
                Me.ButtonOK.IsEnabled = False
            Else
                Me.ButtonOK.IsEnabled = True
            End If
        End Sub

        Private Sub ButtonOK_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ' Force refresh validation of the "Name" field because the initial value of "Name" in the
            ' ViewModel is String.Empty, and the validation process only happens when the user types
            ' text inside the TextBox (note: this is the same behavior as in WPF and Silverlight):
            Dim binding As BindingExpression = Me.NameTextBoxForValidation.GetBindingExpression(TextBox.TextProperty)
            binding.UpdateSource()

            ' If there are no validation errors, display a message:
            If Not Validation.GetHasError(Me.NameTextBoxForValidation) AndAlso Not Validation.GetHasError(Me.AgeTextBoxForValidation) Then
                Dim person = CType(CType(sender, Button).DataContext, Person)
                Dim str As String = "Name: """ & person.Name & """" & Environment.NewLine & "Age: " & person.Age.ToString() & "."
                MessageBox.Show(str)
            Else
                MessageBox.Show("Please fix the validation errors.")
            End If
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Validation_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Validation/Validation_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Validation_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Validation/Validation_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Person.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Validation/Person.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Validation_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Validation/Validation_Demo.xaml.vb"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Person.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Validation/Person.vb"
    }
})
        End Sub

    End Class
End Namespace
