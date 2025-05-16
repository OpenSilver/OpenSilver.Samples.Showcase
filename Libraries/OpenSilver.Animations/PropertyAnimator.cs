
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
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
using System.Runtime.CompilerServices;

namespace OpenSilver.Animationns
{
    /// <summary>
    /// A utility class for animating any property using a lambda expression to transform progress values.
    /// </summary>
    public class PropertyAnimator : IDisposable
    {
        // Composite key class for target+property pairs
        private class AnimationKey
        {
            public WeakReference<DependencyObject> Target { get; }
            public DependencyProperty Property { get; }

            public AnimationKey(DependencyObject target, DependencyProperty property)
            {
                Target = new WeakReference<DependencyObject>(target);
                Property = property;
            }

            public override bool Equals(object obj)
            {
                var other = obj as AnimationKey;
                if (other == null) return false;

                DependencyObject thisTarget, otherTarget;
                if (!Target.TryGetTarget(out thisTarget) || !other.Target.TryGetTarget(out otherTarget))
                    return false;

                return ReferenceEquals(thisTarget, otherTarget) && Property == other.Property;
            }

            public override int GetHashCode()
            {
                DependencyObject target;
                return (Target.TryGetTarget(out target) ? target.GetHashCode() : 0) ^ Property.GetHashCode();
            }
        }

        // Attached property to use as animation proxy
        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.RegisterAttached(
                "Progress",
                typeof(double),
                typeof(PropertyAnimator),
                new PropertyMetadata(0.0, OnProgressChanged));

        // Attached property to track animated properties per object
        public static readonly DependencyProperty AnimatedPropertiesProperty =
            DependencyProperty.RegisterAttached(
                "AnimatedProperties",
                typeof(List<DependencyProperty>),
                typeof(PropertyAnimator),
                new PropertyMetadata(null));

        // Dictionary to track animators by target/property
        private static readonly ConditionalWeakTable<object, PropertyAnimator> _activeAnimators =
            new ConditionalWeakTable<object, PropertyAnimator>();

        private static void OnProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Find all properties being animated for this target
            var propertyList = d.GetValue(AnimatedPropertiesProperty) as List<DependencyProperty>;
            if (propertyList != null)
            {
                foreach (var prop in new List<DependencyProperty>(propertyList)) // Create a copy to avoid modification during iteration
                {
                    var key = new AnimationKey(d, prop);
                    PropertyAnimator animator;
                    if (_activeAnimators.TryGetValue(key, out animator))
                    {
                        animator.UpdatePropertyValue((double)e.NewValue);
                    }
                }
            }
        }

        // Gets the progress attached property value
        public static double GetProgress(DependencyObject obj)
        {
            return (double)obj.GetValue(ProgressProperty);
        }

        // Sets the progress attached property value
        public static void SetProgress(DependencyObject obj, double value)
        {
            obj.SetValue(ProgressProperty, value);
        }

        /// <summary>
        /// Stops all animations on the specified object.
        /// </summary>
        public static void StopAnimations(DependencyObject target)
        {
            var propertyList = target.GetValue(AnimatedPropertiesProperty) as List<DependencyProperty>;
            if (propertyList != null)
            {
                foreach (var prop in new List<DependencyProperty>(propertyList)) // Create a copy to avoid modification during iteration
                {
                    var key = new AnimationKey(target, prop);
                    PropertyAnimator animator;
                    if (_activeAnimators.TryGetValue(key, out animator))
                    {
                        animator.Stop();
                    }
                }

                // Clear the list
                propertyList.Clear();
                target.ClearValue(AnimatedPropertiesProperty);
            }
        }

        private readonly DependencyObject _target;
        private readonly DependencyProperty _property;
        private readonly Func<double, object> _progressTransformer;
        private readonly object _finalValue;
        private Storyboard _storyboard;
        private bool _isDisposed;

        /// <summary>
        /// Gets or sets the duration of the animation.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the easing function for the animation.
        /// </summary>
        public IEasingFunction EasingFunction { get; set; }

        /// <summary>
        /// Gets or sets the fill behavior for when the animation ends.
        /// </summary>
        public FillBehavior FillBehavior { get; set; }

        /// <summary>
        /// Event raised when the animation completes.
        /// </summary>
        public event EventHandler Completed;


        /// <summary>
        /// Creates a new PropertyAnimator to animate a property.
        /// </summary>
        public PropertyAnimator(DependencyObject target, DependencyProperty property,
                               Func<double, object> progressTransformer, object finalValue = null)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _property = property ?? throw new ArgumentNullException(nameof(property));
            _progressTransformer = progressTransformer ?? throw new ArgumentNullException(nameof(progressTransformer));
            _finalValue = finalValue;

