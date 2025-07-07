using Microsoft.Maui.Devices;
using OpenSilver.Samples.Showcase.Search;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("drawing", "inking", "pen input", "sketch", "graphics", "canvas", "png")]
public partial class InkPresenter_Demo : UserControl
{
    private readonly Stack<Stroke> _nextStrokes = new();
    private Stroke _lastStroke;

    public InkPresenter_Demo()
    {
        InitializeComponent();

        var platform = DeviceInfo.Current.Platform;
        if (platform == DevicePlatform.iOS) // downloading base64 data is not supported on iOS yet
        {
            SaveAsPngButton.Visibility = Visibility.Collapsed;
        }
    }

    private void OnIP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        InkPad.CaptureMouse();
        var MyStylusPointCollection = new StylusPointCollection
        {
            e.StylusDevice.GetStylusPoints(InkPad)
        };

        _lastStroke = new Stroke(MyStylusPointCollection);
        _lastStroke.DrawingAttributes.Color = strokeColorPicker.Color;
        _lastStroke.DrawingAttributes.Width = strokeWidthSlider.Value;

        InkPad.Strokes.Add(_lastStroke);
        _nextStrokes.Clear();
        UpdateButtons();
    }

    private void OnIP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        InkPad.ReleaseMouseCapture();
    }

    private void OnIP_MouseMove(object sender, MouseEventArgs e)
    {
        if (_lastStroke != null && InkPad.IsMouseCaptured)
        {
            _lastStroke.StylusPoints.Add(e.StylusDevice.GetStylusPoints(InkPad));
        }
    }

    private void OnClearInkPad(object sender, RoutedEventArgs e)
    {
        _lastStroke = null;
        _nextStrokes.Clear();
        InkPad.Strokes.Clear();
        UpdateButtons();
    }

    private void OnUndoLastStroke(object sender, RoutedEventArgs e)
    {
        var strokes = InkPad.Strokes;
        if (strokes.Count > 0)
        {
            _nextStrokes.Push(strokes[strokes.Count - 1]);
            strokes.RemoveAt(strokes.Count - 1);
        }

        UpdateButtons();
    }

    private void OnRedoLastStroke(object sender, RoutedEventArgs e)
    {
        if (_nextStrokes.Count > 0)
        {
            InkPad.Strokes.Add(_nextStrokes.Pop());
        }

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        clearButton.IsEnabled = InkPad.Strokes.Count > 0;
        undoButton.IsEnabled = InkPad.Strokes.Count > 0;
        redoButton.IsEnabled = _nextStrokes.Count > 0;
    }

    private void OnSaveAsPngButtonClick(object sender, RoutedEventArgs e)
    {
        Interop.ExecuteJavaScriptVoid(
            $$"""
            var link = document.createElement('a');
            link.href = $0.firstChild.toDataURL('image/png');
            link.download = 'canvas_image.png';
            link.click();
            link.remove();
            """,
            Interop.GetDiv(InkPad));
    }
}
