Imports System.Runtime.CompilerServices
Imports System.Windows.Input

Namespace OpenSilver.Samples.Showcase

    Public Interface IRelayCommand
        Inherits ICommand
        Sub NotifyCanExecuteChanged()
    End Interface

    Public NotInheritable Class RelayCommand
        Implements IRelayCommand

        Private ReadOnly _execute As Action
        Private ReadOnly _canExecute As Func(Of Boolean)

        Public Sub New(execute As Action)
            If execute Is Nothing Then Throw New ArgumentNullException(NameOf(execute))
            _execute = execute
        End Sub

        Public Sub New(execute As Action, canExecute As Func(Of Boolean))
            If execute Is Nothing Then Throw New ArgumentNullException(NameOf(execute))
            If canExecute Is Nothing Then Throw New ArgumentNullException(NameOf(canExecute))
            _execute = execute
            _canExecute = canExecute
        End Sub

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub NotifyCanExecuteChanged() Implements IRelayCommand.NotifyCanExecuteChanged
            RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return If(_canExecute?.Invoke(), True)
        End Function

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            _execute()
        End Sub
    End Class

    Public Interface IRelayCommand(Of In T)
        Inherits IRelayCommand
        Function CanExecute(parameter As T) As Boolean
        Sub Execute(parameter As T)
    End Interface

    Public NotInheritable Class RelayCommand(Of T)
        Implements IRelayCommand(Of T)

        Private ReadOnly _execute As Action(Of T)
        Private ReadOnly _canExecute As Predicate(Of T)

        Public Sub New(execute As Action(Of T))
            If execute Is Nothing Then Throw New ArgumentNullException(NameOf(execute))
            _execute = execute
        End Sub

        Public Sub New(execute As Action(Of T), canExecute As Predicate(Of T))
            If execute Is Nothing Then Throw New ArgumentNullException(NameOf(execute))
            If canExecute Is Nothing Then Throw New ArgumentNullException(NameOf(canExecute))
            _execute = execute
            _canExecute = canExecute
        End Sub

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub NotifyCanExecuteChanged() Implements IRelayCommand.NotifyCanExecuteChanged
            RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
        End Sub

        Public Function CanExecute(parameter As T) As Boolean Implements IRelayCommand(Of T).CanExecute
            Return If(_canExecute?.Invoke(parameter), True)
        End Function

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            If parameter Is Nothing AndAlso GetType(T).IsValueType AndAlso Nullable.GetUnderlyingType(GetType(T)) Is Nothing Then
                Return False
            End If

            Dim result As T
            If Not TryGetCommandArgument(parameter, result) Then
                ThrowArgumentExceptionForInvalidCommandArgument(parameter)
            End If

            Return CanExecute(result)
        End Function

        Public Sub Execute(parameter As T) Implements IRelayCommand(Of T).Execute
            _execute(parameter)
        End Sub

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            Dim result As T
            If Not TryGetCommandArgument(parameter, result) Then
                ThrowArgumentExceptionForInvalidCommandArgument(parameter)
            End If

            Execute(result)
        End Sub

        Private Shared Function TryGetCommandArgument(parameter As Object, ByRef result As T) As Boolean
            If parameter Is Nothing AndAlso GetType(T).IsClass Then
                result = Nothing
                Return True
            End If

            If TypeOf parameter Is T Then
                result = CType(parameter, T)
                Return True
            End If

            result = Nothing
            Return False
        End Function

        Private Shared Sub ThrowArgumentExceptionForInvalidCommandArgument(parameter As Object)
            If parameter Is Nothing Then
                Throw New ArgumentException($"Parameter must not be null. Command requires argument of type {GetType(T).Name}.", NameOf(parameter))
            End If

            Throw New ArgumentException($"Parameter is of type {parameter.GetType().Name}, but expected {GetType(T).Name}.", NameOf(parameter))
        End Sub

    End Class

End Namespace