            Duration = TimeSpan.FromMilliseconds(250);
            FillBehavior = this.FillBehavior;

            // Register this animator
            RegisterAnimation();
        }

        private void RegisterAnimation()
        {
            var key = new AnimationKey(_target, _property);

            // Look for existing animation and stop it
            PropertyAnimator existingAnimator;
            if (_activeAnimators.TryGetValue(key, out existingAnimator))
            {
                existingAnimator.Stop();
            }

            // Track which properties are being animated for this target
            var propertyList = _target.GetValue(AnimatedPropertiesProperty) as List<DependencyProperty>;
            if (propertyList == null)
            {
                propertyList = new List<DependencyProperty>();
                _target.SetValue(AnimatedPropertiesProperty, propertyList);
            }

            if (!propertyList.Contains(_property))
            {
                propertyList.Add(_property);
            }

            // Add this animator
            _activeAnimators.Remove(key);
            _activeAnimators.Add(key, this);
        }

        /// <summary>
        /// Updates the property value based on the current progress.
        /// </summary>
        internal void UpdatePropertyValue(double progress)
        {
            if (_isDisposed) return;

            try
            {
                object value = _progressTransformer(progress);
                _target.SetValue(_property, value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating property: {ex.Message}");
            }
        }

        /// <summary>
        /// Starts the animation.
        /// </summary>
        public void Begin()
        {
            if (_isDisposed)
                throw new ObjectDisposedException("PropertyAnimator");

            // Stop any existing animation on this property
            Stop();

            // Set initial state 
            UpdatePropertyValue(0);

            // Create a new storyboard for this animation
            _storyboard = new Storyboard();

            // Create a double animation for the Progress property
            var animation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(Duration),
                EasingFunction = EasingFunction,
                FillBehavior = this.FillBehavior
            };

            // Set the target
            Storyboard.SetTarget(animation, _target);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(PropertyAnimator.Progress)"));

            _storyboard.Children.Add(animation);

            // Hook up completion event
            _storyboard.Completed += Storyboard_Completed;

            // Start the animation
            try
            {
                _storyboard.Begin();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error starting animation: {ex.Message}");
                CleanupAnimation();
            }
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            // Apply final value if needed
            if (_finalValue != null)
            {
                _target.SetValue(_property, _finalValue);
            }

            // Clean up
            CleanupAnimation();

            // Notify completion
            Completed?.Invoke(this, EventArgs.Empty);
        }

        private void CleanupAnimation()
        {
            if (_storyboard != null)
            {
                _storyboard.Completed -= Storyboard_Completed;
                _storyboard.Stop();
                _storyboard = null;
            }

            // Remove from active animators and property list
            var key = new AnimationKey(_target, _property);
            _activeAnimators.Remove(key);

            var propertyList = _target.GetValue(AnimatedPropertiesProperty) as List<DependencyProperty>;
            if (propertyList != null)
            {
                propertyList.Remove(_property);

                // If no more properties are being animated, clear the list entirely
                if (propertyList.Count == 0)
                {
                    _target.ClearValue(AnimatedPropertiesProperty);
                }
            }
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void Stop()
        {
            CleanupAnimation();
        }

        /// <summary>
        /// Releases resources used by the animator.
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                Stop();

                // Clear event handlers
                Completed = null;

                _isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }

    /// <summary>
    /// Extension methods to simplify animation creation.
    /// </summary>
    public static class AnimationExtensions
    {
        /// <summary>
        /// Animates a property using a progress transformer function.
        /// </summary>
        public static PropertyAnimator Animate<TValue>(
            this DependencyObject target,
            DependencyProperty property,
            Func<double, TValue> progressTransformer,
            TimeSpan duration,
            TValue finalValue = default(TValue))
        {
            var animator = new PropertyAnimator(
                target,
                property,
                progress => (object)progressTransformer(progress),
                finalValue);

            animator.Duration = duration;
            animator.Begin();
            return animator;
        }

        /// <summary>
        /// Animates a GridLength property.
        /// </summary>
        public static PropertyAnimator AnimateGridLength(
            this DependencyObject target,
            DependencyProperty property,
            double fromValue,
            double toValue,
            GridUnitType unitType,
            TimeSpan duration)
        {
            return target.Animate<GridLength>(
                property,
                progress => new GridLength(fromValue + (progress * (toValue - fromValue)), unitType),
                duration,
                new GridLength(toValue, unitType));
        }

        /// <summary>
        /// Stops all animations on the specified target.
        /// </summary>
        public static void StopAllAnimations(this DependencyObject target)
        {
            PropertyAnimator.StopAnimations(target);
        }
    }
}
