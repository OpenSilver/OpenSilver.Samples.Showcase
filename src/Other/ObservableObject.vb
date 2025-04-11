Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Namespace OpenSilver.Samples.Showcase

    Public MustInherit Class ObservableObject
        Implements INotifyPropertyChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Protected Function SetProperty(Of T)(ByRef field As T, newValue As T, <CallerMemberName> Optional propertyName As String = Nothing) As Boolean
            If EqualityComparer(Of T).Default.Equals(field, newValue) Then
                Return False
            End If

            field = newValue
            OnPropertyChanged(propertyName)
            Return True
        End Function
    End Class

End Namespace
