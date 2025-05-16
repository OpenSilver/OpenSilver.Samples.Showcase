
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
using System.Windows.Media.Animation;
using System.Collections.Generic;
using System.Windows.Controls;

namespace OpenSilver.Animations
{
    /// <summary>
    /// A utility class for animating any property using a lambda expression to transform progress values.
    /// </summary>
    public class PropertyAnimator : IDisposable
    {
        // Proxy class for animation
        private class AnimationProxy : DependencyObject
        {
            public static readonly DependencyProperty ProgressProperty =
                DependencyProperty.Register("Progress", typeof(double), typeof(AnimationProxy),
                    new PropertyMetadata(0.0, OnProgressChanged));

            public PropertyAnimator Owner { get; set; }

            private static void OnProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var proxy = d as AnimationProxy;
                if (proxy?.Owner != null && !proxy.Owner._isDisposed)
                {
                    proxy.Owner.UpdatePropertyValue((double)e.NewValue);
                }
            }
        }

        // Static dictionary to track active animators by target/property pairs
        private static readonly Dictionary<DependencyObject, Dictionary<DependencyProperty, PropertyAnimator>>
            _activeAnimators = new Dictionary<DependencyObject, Dictionary<DependencyProperty, PropertyAnimator>>();

        // Lock for thread safety
        private static readonly object _animatorsLock = new object();

        /// <summary>
        /// Stops all animations on the specified object.
        /// </summary>
        public static void StopAnimations(DependencyObject target)
        {
            if (target == null) return;

            lock (_animatorsLock)
            {
                Dictionary<DependencyProperty, PropertyAnimator> targetAnimators;
                if (_activeAnimators.TryGetValue(target, out targetAnimators))
                {
                    // Create a copy to avoid modification during iteration
                    foreach (var animator in new List<PropertyAnimator>(targetAnimators.Values))
                    {
                        animator.Stop();
                    }

                    // Dictionary should now be empty after all stops, but clear it just in case
                    _activeAnimators.Remove(target);
                }
            }
        }

        private readonly DependencyObject _target;
        private readonly DependencyProperty _property;
        private readonly Func<double, object> _progressTransformer;
        private readonly AnimationProxy _proxy;
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
        /// Gets or sets whether to set the final value when the animation completes.
        /// Default is true.
        /// </summary>
        public bool ApplyFinalValue { get; set; } = true;

        /// <summary>
        /// Event raised when the animation completes.
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// Creates a new PropertyAnimator to animate a property.
        /// </summary>
        public PropertyAnimator(DependencyObject target, DependencyProperty property,
                              Func<double, object> progressTransformer)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _property = property ?? throw new ArgumentNullException(nameof(property));
            _progressTransformer = progressTransformer ?? throw new ArgumentNullException(nameof(progressTransformer));

            // Create the animation proxy
            _proxy = new AnimationProxy { Owner = this };

            Duration = TimeSpan.FromMilliseconds(300);

            // Stop any existing animation for this target/property
            StopExistingAnimation();
        }

        private void StopExistingAnimation()
        {
            if (_target == null || _property == null) return;

            lock (_animatorsLock)
            {
                Dictionary<DependencyProperty, PropertyAnimator> targetAnimators;
                if (_activeAnimators.TryGetValue(_target, out targetAnimators))
                {
                    PropertyAnimator existingAnimator;
                    if (targetAnimators.TryGetValue(_property, out existingAnimator))
                    {
                        existingAnimator.Stop();
                    }
                }
            }
        }

        private void RegisterAnimation()
        {
            if (_target == null || _property == null || _isDisposed) return;

            lock (_animatorsLock)
            {
                Dictionary<DependencyProperty, PropertyAnimator> targetAnimators;
                if (!_activeAnimators.TryGetValue(_target, out targetAnimators))
                {
                    targetAnimators = new Dictionary<DependencyProperty, PropertyAnimator>();
                    _activeAnimators[_target] = targetAnimators;
                }

                targetAnimators[_property] = this;
            }
        }

        private void UnregisterAnimation()
        {
            if (_target == null || _property == null) return;

            lock (_animatorsLock)
            {
                Dictionary<DependencyProperty, PropertyAnimator> targetAnimators;
                if (_activeAnimators.TryGetValue(_target, out targetAnimators))
                {
                    targetAnimators.Remove(_property);

                    // If no more properties are being animated for this target, remove the target entry
                    if (targetAnimators.Count == 0)
                    {
                        _activeAnimators.Remove(_target);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the property value based on the current progress.
        /// </summary>
        internal void UpdatePropertyValue(double progress)
        {
            if (_isDisposed || _target == null || _property == null) return;

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

            // Set initial state 
            UpdatePropertyValue(0);

            // Register this animation
            RegisterAnimation();

            // Create a new storyboard for this animation
            _storyboard = new Storyboard();

            // Create a double animation for the Progress property
            var animation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(Duration),
                EasingFunction = EasingFunction
            };

            // Set target to the proxy object
            Storyboard.SetTarget(animation, _proxy);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Progress"));

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
            // Clean up first (doesn't work properly if called after setting the final value)
            CleanupAnimation();

            // Apply final value if enabled
            if (ApplyFinalValue && _target != null && _property != null)
            {
                try
                {
                    // Calculate final value using the transformer with progress=1
                    object finalValue = _progressTransformer(1.0);
                    _target.SetValue(_property, finalValue);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error setting final value: {ex.Message}");
                }
            }

            // Notify completion
            Completed?.Invoke(this, EventArgs.Empty);
        }

        private void CleanupAnimation()
        {
            // Clean up storyboard
            if (_storyboard != null)
            {
                try
                {
                    _storyboard.Completed -= Storyboard_Completed;
                    _storyboard.Stop();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error cleaning up storyboard: {ex.Message}");
                }
                finally
                {
                    _storyboard = null;
                }
            }

            // Unregister from active animators
            UnregisterAnimation();
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

                // Clear reference to owner
                if (_proxy != null)
                {
                    _proxy.Owner = null;
                }

                _isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}