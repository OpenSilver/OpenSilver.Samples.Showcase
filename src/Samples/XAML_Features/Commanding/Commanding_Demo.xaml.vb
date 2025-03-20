Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows.Input
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("click", "interaction", "command")>
    Partial Public Class Commanding_Demo
        Inherits UserControl
        Private _myICommand As ICommand

        Public Sub New()
            Me.InitializeComponent()

            PrepareICommandTest()

        End Sub

        Private Sub PrepareICommandTest()
            Dim items As List(Of String) = New List(Of String)()
            items.Add("MessageBox Yay!")
            items.Add("TextBox Boo!")
            items.Add("MessageBox Wow!")
            Me.MyComboBoxForICommand.ItemsSource = items
            Me.MyComboBoxForICommand.SelectedIndex = 0
            items = New List(Of String)()
            items.Add("Display in TextBlock")
            items.Add("Display in MessageBox")
            Me.MyComboBoxForCommandTest.ItemsSource = items
            Me.MyComboBoxForCommandTest.SelectedIndex = 0
            Me.MyButtonForTestCommand.Command = New TestCommandInTextBlock(Me.MessageTextBlock)
        End Sub

        Private Sub ButtonTestICommand_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If _myICommand IsNot Nothing AndAlso _myICommand.CanExecute(Me.MessageTextTextBox) Then
                _myICommand.Execute(Me.MessageTextTextBox)
            End If
        End Sub

        Private Sub ComboBoxForCommandTest_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            Dim comboBox = CType(sender, ComboBox)
            Select Case comboBox.SelectedIndex
                Case 0
                    Me.MyButtonForTestCommand.Command = New TestCommandInTextBlock(Me.MessageTextBlock)
                Case Else
                    Me.MyButtonForTestCommand.Command = New TestCommandInMessageBox()
            End Select
        End Sub


        Private Sub MyComboBoxForICommand_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            Dim comboBox = CType(sender, ComboBox)
            Select Case comboBox.SelectedIndex
                Case 0
                    _myICommand = New TestICommandClass()
                Case 1
                    _myICommand = New TestICommandClass2()
                Case 2
                    _myICommand = New TestICommandClass3()
                Case Else
                    _myICommand = New TestICommandClass()
            End Select
        End Sub
        Public Class TestICommandClass
            Implements ICommand
            Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

            Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
                Return True
            End Function

            Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
                MessageBox.Show("Yay!")
            End Sub
        End Class

        Public Class TestICommandClass2
            Implements ICommand
            Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

            Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
                Return TypeOf parameter Is TextBox
            End Function

            Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
                If TypeOf parameter Is TextBox Then
                    CType(parameter, TextBox).Text = "Boo!"
                End If
            End Sub
        End Class

        Public Class TestICommandClass3
            Implements ICommand
            Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

            Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
                Return True
            End Function

            Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
                MessageBox.Show("Wow!")
            End Sub
        End Class

        Public Class TestCommandInTextBlock
            Implements ICommand
            Private _messageTextTextBlock As TextBlock

            Public Sub New(ByVal messageTextTextBlock As TextBlock)
                _messageTextTextBlock = messageTextTextBlock
            End Sub

            Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

            Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
                Return parameter IsNot Nothing AndAlso TypeOf parameter Is String
            End Function

            Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
                _messageTextTextBlock.Text = CStr(parameter)
            End Sub
        End Class
        Public Class TestCommandInMessageBox
            Implements ICommand
            Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

            Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
                Return parameter IsNot Nothing AndAlso TypeOf parameter Is String
            End Function

            Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
                MessageBox.Show(CStr(parameter))
            End Sub
        End Class
    End Class
End Namespace
