Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("animation", "effects", "motion", "behavior", "storyboard", "tool", "transitions", "easing")>
    Partial Public Class AnimationTool_Demo
        Inherits UserControl

#Region "Private Fields"

        Private _currentStoryboard As Storyboard
        Private _currentEasingFunction As EasingFunctionBase = New CircleEase()
        Private _currentEasingMode As EasingMode = EasingMode.EaseOut

        Private ReadOnly Property RepeatForever As Boolean
            Get
                Return RepeatForeverCheckBox.IsChecked = True
            End Get
        End Property

        ' Original element properties for reset
        Private ReadOnly _originalRect As Rect
        Private ReadOnly _originalOpacity As Double
        Private _originalColor As Color

        ' XAML-defined storyboards
        Private ReadOnly _translationStoryboard As Storyboard
        Private ReadOnly _rotationStoryboard As Storyboard
        Private ReadOnly _scaleStoryboard As Storyboard
        Private ReadOnly _opacityStoryboard As Storyboard
        Private ReadOnly _colorStoryboard As Storyboard
        Private ReadOnly _keyframeStoryboard As Storyboard

#End Region

        Public Sub New()
            InitializeComponent()

            EasingModeComboBox.ItemsSource = [Enum].GetValues(GetType(EasingMode))
            FillBehaviorComboBox.ItemsSource = [Enum].GetValues(GetType(FillBehavior))

            ' Store original values
            _originalRect = New Rect(Canvas.GetLeft(AnimatedRectangle), Canvas.GetTop(AnimatedRectangle), AnimatedRectangle.Width, AnimatedRectangle.Height)
            _originalOpacity = AnimatedRectangle.Opacity
            _originalColor = CType(AnimatedRectangle.Background, SolidColorBrush).Color

            ' Initialize storyboard references from XAML resources
            _translationStoryboard = TryCast(Resources("TranslationAnimation"), Storyboard)
            _rotationStoryboard = TryCast(Resources("RotationAnimation"), Storyboard)
            _scaleStoryboard = TryCast(Resources("ScaleAnimation"), Storyboard)
            _opacityStoryboard = TryCast(Resources("OpacityAnimation"), Storyboard)
            _colorStoryboard = TryCast(Resources("ColorAnimation"), Storyboard)
            _keyframeStoryboard = TryCast(Resources("KeyframeAnimation"), Storyboard)

            SetupEventHandlers()
        End Sub

        Private Sub SetupEventHandlers()
            AddHandler AnimationTypeComboBox.SelectionChanged, AddressOf AnimationTypeComboBox_SelectionChanged
            AddHandler EasingFunctionComboBox.SelectionChanged, AddressOf EasingFunctionComboBox_SelectionChanged
            AddHandler EasingModeComboBox.SelectionChanged, AddressOf EasingModeComboBox_SelectionChanged
        End Sub

#Region "Event Handlers"

        Private Sub AnimationTypeComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            ResetElement()
        End Sub

        Private Sub EasingModeComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            SetEasingMode()
        End Sub

        Private Sub EasingFunctionComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            UpdateEasingFunction()
        End Sub

        Private Sub RepeatForeverCheckBoxStateChanged(sender As Object, e As RoutedEventArgs)
            RepeatCountLabel.IsEnabled = Not RepeatForever
            RepeatCountNumericUpDown.IsEnabled = Not RepeatForever
        End Sub

        Private Sub PlayButton_Click(sender As Object, e As RoutedEventArgs)
            UpdateEasingFunction()
            PlayAnimation()
            PauseResumeButton.IsEnabled = True
            StopButton.IsEnabled = True
            PauseResumeButton.Content = "Pause"
        End Sub

        Private Sub PauseResumeButton_Click(sender As Object, e As RoutedEventArgs)
            If _currentStoryboard Is Nothing Then
                Return
            End If

            If _currentStoryboard.GetIsPaused() Then
                ' Resume animation
                _currentStoryboard.Resume()
                PauseResumeButton.Content = "Pause"
            Else
                ' Pause animation
                _currentStoryboard.Pause()
                PauseResumeButton.Content = "Resume"
            End If
        End Sub

        Private Sub StopButton_Click(sender As Object, e As RoutedEventArgs)
            StopAnimation()
        End Sub

#End Region

