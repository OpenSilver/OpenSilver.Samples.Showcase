using OpenSilver.Samples.Showcase.Search;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("animation", "effects", "motion", "behavior", "storyboard", "tool", "transitions", "easing")]
public partial class AnimationTool_Demo : UserControl
{
    #region Private Fields

    private Storyboard _currentStoryboard;
    private EasingFunctionBase _currentEasingFunction = new CircleEase();
    private EasingMode _currentEasingMode = EasingMode.EaseOut;

    private bool RepeatForever => RepeatForeverCheckBox.IsChecked == true;

    // Original element properties for reset
    private readonly Rect _originalRect;
    private readonly double _originalOpacity;
    private Color _originalColor;

    // XAML-defined storyboards
    private readonly Storyboard _translationStoryboard;
    private readonly Storyboard _rotationStoryboard;
    private readonly Storyboard _scaleStoryboard;
    private readonly Storyboard _opacityStoryboard;
    private readonly Storyboard _colorStoryboard;
    private readonly Storyboard _keyframeStoryboard;

    #endregion

    public AnimationTool_Demo()
    {
        InitializeComponent();

        EasingModeComboBox.ItemsSource = Enum.GetValues(typeof(EasingMode));
        FillBehaviorComboBox.ItemsSource = Enum.GetValues(typeof(FillBehavior));

        // Store original values
        _originalRect = new Rect(Canvas.GetLeft(AnimatedRectangle), Canvas.GetTop(AnimatedRectangle), AnimatedRectangle.Width, AnimatedRectangle.Height);
        _originalOpacity = AnimatedRectangle.Opacity;
        _originalColor = ((SolidColorBrush)AnimatedRectangle.Background).Color;

        // Initialize storyboard references from XAML resources
        _translationStoryboard = Resources["TranslationAnimation"] as Storyboard;
        _rotationStoryboard = Resources["RotationAnimation"] as Storyboard;
        _scaleStoryboard = Resources["ScaleAnimation"] as Storyboard;
        _opacityStoryboard = Resources["OpacityAnimation"] as Storyboard;
        _colorStoryboard = Resources["ColorAnimation"] as Storyboard;
        _keyframeStoryboard = Resources["KeyframeAnimation"] as Storyboard;

        SetupEventHandlers();
    }

    private void SetupEventHandlers()
    {
        AnimationTypeComboBox.SelectionChanged += AnimationTypeComboBox_SelectionChanged;
        EasingFunctionComboBox.SelectionChanged += EasingFunctionComboBox_SelectionChanged;
        EasingModeComboBox.SelectionChanged += EasingModeComboBox_SelectionChanged;
    }

    #region Event Handlers

