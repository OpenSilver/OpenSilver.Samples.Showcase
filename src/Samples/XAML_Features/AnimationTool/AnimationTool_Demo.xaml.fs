namespace OpenSilver.Samples.Showcase

open System
open System.Linq
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Media.Animation
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("animation", "effects", "motion", "behavior", "storyboard", "tool", "transitions", "easing")>]
type AnimationTool_Demo() as this =
    inherit AnimationTool_DemoXaml()

    // Private Fields
    let mutable _currentStoryboard : Storyboard = null
    let mutable _currentEasingFunction : EasingFunctionBase = new CircleEase() :> EasingFunctionBase
    let mutable _currentEasingMode : EasingMode = EasingMode.EaseOut

    // Original element properties for reset
    let mutable _originalRect : Rect = Rect()
    let mutable _originalOpacity : float = 0.0
    let mutable _originalColor : Color = Color()

    // XAML-defined storyboards
    let mutable _translationStoryboard : Storyboard = null
    let mutable _rotationStoryboard : Storyboard = null
    let mutable _scaleStoryboard : Storyboard = null
    let mutable _opacityStoryboard : Storyboard = null
    let mutable _colorStoryboard : Storyboard = null
    let mutable _keyframeStoryboard : Storyboard = null

    // Helper property
    member private this.RepeatForever = 
        this.RepeatForeverCheckBox.IsChecked.HasValue && this.RepeatForeverCheckBox.IsChecked.Value

    // Event Handlers
    member private this.AnimationTypeComboBox_SelectionChanged(sender : obj, e : SelectionChangedEventArgs) =
        this.ResetElement()

    member private this.EasingModeComboBox_SelectionChanged(sender : obj, e : SelectionChangedEventArgs) =
        this.SetEasingMode()

    member private this.EasingFunctionComboBox_SelectionChanged(sender : obj, e : SelectionChangedEventArgs) =
        this.UpdateEasingFunction()

    member private this.RepeatForeverCheckBoxStateChanged(sender : obj, e : RoutedEventArgs) =
        this.RepeatCountLabel.IsEnabled <- not this.RepeatForever
        this.RepeatCountNumericUpDown.IsEnabled <- not this.RepeatForever

    member private this.PlayButton_Click(sender : obj, e : RoutedEventArgs) =
        this.UpdateEasingFunction()
        this.PlayAnimation()
        this.PauseResumeButton.IsEnabled <- true
        this.StopButton.IsEnabled <- true
        this.PauseResumeButton.Content <- "Pause"

    member private this.PauseResumeButton_Click(sender : obj, e : RoutedEventArgs) =
        if _currentStoryboard = null then
            ()
        else
            if _currentStoryboard.GetIsPaused() then
                // Resume animation
                _currentStoryboard.Resume()
                this.PauseResumeButton.Content <- "Pause"
            else
                // Pause animation
                _currentStoryboard.Pause()
                this.PauseResumeButton.Content <- "Resume"

    member private this.StopButton_Click(sender : obj, e : RoutedEventArgs) =
        this.StopAnimation()

    // Helper Methods
    //member private this.SetupEventHandlers() =
    //    this.AnimationTypeComboBox.SelectionChanged.Add(this.AnimationTypeComboBox_SelectionChanged)
    //    this.EasingFunctionComboBox.SelectionChanged.Add(this.EasingFunctionComboBox_SelectionChanged)
    //    this.EasingModeComboBox.SelectionChanged.Add(this.EasingModeComboBox_SelectionChanged)

    member private this.SetEasingMode() =
        match this.EasingModeComboBox.SelectedItem with
        | :? EasingMode as mode ->
            _currentEasingMode <- mode
            _currentEasingFunction.EasingMode <- mode
        | _ -> ()

    member private this.UpdateEasingFunction() =
        // Hide all property panels
        this.BackEaseProperties.Visibility <- Visibility.Collapsed
        this.BounceEaseProperties.Visibility <- Visibility.Collapsed
        this.ExponentialEaseProperties.Visibility <- Visibility.Collapsed
        this.PowerEaseProperties.Visibility <- Visibility.Collapsed

        // Update the easing function based on selection but keep the current easing mode
        match this.EasingFunctionComboBox.SelectedIndex with
        | 0 -> // BackEase
            _currentEasingFunction <- new BackEase(
                EasingMode = _currentEasingMode,
                Amplitude = this.BackEaseAmplitudeNumericUpDown.Value
            ) :> EasingFunctionBase
            this.BackEaseProperties.Visibility <- Visibility.Visible
        | 1 -> // BounceEase
            _currentEasingFunction <- new BounceEase(
                EasingMode = _currentEasingMode,
                Bounces = int this.BounceEaseBouncesNumericUpDown.Value,
                Bounciness = this.BounceEaseBouncinesNumericUpDown.Value
            ) :> EasingFunctionBase
            this.BounceEaseProperties.Visibility <- Visibility.Visible
        | 2 -> // CircleEase
            _currentEasingFunction <- new CircleEase(EasingMode = _currentEasingMode) :> EasingFunctionBase
        | 3 -> // CubicEase
            _currentEasingFunction <- new CubicEase(EasingMode = _currentEasingMode) :> EasingFunctionBase
        | 4 -> // ExponentialEase
            _currentEasingFunction <- new ExponentialEase(
                EasingMode = _currentEasingMode,
                Exponent = this.ExponentialEaseExponentNumericUpDown.Value
            ) :> EasingFunctionBase
            this.ExponentialEaseProperties.Visibility <- Visibility.Visible
        | 5 -> // PowerEase
            _currentEasingFunction <- new PowerEase(
                EasingMode = _currentEasingMode,
                Power = this.PowerEasePowerNumericUpDown.Value
            ) :> EasingFunctionBase
            this.PowerEaseProperties.Visibility <- Visibility.Visible
        | 6 -> // QuadraticEase
            _currentEasingFunction <- new QuadraticEase(EasingMode = _currentEasingMode) :> EasingFunctionBase
        | 7 -> // QuarticEase
            _currentEasingFunction <- new QuarticEase(EasingMode = _currentEasingMode) :> EasingFunctionBase
        | 8 -> // QuinticEase
            _currentEasingFunction <- new QuinticEase(EasingMode = _currentEasingMode) :> EasingFunctionBase
        | 9 -> // SineEase
            _currentEasingFunction <- new SineEase(EasingMode = _currentEasingMode) :> EasingFunctionBase
        | _ -> ()

    member private this.PlayAnimation() =
        // Stop any currently running animation
        this.StopCurrentAnimation()

        // Configure and play the selected animation
        match this.AnimationTypeComboBox.SelectedIndex with
        | 0 -> // Translation (Move)
            _currentStoryboard <- _translationStoryboard
            this.ConfigureAnimation(_currentStoryboard)
        | 1 -> // Rotation
            match this.AnimatedRectangle.RenderTransform with
            | :? RotateTransform -> ()
            | _ -> this.AnimatedRectangle.RenderTransform <- new RotateTransform()
            _currentStoryboard <- _rotationStoryboard
            this.ConfigureAnimation(_currentStoryboard)
        | 2 -> // Scale
            match this.AnimatedRectangle.RenderTransform with
            | :? ScaleTransform -> ()
            | _ -> this.AnimatedRectangle.RenderTransform <- new ScaleTransform()
            _currentStoryboard <- _scaleStoryboard
            this.ConfigureAnimation(_currentStoryboard)
        | 3 -> // Opacity
            _currentStoryboard <- _opacityStoryboard
            this.ConfigureAnimation(_currentStoryboard)
        | 4 -> // Color Change
            _currentStoryboard <- _colorStoryboard
            this.ConfigureAnimation(_currentStoryboard)
        | 5 -> // Keyframe Animation
            match this.AnimatedRectangle.RenderTransform with
            | :? RotateTransform -> ()
            | _ -> this.AnimatedRectangle.RenderTransform <- new RotateTransform()
            _currentStoryboard <- _keyframeStoryboard
            this.ConfigureAnimation(_currentStoryboard)
        | _ -> ()

        // Subscribe to the Completed event 
        if _currentStoryboard <> null then
            //_currentStoryboard.Completed.AddHandler(EventHandler(this.Storyboard_Completed))
            // Start the animation
            _currentStoryboard.Begin()

    member private this.ConfigureAnimation(storyboard : Storyboard) =
        // Apply common settings to all animations in the storyboard
        for timeline in storyboard.Children do
            timeline.Duration <- TimeSpan.FromSeconds(this.DurationNumericUpDown.Value)
            timeline.RepeatBehavior <- 
                if this.RepeatForever then 
                    RepeatBehavior.Forever 
                else 
                    new RepeatBehavior(this.RepeatCountNumericUpDown.Value)
            timeline.FillBehavior <- this.FillBehaviorComboBox.SelectedItem :?> FillBehavior
            timeline.BeginTime <- TimeSpan.FromSeconds(this.BeginTimeNumericUpDown.Value)
            timeline.SpeedRatio <- this.SpeedRatioNumericUpDown.Value
            timeline.AutoReverse <- this.AutoReverseCheckBox.IsChecked.Value

            // Apply easing function to animations that support it
            match timeline with
            | :? DoubleAnimation as doubleAnimation ->
                doubleAnimation.EasingFunction <- _currentEasingFunction
            | :? ColorAnimation as colorAnimation ->
                colorAnimation.EasingFunction <- _currentEasingFunction
            | :? DoubleAnimationUsingKeyFrames as keyframeAnimation ->
                for keyframe in keyframeAnimation.KeyFrames.OfType<EasingDoubleKeyFrame>() do
                    match keyframe.EasingFunction with
                    | :? EasingFunctionBase as easingFunction ->
                        // Update the easing mode but preserve the specific easing function type
                        easingFunction.EasingMode <- _currentEasingMode
                    | _ -> ()
            | _ -> ()

    member private this.Storyboard_Completed(sender : obj, e : EventArgs) =
        // If not set to repeat forever, disable the control buttons
        if not this.RepeatForever then
            this.StopButton.IsEnabled <- false
            this.PauseResumeButton.IsEnabled <- false
            this.PauseResumeButton.Content <- "Pause"

    member private this.StopAnimation() =
        this.StopCurrentAnimation()
        this.ResetElement()
        this.StopButton.IsEnabled <- false
        this.PauseResumeButton.IsEnabled <- false
        this.PauseResumeButton.Content <- "Pause"

    member private this.StopCurrentAnimation() =
        if _currentStoryboard <> null then
            //_currentStoryboard.Completed.RemoveHandler(EventHandler(this.Storyboard_Completed))
            _currentStoryboard.Stop()

    member private this.ResetElement() =
        // Stop any running animations
        this.StopCurrentAnimation()
        _currentStoryboard <- null

        // Reset to original position and properties
        Canvas.SetLeft(this.AnimatedRectangle, _originalRect.X)
        Canvas.SetTop(this.AnimatedRectangle, _originalRect.Y)
        this.AnimatedRectangle.Width <- _originalRect.Width
        this.AnimatedRectangle.Height <- _originalRect.Height
        this.AnimatedRectangle.Opacity <- _originalOpacity
        this.AnimatedRectangle.Background <- new SolidColorBrush(_originalColor)

        this.ResetTransforms()

    member private this.ResetTransforms() =
        match this.AnimatedRectangle.RenderTransform with
        | :? RotateTransform as rotateTransform ->
            rotateTransform.Angle <- 0.0
        | :? ScaleTransform as scaleTransform ->
            scaleTransform.ScaleX <- 1.0
            scaleTransform.ScaleY <- 1.0
        | _ -> ()

    //do
    //    this.InitializeComponent()

    //    this.EasingModeComboBox.ItemsSource <- Enum.GetValues(typeof<EasingMode>)
    //    this.FillBehaviorComboBox.ItemsSource <- Enum.GetValues(typeof<FillBehavior>)

    //    // Store original values
    //    _originalRect <- new Rect(Canvas.GetLeft(this.AnimatedRectangle), Canvas.GetTop(this.AnimatedRectangle), 
    //                              this.AnimatedRectangle.Width, this.AnimatedRectangle.Height)
    //    _originalOpacity <- this.AnimatedRectangle.Opacity
    //    _originalColor <- (this.AnimatedRectangle.Background :?> SolidColorBrush).Color

    //    // Initialize storyboard references from XAML resources
    //    _translationStoryboard <- this.Resources.["TranslationAnimation"] :?> Storyboard
    //    _rotationStoryboard <- this.Resources.["RotationAnimation"] :?> Storyboard
    //    _scaleStoryboard <- this.Resources.["ScaleAnimation"] :?> Storyboard
    //    _opacityStoryboard <- this.Resources.["OpacityAnimation"] :?> Storyboard
    //    _colorStoryboard <- this.Resources.["ColorAnimation"] :?> Storyboard
    //    _keyframeStoryboard <- this.Resources.["KeyframeAnimation"] :?> Storyboard

    //    this.SetupEventHandlers()