#Region "Helper Methods"

        Private Sub SetEasingMode()
            If TypeOf EasingModeComboBox.SelectedItem Is EasingMode Then
                Dim mode As EasingMode = CType(EasingModeComboBox.SelectedItem, EasingMode)
                _currentEasingMode = mode
                _currentEasingFunction.EasingMode = mode
            End If
        End Sub

        Private Sub UpdateEasingFunction()
            ' Hide all property panels
            BackEaseProperties.Visibility = Visibility.Collapsed
            BounceEaseProperties.Visibility = Visibility.Collapsed
            ExponentialEaseProperties.Visibility = Visibility.Collapsed
            PowerEaseProperties.Visibility = Visibility.Collapsed

            ' Update the easing function based on selection but keep the current easing mode
            Select Case EasingFunctionComboBox.SelectedIndex
                Case 0 ' BackEase
                    _currentEasingFunction = New BackEase With {
                        .EasingMode = _currentEasingMode,
                        .Amplitude = BackEaseAmplitudeNumericUpDown.Value
                    }
                    BackEaseProperties.Visibility = Visibility.Visible

                Case 1 ' BounceEase
                    _currentEasingFunction = New BounceEase With {
                        .EasingMode = _currentEasingMode,
                        .Bounces = CInt(BounceEaseBouncesNumericUpDown.Value),
                        .Bounciness = BounceEaseBouncinesNumericUpDown.Value
                    }
                    BounceEaseProperties.Visibility = Visibility.Visible

                Case 2 ' CircleEase
                    _currentEasingFunction = New CircleEase With {.EasingMode = _currentEasingMode}

                Case 3 ' CubicEase
                    _currentEasingFunction = New CubicEase With {.EasingMode = _currentEasingMode}

                Case 4 ' ExponentialEase
                    _currentEasingFunction = New ExponentialEase With {
                        .EasingMode = _currentEasingMode,
                        .Exponent = ExponentialEaseExponentNumericUpDown.Value
                    }
                    ExponentialEaseProperties.Visibility = Visibility.Visible

                Case 5 ' PowerEase
                    _currentEasingFunction = New PowerEase With {
                        .EasingMode = _currentEasingMode,
                        .Power = PowerEasePowerNumericUpDown.Value
                    }
                    PowerEaseProperties.Visibility = Visibility.Visible

                Case 6 ' QuadraticEase
                    _currentEasingFunction = New QuadraticEase With {.EasingMode = _currentEasingMode}

                Case 7 ' QuarticEase
                    _currentEasingFunction = New QuarticEase With {.EasingMode = _currentEasingMode}

                Case 8 ' QuinticEase
                    _currentEasingFunction = New QuinticEase With {.EasingMode = _currentEasingMode}

                Case 9 ' SineEase
                    _currentEasingFunction = New SineEase With {.EasingMode = _currentEasingMode}
            End Select
        End Sub

        Private Sub PlayAnimation()
            ' Stop any currently running animation
            StopCurrentAnimation()

            ' Configure and play the selected animation
            Select Case AnimationTypeComboBox.SelectedIndex
                Case 0 ' Translation (Move)
                    _currentStoryboard = _translationStoryboard
                    ConfigureAnimation(_currentStoryboard)

                Case 1 ' Rotation
                    If Not (TypeOf AnimatedRectangle.RenderTransform Is RotateTransform) Then
                        AnimatedRectangle.RenderTransform = New RotateTransform()
                    End If
                    _currentStoryboard = _rotationStoryboard
                    ConfigureAnimation(_currentStoryboard)

                Case 2 ' Scale
                    If Not (TypeOf AnimatedRectangle.RenderTransform Is ScaleTransform) Then
                        AnimatedRectangle.RenderTransform = New ScaleTransform()
                    End If
                    _currentStoryboard = _scaleStoryboard
                    ConfigureAnimation(_currentStoryboard)

                Case 3 ' Opacity
                    _currentStoryboard = _opacityStoryboard
                    ConfigureAnimation(_currentStoryboard)

                Case 4 ' Color Change
                    _currentStoryboard = _colorStoryboard
                    ConfigureAnimation(_currentStoryboard)

                Case 5 ' Keyframe Animation
                    If Not (TypeOf AnimatedRectangle.RenderTransform Is RotateTransform) Then
                        AnimatedRectangle.RenderTransform = New RotateTransform()
                    End If
                    _currentStoryboard = _keyframeStoryboard
                    ConfigureAnimation(_currentStoryboard)
            End Select

            ' Subscribe to the Completed event 
            AddHandler _currentStoryboard.Completed, AddressOf Storyboard_Completed

            ' Start the animation
            _currentStoryboard.Begin()
        End Sub

        Private Sub ConfigureAnimation(storyboard As Storyboard)
            ' Apply common settings to all animations in the storyboard
            For Each timeline As Timeline In storyboard.Children
                timeline.Duration = TimeSpan.FromSeconds(DurationNumericUpDown.Value)
                timeline.RepeatBehavior = If(RepeatForever, RepeatBehavior.Forever, New RepeatBehavior(RepeatCountNumericUpDown.Value))
                timeline.FillBehavior = CType(FillBehaviorComboBox.SelectedItem, FillBehavior)
                timeline.BeginTime = TimeSpan.FromSeconds(BeginTimeNumericUpDown.Value)
                timeline.SpeedRatio = SpeedRatioNumericUpDown.Value
                timeline.AutoReverse = CBool(AutoReverseCheckBox.IsChecked)

                ' Apply easing function to animations that support it
                If TypeOf timeline Is DoubleAnimation Then
                    Dim doubleAnimation As DoubleAnimation = CType(timeline, DoubleAnimation)
                    doubleAnimation.EasingFunction = _currentEasingFunction
                ElseIf TypeOf timeline Is ColorAnimation Then
                    Dim colorAnimation As ColorAnimation = CType(timeline, ColorAnimation)
                    colorAnimation.EasingFunction = _currentEasingFunction
                ElseIf TypeOf timeline Is DoubleAnimationUsingKeyFrames Then
                    Dim keyframeAnimation As DoubleAnimationUsingKeyFrames = CType(timeline, DoubleAnimationUsingKeyFrames)
                    For Each keyframe In keyframeAnimation.KeyFrames.OfType(Of EasingDoubleKeyFrame)()
                        If TypeOf keyframe.EasingFunction Is EasingFunctionBase Then
                            Dim easingFunction As EasingFunctionBase = CType(keyframe.EasingFunction, EasingFunctionBase)
                            ' Update the easing mode but preserve the specific easing function type
                            easingFunction.EasingMode = _currentEasingMode
                        End If
                    Next
                End If
            Next
        End Sub

        Private Sub Storyboard_Completed(sender As Object, e As EventArgs)
            ' If not set to repeat forever, disable the control buttons
            If Not RepeatForever Then
                StopButton.IsEnabled = False
                PauseResumeButton.IsEnabled = False
                PauseResumeButton.Content = "Pause"
            End If
        End Sub

        Private Sub StopAnimation()
            StopCurrentAnimation()
            ResetElement()
            StopButton.IsEnabled = False
            PauseResumeButton.IsEnabled = False
            PauseResumeButton.Content = "Pause"
        End Sub

        Private Sub StopCurrentAnimation()
            If _currentStoryboard IsNot Nothing Then
                RemoveHandler _currentStoryboard.Completed, AddressOf Storyboard_Completed
                _currentStoryboard.Stop()
            End If
        End Sub

        Private Sub ResetElement()
            ' Stop any running animations
            StopCurrentAnimation()
            _currentStoryboard = Nothing

            ' Reset to original position and properties
            Canvas.SetLeft(AnimatedRectangle, _originalRect.X)
            Canvas.SetTop(AnimatedRectangle, _originalRect.Y)
            AnimatedRectangle.Width = _originalRect.Width
            AnimatedRectangle.Height = _originalRect.Height
            AnimatedRectangle.Opacity = _originalOpacity
            AnimatedRectangle.Background = New SolidColorBrush(_originalColor)

            ResetTransforms()
        End Sub

        Private Sub ResetTransforms()
            If TypeOf AnimatedRectangle.RenderTransform Is RotateTransform Then
                Dim rotateTransform As RotateTransform = CType(AnimatedRectangle.RenderTransform, RotateTransform)
                rotateTransform.Angle = 0
            ElseIf TypeOf AnimatedRectangle.RenderTransform Is ScaleTransform Then
                Dim scaleTransform As ScaleTransform = CType(AnimatedRectangle.RenderTransform, ScaleTransform)
                scaleTransform.ScaleX = 1
                scaleTransform.ScaleY = 1
            End If
        End Sub

#End Region
    End Class
End Namespace