    private void AnimationTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ResetElement();
    }

    private void EasingModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SetEasingMode();
    }

    private void EasingFunctionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateEasingFunction();
    }

    private void RepeatForeverCheckBoxStateChanged(object sender, RoutedEventArgs e)
    {
        RepeatCountLabel.IsEnabled = !RepeatForever;
        RepeatCountNumericUpDown.IsEnabled = !RepeatForever;
    }

    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        UpdateEasingFunction();
        PlayAnimation();
        PauseResumeButton.IsEnabled = true;
        StopButton.IsEnabled = true;
        PauseResumeButton.Content = "Pause";
    }

    private void PauseResumeButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentStoryboard == null)
            return;

        if (_currentStoryboard.GetIsPaused())
        {
            // Resume animation
            _currentStoryboard.Resume();
            PauseResumeButton.Content = "Pause";
        }
        else
        {
            // Pause animation
            _currentStoryboard.Pause();
            PauseResumeButton.Content = "Resume";
        }
    }

    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        StopAnimation();
    }

    #endregion

    #region Helper Methods

    private void SetEasingMode()
    {
        if (EasingModeComboBox.SelectedItem is EasingMode mode)
        {
            _currentEasingMode = mode;
            _currentEasingFunction.EasingMode = mode;
        }
    }

    private void UpdateEasingFunction()
    {
        // Hide all property panels
        BackEaseProperties.Visibility = Visibility.Collapsed;
        BounceEaseProperties.Visibility = Visibility.Collapsed;
        ExponentialEaseProperties.Visibility = Visibility.Collapsed;
        PowerEaseProperties.Visibility = Visibility.Collapsed;

        // Update the easing function based on selection but keep the current easing mode
        switch (EasingFunctionComboBox.SelectedIndex)
        {
            case 0: // BackEase
                _currentEasingFunction = new BackEase
                {
                    EasingMode = _currentEasingMode,
                    Amplitude = BackEaseAmplitudeNumericUpDown.Value
                };
                BackEaseProperties.Visibility = Visibility.Visible;
                break;
            case 1: // BounceEase
                _currentEasingFunction = new BounceEase
                {
                    EasingMode = _currentEasingMode,
                    Bounces = (int)BounceEaseBouncesNumericUpDown.Value,
                    Bounciness = BounceEaseBouncinesNumericUpDown.Value
                };
                BounceEaseProperties.Visibility = Visibility.Visible;
                break;
            case 2: // CircleEase
                _currentEasingFunction = new CircleEase { EasingMode = _currentEasingMode };
                break;
            case 3: // CubicEase
                _currentEasingFunction = new CubicEase { EasingMode = _currentEasingMode };
                break;
            case 4: // ExponentialEase
                _currentEasingFunction = new ExponentialEase
                {
                    EasingMode = _currentEasingMode,
                    Exponent = ExponentialEaseExponentNumericUpDown.Value
                };
                ExponentialEaseProperties.Visibility = Visibility.Visible;
                break;
            case 5: // PowerEase
                _currentEasingFunction = new PowerEase
                {
                    EasingMode = _currentEasingMode,
                    Power = PowerEasePowerNumericUpDown.Value
                };
                PowerEaseProperties.Visibility = Visibility.Visible;
                break;
            case 6: // QuadraticEase
                _currentEasingFunction = new QuadraticEase { EasingMode = _currentEasingMode };
                break;
            case 7: // QuarticEase
                _currentEasingFunction = new QuarticEase { EasingMode = _currentEasingMode };
                break;
            case 8: // QuinticEase
                _currentEasingFunction = new QuinticEase { EasingMode = _currentEasingMode };
                break;
            case 9: // SineEase
                _currentEasingFunction = new SineEase { EasingMode = _currentEasingMode };
                break;
        }
    }

    private void PlayAnimation()
    {
        // Stop any currently running animation
        StopCurrentAnimation();

        // Configure and play the selected animation
        switch (AnimationTypeComboBox.SelectedIndex)
        {
            case 0: // Translation (Move)
                _currentStoryboard = _translationStoryboard;
                ConfigureAnimation(_currentStoryboard);
                break;
            case 1: // Rotation
                if (AnimatedRectangle.RenderTransform is not RotateTransform)
                {
                    AnimatedRectangle.RenderTransform = new RotateTransform();
                }
                _currentStoryboard = _rotationStoryboard;
                ConfigureAnimation(_currentStoryboard);
                break;
            case 2: // Scale
                if (AnimatedRectangle.RenderTransform is not ScaleTransform)
                {
                    AnimatedRectangle.RenderTransform = new ScaleTransform();
                }
                _currentStoryboard = _scaleStoryboard;
                ConfigureAnimation(_currentStoryboard);
                break;
            case 3: // Opacity
                _currentStoryboard = _opacityStoryboard;
                ConfigureAnimation(_currentStoryboard);
                break;
            case 4: // Color Change
                _currentStoryboard = _colorStoryboard;
                ConfigureAnimation(_currentStoryboard);
                break;
            case 5: // Keyframe Animation
                if (AnimatedRectangle.RenderTransform is not RotateTransform)
                {
                    AnimatedRectangle.RenderTransform = new RotateTransform();
                }
                _currentStoryboard = _keyframeStoryboard;
                ConfigureAnimation(_currentStoryboard);
                break;
        }

        // Subscribe to the Completed event 
        _currentStoryboard.Completed += Storyboard_Completed;

        // Start the animation
        _currentStoryboard.Begin();
    }

    private void ConfigureAnimation(Storyboard storyboard)
    {
        // Apply common settings to all animations in the storyboard
        foreach (Timeline timeline in storyboard.Children)
        {
            timeline.Duration = TimeSpan.FromSeconds(DurationNumericUpDown.Value);
            timeline.RepeatBehavior = RepeatForever ? RepeatBehavior.Forever : new RepeatBehavior(RepeatCountNumericUpDown.Value);
            timeline.FillBehavior = (FillBehavior)FillBehaviorComboBox.SelectedItem;
            timeline.BeginTime = TimeSpan.FromSeconds(BeginTimeNumericUpDown.Value);
            // todo: latest OpenSilver supports SpeedRatio and AutoReverse

            // Apply easing function to animations that support it
            if (timeline is DoubleAnimation doubleAnimation)
            {
                doubleAnimation.EasingFunction = _currentEasingFunction;
            }
            else if (timeline is ColorAnimation colorAnimation)
            {
                colorAnimation.EasingFunction = _currentEasingFunction;
            }
            else if (timeline is DoubleAnimationUsingKeyFrames keyframeAnimation)
            {
                foreach (var keyframe in keyframeAnimation.KeyFrames.OfType<EasingDoubleKeyFrame>())
                {
                    if (keyframe.EasingFunction is EasingFunctionBase easingFunction)
                    {
                        // Update the easing mode but preserve the specific easing function type
                        easingFunction.EasingMode = _currentEasingMode;
                    }
                }
            }
        }
    }

    private void Storyboard_Completed(object sender, EventArgs e)
    {
        // If not set to repeat forever, disable the control buttons
        if (!RepeatForever)
        {
            StopButton.IsEnabled = false;
            PauseResumeButton.IsEnabled = false;
            PauseResumeButton.Content = "Pause";
        }
    }

    private void StopAnimation()
    {
        StopCurrentAnimation();
        ResetElement();
        StopButton.IsEnabled = false;
        PauseResumeButton.IsEnabled = false;
        PauseResumeButton.Content = "Pause";
    }

    private void StopCurrentAnimation()
    {
        if (_currentStoryboard != null)
        {
            _currentStoryboard.Completed -= Storyboard_Completed;
            _currentStoryboard.Stop();
        }
    }

    private void ResetElement()
    {
        // Stop any running animations
        StopCurrentAnimation();
        _currentStoryboard = null;

        // Reset to original position and properties
        Canvas.SetLeft(AnimatedRectangle, _originalRect.X);
        Canvas.SetTop(AnimatedRectangle, _originalRect.Y);
        AnimatedRectangle.Width = _originalRect.Width;
        AnimatedRectangle.Height = _originalRect.Height;
        AnimatedRectangle.Opacity = _originalOpacity;
        AnimatedRectangle.Background = new SolidColorBrush(_originalColor);

        ResetTransforms();
    }

    private void ResetTransforms()
    {
        if (AnimatedRectangle.RenderTransform is RotateTransform rotateTransform)
        {
            rotateTransform.Angle = 0;
        }
        else if (AnimatedRectangle.RenderTransform is ScaleTransform scaleTransform)
        {
            scaleTransform.ScaleX = 1;
            scaleTransform.ScaleY = 1;
        }
    }

    #endregion
}
