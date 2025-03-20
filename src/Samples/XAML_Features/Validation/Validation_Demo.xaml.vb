﻿Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("input", "form", "data entry", "error handling", "UI")>
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
    End Class
End Namespace
