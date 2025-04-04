using OpenSilver.Samples.Showcase.Search;
using System;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("animation")]
public partial class TransitioningContentControl_Demo : ContentControl
{
    public TransitioningContentControl_Demo()
    {
        InitializeComponent();
    }

    private void ChangeContentWithDefaultTransition(object sender, RoutedEventArgs e)
    {
        SetContentWithTransition(TransitioningContentControl.DefaultTransitionState);
    }

    private void ChangeContentWithDownTransition(object sender, RoutedEventArgs e)
    {
        SetContentWithTransition("DownTransition");
    }

    private void ChangeContentWithUpTransition(object sender, RoutedEventArgs e)
    {
        SetContentWithTransition("UpTransition");
    }

    private void SetContentWithTransition(string transition)
    {
        defaultTCC.Transition = transition;
        defaultTCC.Content = DateTime.Now.Ticks;
    }
}
