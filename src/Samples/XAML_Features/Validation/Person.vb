Imports System.ComponentModel

Namespace Global.OpenSilver.Samples.Showcase
    'Validation:
    Public Class Person
        Implements INotifyPropertyChanged
        Private _name As String
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                If String.IsNullOrWhiteSpace(value) Then
                    Throw New Exception("Name cannot be empty.")
                End If
                _name = value
                RaisePropertyChanged("Name")
            End Set
        End Property

        Private _age As Integer = 1
        Public Property Age As Integer
            Get
                Return _age
            End Get
            Set(ByVal value As Integer)
                If value <= 0 Then
                    Throw New Exception("Age cannot be lower than 0.")
                End If
                _age = value
                RaisePropertyChanged("Age")
            End Set
        End Property

        Private Sub RaisePropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace
