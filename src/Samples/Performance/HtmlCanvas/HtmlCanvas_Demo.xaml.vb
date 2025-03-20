﻿Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("performance", "HTML canvas", "sprites", "FPS", "graphics")>
    Partial Public Class HtmlCanvas_Demo
        Inherits UserControl
        Private _lastTickCount As Integer = 0
        Private ReadOnly SpriteCount As Integer() = New Integer() {5, 10, 25, 50, 100, 250, 500, 1000, 2500}
        Private _loaded As Boolean = False

        ' Define variables to calculate the average "Frames Per Second":
        Private _counterToCalculateAverageFPS As Integer = 0
        Const NumberOfSecondsOverWhichToCalculateAverageFPS As Integer = 2
        Private _tickCountToCalculateAverageFPS As Integer = Environment.TickCount

        Public Sub New()
            Me.InitializeComponent()

            AddHandler Unloaded, AddressOf HtmlCanvas_Demo_Unloaded
        End Sub

        Private Async Sub HtmlCanvas_Demo_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not OpenSilver.Interop.IsRunningInTheSimulator Then
                ' Load the initial sprites:
                Me.ComboBoxToChooseNumberOfSprites.SelectedIndex = 1 ' This will raise the "SelectionChanged" event, which loads the sprites.

                ' Start the main drawing loop:
                _lastTickCount = Environment.TickCount
                _loaded = True
                Await MainLoopAsync()
            Else
                Visibility = Visibility.Collapsed
                MessageBox.Show("The Simulator is too slow to run this demo. Please run the demo in the browser instead.")
            End If
        End Sub

        Private Sub HtmlCanvas_Demo_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            _loaded = False ' This results in the main loop exiting.
        End Sub

        Private Sub ComboBox_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            ' Get the selected element in the ComboBox:
            Dim selectedIndex = CType(sender, ComboBox).SelectedIndex

            ' Get the number of sprites:
            Dim spriteCount = Me.SpriteCount(selectedIndex)

            ' Load the sprites:
            LoadSprites(spriteCount)
        End Sub

        Private Sub LoadSprites(ByVal spriteCount As Integer)
            Dim rand As Random = New Random()
            Dim canvasWidth As Double = Me.HtmlCanvas1.ActualWidth
            Dim canvasHeight As Double = Me.HtmlCanvas1.ActualHeight

            ' Remove the previous sprites, if any:
            Me.HtmlCanvas1.Children.Clear()

            ' Create the sprites:
            For i = 0 To spriteCount - 1
                ' Create a new sprite:
                Dim sprite = New TestPerformance.MainSprite(i)

                ' Set its position randomly:
                sprite.X = rand.Next(Math.Abs(CInt(canvasWidth) - CInt(sprite.Width)))
                sprite.Y = rand.Next(Math.Abs(CInt(canvasHeight) - CInt(sprite.Height)))

                ' Set its velocity randomly:
                sprite.VelocityX = rand.Next(-10, 10)
                sprite.VelocityY = rand.Next(-10, 10)

                ' Add the sprite to the HtmlCanvas:
                Me.HtmlCanvas1.Children.Add(sprite)
            Next
        End Sub

        Private Async Function MainLoopAsync() As Task
            If Not _loaded Then Return

            ' Determine the time elapsed since the last redraw:
            Dim newTickCount = Environment.TickCount
            Dim timeElapsedInMilliseconds = newTickCount - _lastTickCount
            _lastTickCount = newTickCount

            ' Determine the size of the window in pixels:
            Dim canvasWidth As Double = Me.HtmlCanvas1.ActualWidth
            Dim canvasHeight As Double = Me.HtmlCanvas1.ActualHeight

            ' Move the sprites:
            For Each child In Me.HtmlCanvas1.Children
                Dim timeFactor = timeElapsedInMilliseconds / 30.0R ' This is intended to adjust the movement so that, no matter the frames per second, the items always move at the same speed.

                Dim sprite = CType(child, TestPerformance.MainSprite)
                sprite.Move(sprite.VelocityX * timeFactor, sprite.VelocityY * timeFactor)

                If sprite.X < 0 Then
                    sprite.X = 0
                    sprite.VelocityX = Math.Abs(sprite.VelocityX)
                ElseIf sprite.X > canvasWidth Then
                    sprite.X = canvasWidth
                    sprite.VelocityX = -Math.Abs(sprite.VelocityX)
                End If
                If sprite.Y < 0 Then
                    sprite.Y = 0
                    sprite.VelocityY = Math.Abs(sprite.VelocityY)
                ElseIf sprite.Y > canvasHeight Then
                    sprite.Y = canvasHeight
                    sprite.VelocityY = -Math.Abs(sprite.VelocityY)
                End If
            Next

            ' Redraw:
            Me.HtmlCanvas1.Draw()

            ' Calculate the average "Frames Per Second":
            _counterToCalculateAverageFPS += 1
            Dim tickCountToCalculateAverageFPS = Environment.TickCount ' In milliseconds
            Dim durationSinceLastFPSCalculation = (tickCountToCalculateAverageFPS - _tickCountToCalculateAverageFPS) / 1000.0R ' In seconds
            If durationSinceLastFPSCalculation > NumberOfSecondsOverWhichToCalculateAverageFPS Then
                ' Divide the number of frames by the time elapsed:
                Dim framesPerSecond = _counterToCalculateAverageFPS / durationSinceLastFPSCalculation
                Me.FramesPerSecondTextBlock.Text = CInt(framesPerSecond).ToString()

                ' Reset counter:
                _counterToCalculateAverageFPS = 0
                _tickCountToCalculateAverageFPS = tickCountToCalculateAverageFPS
            End If

            Await Task.Delay(1)
            Await MainLoopAsync()
        End Function
    End Class
End Namespace
