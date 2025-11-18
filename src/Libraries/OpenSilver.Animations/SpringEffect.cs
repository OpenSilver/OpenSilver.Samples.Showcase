
/*===================================================================================
* 
*   Copyright (c) Userware/OpenSilver.net
*      
*   This file is part of OpenSilver (https://opensilver.net), which is
*   licensed under the MIT license: https://opensource.org/licenses/MIT
*   
*   IMPORTANT: Make sure to preserve this copyright notice on all copies of this code.
*  
\*====================================================================================*/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace OpenSilver.Animations
{
    /// <summary>
    /// Provides a spring-like animation effect for UIElements when the mouse enters them.
    /// The animation is based on the velocity and direction of the mouse entry.
    /// </summary>
    public static class SpringEffect
    {
        #region Attached Properties

        /// <summary>
        /// Dependency property for enabling or disabling the spring effect on an element.
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(SpringEffect),
                new PropertyMetadata(false, OnIsEnabledChanged));

        /// <summary>
        /// Dependency property that controls the maximum distance in pixels that the element can move.
        /// Default value is 3.0 pixels.
        /// </summary>
        public static readonly DependencyProperty MaxDistanceProperty =
            DependencyProperty.RegisterAttached("MaxDistance", typeof(double), typeof(SpringEffect),
                new PropertyMetadata(3.0));

        /// <summary>
        /// Dependency property that controls the total duration of the animation in milliseconds.
        /// Default value is 400 milliseconds.
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.RegisterAttached("AnimationDuration", typeof(double), typeof(SpringEffect),
                new PropertyMetadata(400.0));

        /// <summary>
        /// Dependency property that controls how much rotation is applied during the animation.
        /// Higher values create more pronounced rotation effects.
        /// Default value is 2.0 degrees.
        /// </summary>
        public static readonly DependencyProperty RotationIntensityProperty =
            DependencyProperty.RegisterAttached("RotationIntensity", typeof(double), typeof(SpringEffect),
                new PropertyMetadata(2.0));

        /// <summary>
        /// Dependency property that controls how much scaling is applied during the animation.
        /// Set to 0 for no scaling effect. Higher values create more noticeable scaling.
        /// Default value is 0.0 (no scaling).
        /// </summary>
        public static readonly DependencyProperty ScaleIntensityProperty =
            DependencyProperty.RegisterAttached("ScaleIntensity", typeof(double), typeof(SpringEffect),
                new PropertyMetadata(0.0));

        /// <summary>
        /// Dependency property that controls the "bounciness" of the spring return animation.
        /// Higher values create more pronounced spring-back effects.
        /// Default value is 0.8.
        /// </summary>
        public static readonly DependencyProperty SpringAmplitudeProperty =
            DependencyProperty.RegisterAttached("SpringAmplitude", typeof(double), typeof(SpringEffect),
                new PropertyMetadata(0.8));

        /// <summary>
        /// Dependency property that controls how mouse velocity is scaled to affect the animation.
        /// Lower values create more subtle movements, higher values create more dramatic movements.
        /// Default value is 0.01.
        /// </summary>
        public static readonly DependencyProperty VelocityScaleProperty =
            DependencyProperty.RegisterAttached("VelocityScale", typeof(double), typeof(SpringEffect),
                new PropertyMetadata(0.01));

        #endregion

        #region Property Accessor Methods

        /// <summary>
        /// Gets whether the spring effect is enabled for the specified element.
        /// </summary>
        /// <param name="obj">The dependency object to get the value from.</param>
        /// <returns>True if the spring effect is enabled; otherwise, false.</returns>
        public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);

        /// <summary>
        /// Enables or disables the spring effect for the specified element.
        /// </summary>
        /// <param name="obj">The dependency object to set the value on.</param>
        /// <param name="value">True to enable the spring effect; otherwise, false.</param>
        public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);

        /// <summary>
        /// Gets the maximum distance in pixels that the element can move during the spring animation.
        /// </summary>
        /// <param name="obj">The dependency object to get the value from.</param>
        /// <returns>The maximum distance in pixels.</returns>
        public static double GetMaxDistance(DependencyObject obj) => (double)obj.GetValue(MaxDistanceProperty);

        /// <summary>
        /// Sets the maximum distance in pixels that the element can move during the spring animation.
        /// Lower values (1-5) create subtle effects, while higher values create more dramatic movements.
        /// </summary>
        /// <param name="obj">The dependency object to set the value on.</param>
        /// <param name="value">The maximum distance in pixels.</param>
        public static void SetMaxDistance(DependencyObject obj, double value) => obj.SetValue(MaxDistanceProperty, value);

        /// <summary>
        /// Gets the duration of the spring animation in milliseconds.
        /// </summary>
        /// <param name="obj">The dependency object to get the value from.</param>
        /// <returns>The animation duration in milliseconds.</returns>
        public static double GetAnimationDuration(DependencyObject obj) => (double)obj.GetValue(AnimationDurationProperty);

        /// <summary>
        /// Sets the duration of the spring animation in milliseconds.
        /// Typical values range from 200ms (fast) to 600ms (slow).
        /// </summary>
        /// <param name="obj">The dependency object to set the value on.</param>
        /// <param name="value">The animation duration in milliseconds.</param>
        public static void SetAnimationDuration(DependencyObject obj, double value) => obj.SetValue(AnimationDurationProperty, value);

        /// <summary>
        /// Gets the rotation intensity factor for the spring animation.
        /// </summary>
        /// <param name="obj">The dependency object to get the value from.</param>
        /// <returns>The rotation intensity factor.</returns>
        public static double GetRotationIntensity(DependencyObject obj) => (double)obj.GetValue(RotationIntensityProperty);

        /// <summary>
        /// Sets the rotation intensity factor for the spring animation.
        /// Values between 0-3 are recommended. Set to 0 for no rotation effect.
        /// Higher values create more dramatic rotation effects.
        /// </summary>
        /// <param name="obj">The dependency object to set the value on.</param>
        /// <param name="value">The rotation intensity factor.</param>
        public static void SetRotationIntensity(DependencyObject obj, double value) => obj.SetValue(RotationIntensityProperty, value);

        /// <summary>
        /// Gets the scale intensity factor for the spring animation.
        /// </summary>
        /// <param name="obj">The dependency object to get the value from.</param>
        /// <returns>The scale intensity factor.</returns>
        public static double GetScaleIntensity(DependencyObject obj) => (double)obj.GetValue(ScaleIntensityProperty);

        /// <summary>
        /// Sets the scale intensity factor for the spring animation.
        /// Values between 0-0.1 are recommended. Set to 0 for no scaling effect.
        /// A value of 0.05 means the element can scale up to 5% larger during animation.
        /// </summary>
        /// <param name="obj">The dependency object to set the value on.</param>
        /// <param name="value">The scale intensity factor.</param>
        public static void SetScaleIntensity(DependencyObject obj, double value) => obj.SetValue(ScaleIntensityProperty, value);

        /// <summary>
        /// Gets the spring amplitude factor that controls the "bounciness" of the return animation.
        /// </summary>
        /// <param name="obj">The dependency object to get the value from.</param>
        /// <returns>The spring amplitude factor.</returns>
        public static double GetSpringAmplitude(DependencyObject obj) => (double)obj.GetValue(SpringAmplitudeProperty);

        /// <summary>
        /// Sets the spring amplitude factor that controls the "bounciness" of the return animation.
        /// Values between 0.2-1.0 are recommended. Higher values create more pronounced spring-back effects.
        /// </summary>
        /// <param name="obj">The dependency object to set the value on.</param>
        /// <param name="value">The spring amplitude factor.</param>
        public static void SetSpringAmplitude(DependencyObject obj, double value) => obj.SetValue(SpringAmplitudeProperty, value);

        /// <summary>
        /// Gets the velocity scale factor that controls how mouse movement speed affects the animation.
        /// </summary>
        /// <param name="obj">The dependency object to get the value from.</param>
        /// <returns>The velocity scale factor.</returns>
        public static double GetVelocityScale(DependencyObject obj) => (double)obj.GetValue(VelocityScaleProperty);

        /// <summary>
        /// Sets the velocity scale factor that controls how mouse movement speed affects the animation.
        /// Lower values (0.1-0.3) create more subtle movements, while higher values (0.5-1.0) create
        /// more dramatic movements in response to fast mouse entry.
        /// </summary>
        /// <param name="obj">The dependency object to set the value on.</param>
        /// <param name="value">The velocity scale factor.</param>
        public static void SetVelocityScale(DependencyObject obj, double value) => obj.SetValue(VelocityScaleProperty, value);

        #endregion

        #region Private Implementation

        private static readonly DependencyProperty StateProperty =
            DependencyProperty.RegisterAttached("State", typeof(SpringState), typeof(SpringEffect),
                new PropertyMetadata(null));

        private class SpringState
        {
            // Mouse velocity tracking
            public Point? FirstMousePosition { get; set; }
            public DateTime FirstMouseTime { get; set; }

            // Animation state
            public bool IsAnimating { get; set; }
            public DateTime AnimationStartTime { get; set; }
            public CompositeTransform OriginalTransform { get; set; }

            // Element properties
            public double ElementWidth { get; set; }
            public double ElementHeight { get; set; }
            public SizeChangedEventHandler SizeChangedHandler { get; set; }

            // Entry tracking
            public bool IsTrackingEntry { get; set; }
            public DateTime EntryTime { get; set; }
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is UIElement element))
                return;

            if ((bool)e.NewValue)
                AttachBehavior(element);
            else
                DetachBehavior(element);
        }

        private static void AttachBehavior(UIElement element)
        {
            var state = new SpringState();

            // Ensure we have a CompositeTransform
            var transform = element.RenderTransform as CompositeTransform;
            if (transform == null)
            {
                transform = new CompositeTransform();
                element.RenderTransform = transform;
            }

            // Store original transform values
            state.OriginalTransform = new CompositeTransform
            {
                TranslateX = transform.TranslateX,
                TranslateY = transform.TranslateY,
                ScaleX = transform.ScaleX == 0 ? 1 : transform.ScaleX,
                ScaleY = transform.ScaleY == 0 ? 1 : transform.ScaleY,
                Rotation = transform.Rotation
            };

            // Store the state
            element.SetValue(StateProperty, state);

            // Handle element size changes
            InitializeElementSize(element, state);

            // Attach event handlers
            element.MouseEnter += Element_MouseEnter;
            element.MouseMove += Element_MouseMove;
            element.MouseLeave += Element_MouseLeave;
        }

        private static void InitializeElementSize(UIElement element, SpringState state)
        {
            if (!(element is FrameworkElement frameworkElement))
                return;

            // If already loaded, update size info
            if (frameworkElement.ActualWidth > 0 && frameworkElement.ActualHeight > 0)
            {
                state.ElementWidth = frameworkElement.ActualWidth;
                state.ElementHeight = frameworkElement.ActualHeight;
            }

            // Add handler for size changes
            SizeChangedEventHandler sizeChangedHandler = (s, e) =>
            {
                state.ElementWidth = e.NewSize.Width;
                state.ElementHeight = e.NewSize.Height;
            };

            frameworkElement.SizeChanged += sizeChangedHandler;
            state.SizeChangedHandler = sizeChangedHandler;
        }

        private static void DetachBehavior(UIElement element)
        {
            // Detach event handlers
            element.MouseEnter -= Element_MouseEnter;
            element.MouseMove -= Element_MouseMove;
            element.MouseLeave -= Element_MouseLeave;

            // Get state
            var state = element.GetValue(StateProperty) as SpringState;
            if (state == null)
                return;

            // Detach size changed handler if applicable
            if (state.SizeChangedHandler != null && element is FrameworkElement frameworkElement)
            {
                frameworkElement.SizeChanged -= state.SizeChangedHandler;
            }

            // Reset transform to original values
            if (element.RenderTransform is CompositeTransform transform)
            {
                ResetTransform(transform, state.OriginalTransform);
            }

            // Remove state
            element.ClearValue(StateProperty);
        }

        private static void ResetTransform(CompositeTransform transform, CompositeTransform originalValues)
        {
            transform.TranslateX = originalValues.TranslateX;
            transform.TranslateY = originalValues.TranslateY;
            transform.ScaleX = originalValues.ScaleX;
            transform.ScaleY = originalValues.ScaleY;
            transform.Rotation = originalValues.Rotation;
        }

        private static void Element_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!(sender is UIElement element))
                return;

            var state = element.GetValue(StateProperty) as SpringState;
            if (state == null)
                return;

            // If we're already animating, ignore this event to prevent retriggering
            // during animation when element moves under the mouse
            if (state.IsAnimating)
            {
                double animDuration = GetAnimationDuration(element);
                if ((DateTime.Now - state.AnimationStartTime).TotalMilliseconds < animDuration)
                {
                    return;
                }
            }

            // Start tracking entry behavior
            state.IsTrackingEntry = true;
            state.EntryTime = DateTime.Now;
            state.FirstMousePosition = null;
        }

        private static void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is UIElement element))
                return;

            var state = element.GetValue(StateProperty) as SpringState;
            if (state == null)
                return;

            // If we're animating or not tracking entry, ignore
            if (state.IsAnimating || !state.IsTrackingEntry)
                return;

            // If too much time has passed since entry, stop tracking entry behavior
            const double entryTimeoutMs = 150;
            if ((DateTime.Now - state.EntryTime).TotalMilliseconds > entryTimeoutMs)
            {
                state.IsTrackingEntry = false;
                return;
            }

            // Get mouse position
            Point mousePos = e.GetPosition(element);

            if (state.FirstMousePosition == null)
            {
                // First mouse move after entry
                state.FirstMousePosition = mousePos;
                state.FirstMouseTime = DateTime.Now;
            }
            else
            {
                // Second mouse move - calculate velocity and start animation
                double timeDiffMs = (DateTime.Now - state.FirstMouseTime).TotalMilliseconds;
                const double maxTimeBetweenMoveMs = 200;

                // If too much time between mouse movements, ignore
                if (timeDiffMs > maxTimeBetweenMoveMs)
                {
                    state.FirstMousePosition = mousePos;
                    state.FirstMouseTime = DateTime.Now;
                    return;
                }

                // Velocity calculation and animation
                TriggerSpringAnimation(element, state, mousePos, timeDiffMs);

                // Stop tracking entry behavior after animation starts
                state.IsTrackingEntry = false;
            }
        }

        private static void Element_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!(sender is UIElement element))
                return;

            var state = element.GetValue(StateProperty) as SpringState;
            if (state == null)
                return;

            // Stop tracking entry behavior
            state.IsTrackingEntry = false;
            state.FirstMousePosition = null;
        }

        private static void TriggerSpringAnimation(UIElement element, SpringState state, Point currentPos, double elapsedMs)
        {
            if (state.FirstMousePosition == null)
                return;

            // Calculate mouse movement
            Point firstPos = state.FirstMousePosition.Value;
            double moveX = currentPos.X - firstPos.X;
            double moveY = currentPos.Y - firstPos.Y;

            // Calculate velocity (pixels per millisecond)
            double velX = moveX / elapsedMs;
            double velY = moveY / elapsedMs;

            // Calculate speed and direction
            double speed = Math.Sqrt(velX * velX + velY * velY);

            // Ignore very small movements
            const double minSpeedThreshold = 0.05;
            if (speed < minSpeedThreshold)
                return;

            // Normalize direction vector
            double dirX = velX / speed;
            double dirY = velY / speed;

            // Get animation parameters from attached properties
            AnimationParameters parameters = GetAnimationParameters(element);

            // Apply velocity scaling and capping
            double cappedSpeed = Math.Min(speed * parameters.VelocityScale * 100, 1.0);

            // Calculate displacement for animation
            double displaceX = dirX * cappedSpeed * parameters.MaxDistance;
            double displaceY = dirY * cappedSpeed * parameters.MaxDistance;

            // Calculate rotation based on entry point relative to center
            double rotation = CalculateRotation(state, dirX, dirY, cappedSpeed, parameters.RotationIntensity);

            // Calculate scale 
            double scaleFactor = 1 + (parameters.ScaleIntensity * cappedSpeed);

            // Mark as animating and record start time
            state.IsAnimating = true;
            state.AnimationStartTime = DateTime.Now;

            // Start two-phase animation
            StartTwoPhaseAnimation(element, state, displaceX, displaceY, rotation, scaleFactor, parameters);
        }

        private struct AnimationParameters
        {
            public double MaxDistance;
            public double RotationIntensity;
            public double ScaleIntensity;
            public double Duration;
            public double SpringAmplitude;
            public double VelocityScale;
        }

        private static AnimationParameters GetAnimationParameters(UIElement element)
        {
            return new AnimationParameters
            {
                MaxDistance = GetMaxDistance(element),
                RotationIntensity = GetRotationIntensity(element),
                ScaleIntensity = GetScaleIntensity(element),
                Duration = GetAnimationDuration(element),
                SpringAmplitude = GetSpringAmplitude(element),
                VelocityScale = GetVelocityScale(element)
            };
        }

        private static double CalculateRotation(SpringState state, double dirX, double dirY,
            double speed, double rotationIntensity)
        {
            // Calculate rotation based on entry point and direction
            double centerX = state.ElementWidth / 2;
            double centerY = state.ElementHeight / 2;

            if (state.FirstMousePosition == null)
                return 0;

            // Vector from center to entry point
            Point entryPoint = state.FirstMousePosition.Value;
            double entryPosX = entryPoint.X - centerX;
            double entryPosY = entryPoint.Y - centerY;
            double entryDist = Math.Sqrt(entryPosX * entryPosX + entryPosY * entryPosY);

            // Normalize entry vector if not at center
            if (entryDist > 0.001)
            {
                entryPosX /= entryDist;
                entryPosY /= entryDist;
            }
            else
            {
                entryPosX = 0;
                entryPosY = 0;
            }

            // Cross product to determine rotation (like a lever effect)
            double cross = entryPosX * dirY - entryPosY * dirX;
            return cross * rotationIntensity * speed;
        }

        private static void StartTwoPhaseAnimation(UIElement element, SpringState state,
            double displaceX, double displaceY, double rotation, double scaleFactor,
            AnimationParameters parameters)
        {
            var transform = element.RenderTransform as CompositeTransform;
            if (transform == null)
                return;

            // Phase 1: Initial displacement (30% of duration)
            var phase1Duration = TimeSpan.FromMilliseconds(parameters.Duration * 0.3);
            var storyboard = new Storyboard();

            // Create push-out animations
            storyboard.Children.Add(CreateAnimation(transform, "TranslateX",
                state.OriginalTransform.TranslateX,
                state.OriginalTransform.TranslateX + displaceX,
                phase1Duration, new QuadraticEase { EasingMode = EasingMode.EaseOut }));

            storyboard.Children.Add(CreateAnimation(transform, "TranslateY",
                state.OriginalTransform.TranslateY,
                state.OriginalTransform.TranslateY + displaceY,
                phase1Duration, new QuadraticEase { EasingMode = EasingMode.EaseOut }));

            storyboard.Children.Add(CreateAnimation(transform, "Rotation",
                state.OriginalTransform.Rotation,
                state.OriginalTransform.Rotation + rotation,
                phase1Duration, new QuadraticEase { EasingMode = EasingMode.EaseOut }));

            if (parameters.ScaleIntensity > 0)
            {
                storyboard.Children.Add(CreateAnimation(transform, "ScaleX",
                    state.OriginalTransform.ScaleX,
                    state.OriginalTransform.ScaleX * scaleFactor,
                    phase1Duration, new QuadraticEase { EasingMode = EasingMode.EaseOut }));

                storyboard.Children.Add(CreateAnimation(transform, "ScaleY",
                    state.OriginalTransform.ScaleY,
                    state.OriginalTransform.ScaleY * scaleFactor,
                    phase1Duration, new QuadraticEase { EasingMode = EasingMode.EaseOut }));
            }

            // Setup the return animation (Phase 2)
            EventHandler firstPhaseCompletedHandler = null;
            firstPhaseCompletedHandler = (s, e) =>
            {
                storyboard.Completed -= firstPhaseCompletedHandler;
                StartReturnPhase(element, state, parameters);
            };

            storyboard.Completed += firstPhaseCompletedHandler;
            storyboard.Begin();
        }

        private static void StartReturnPhase(UIElement element, SpringState state, AnimationParameters parameters)
        {
            var transform = element.RenderTransform as CompositeTransform;
            if (transform == null)
                return;

            // Phase 2: Return with spring (70% of duration)
            var phase2Duration = TimeSpan.FromMilliseconds(parameters.Duration * 0.7);
            var returnStoryboard = new Storyboard();
            var springEase = new BackEase { Amplitude = parameters.SpringAmplitude, EasingMode = EasingMode.EaseOut };

            // Create return animations
            returnStoryboard.Children.Add(CreateAnimation(transform, "TranslateX",
                transform.TranslateX, state.OriginalTransform.TranslateX,
                phase2Duration, springEase));

            returnStoryboard.Children.Add(CreateAnimation(transform, "TranslateY",
                transform.TranslateY, state.OriginalTransform.TranslateY,
                phase2Duration, springEase));

            returnStoryboard.Children.Add(CreateAnimation(transform, "Rotation",
                transform.Rotation, state.OriginalTransform.Rotation,
                phase2Duration, springEase));

            if (parameters.ScaleIntensity > 0)
            {
                returnStoryboard.Children.Add(CreateAnimation(transform, "ScaleX",
                    transform.ScaleX, state.OriginalTransform.ScaleX,
                    phase2Duration, springEase));

                returnStoryboard.Children.Add(CreateAnimation(transform, "ScaleY",
                    transform.ScaleY, state.OriginalTransform.ScaleY,
                    phase2Duration, springEase));
            }

            // Setup completion handler
            EventHandler returnCompletedHandler = null;
            returnCompletedHandler = (rs, re) =>
            {
                returnStoryboard.Completed -= returnCompletedHandler;
                state.IsAnimating = false;
            };

            returnStoryboard.Completed += returnCompletedHandler;
            returnStoryboard.Begin();
        }

        private static Timeline CreateAnimation(DependencyObject target, string propertyPath,
            double from, double to, TimeSpan duration, IEasingFunction easingFunction)
        {
            var animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = easingFunction
            };

            Storyboard.SetTarget(animation, target);
            Storyboard.SetTargetProperty(animation, new PropertyPath(propertyPath));

            return animation;
        }

        #endregion
    }
}