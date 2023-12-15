Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Ink
Imports System.Windows.Input

Namespace OpenSilver.Samples.Showcase
    Partial Public Class InkPresenter_Demo
        Inherits UserControl
        Implements INotifyPropertyChanged

        Private LastStroke As Stroke
        Private nextStrokes As Stack(Of Stroke) = New Stack(Of Stroke)()
        'Public Event PropertyChanged As PropertyChangedEventHandler
        Private Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Sub New()
            InitializeComponent()
        End Sub

        Private _canClearStrokes As Boolean

        Public Property CanClearStrokes As Boolean
            Get
                Return _canClearStrokes
            End Get
            Set(ByVal value As Boolean)
                _canClearStrokes = value
                OnPropertyChanged()
            End Set
        End Property

        Private _canUndoStroke As Boolean

        Public Property CanUndoStroke As Boolean
            Get
                Return _canUndoStroke
            End Get
            Set(ByVal value As Boolean)
                _canUndoStroke = value
                OnPropertyChanged()
            End Set
        End Property

        Private _canRedoStroke As Boolean

        Public Property CanRedoStroke As Boolean
            Get
                Return _canRedoStroke
            End Get
            Set(ByVal value As Boolean)
                _canRedoStroke = value
                OnPropertyChanged()
            End Set
        End Property

        Private Sub OnIP_MouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            InkPad.CaptureMouse()
            Dim MyStylusPointCollection As StylusPointCollection = New StylusPointCollection()
            MyStylusPointCollection.Add(e.StylusDevice.GetStylusPoints(InkPad))
            LastStroke = New Stroke(MyStylusPointCollection)
            InkPad.Strokes.Add(LastStroke)
            CanUndoStroke = True
            CanClearStrokes = True

            CanRedoStroke = False
            nextStrokes.Clear()

        End Sub

        Private Sub OnIP_MouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            InkPad.ReleaseMouseCapture()
        End Sub

        Private Sub OnIP_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            If LastStroke IsNot Nothing AndAlso InkPad.IsMouseCaptured Then
                LastStroke.StylusPoints.Add(e.StylusDevice.GetStylusPoints(InkPad))
            End If
        End Sub

        Private Sub OnIP_LostMouseCapture(ByVal sender As Object, ByVal e As MouseEventArgs)
        End Sub

        Private Sub OnClearInkPad(ByVal sender As Object, ByVal e As RoutedEventArgs)
            LastStroke = Nothing
            nextStrokes.Clear()
            InkPad.Strokes.Clear()
            CanClearStrokes = False
            CanUndoStroke = False
            CanRedoStroke = False
        End Sub

        Private Sub OnUndoLastStroke(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim strokes = InkPad.Strokes

            If strokes.Count > 0 Then
                nextStrokes.Push(strokes(strokes.Count - 1))
                strokes.RemoveAt(strokes.Count - 1)
                CanRedoStroke = True
            End If

            If strokes.Count = 0 Then
                CanUndoStroke = False
            End If
        End Sub

        Private Sub OnRedoLastStroke(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If nextStrokes.Count > 0 Then
                InkPad.Strokes.Add(nextStrokes.Pop())
                CanUndoStroke = True
            End If
            If nextStrokes.Count = 0 Then
                CanRedoStroke = False
            End If
        End Sub

        Private Sub OnPropertyChanged(
<CallerMemberName> ByVal Optional propertyName As String = "")
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class
End Namespace

