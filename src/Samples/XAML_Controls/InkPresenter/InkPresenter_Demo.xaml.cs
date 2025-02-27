using OpenSilver.Samples.Showcase.Search;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("drawing", "inking", "pen input", "sketch", "graphics")]
    public partial class InkPresenter_Demo : UserControl, INotifyPropertyChanged
    {

        private Stroke LastStroke;
        private Stack<Stroke> nextStrokes = new Stack<Stroke>();
        public event PropertyChangedEventHandler PropertyChanged;

        public InkPresenter_Demo()
        {
            InitializeComponent();
        }


        private bool _canClearStrokes;
        public bool CanClearStrokes
        {
            get { return _canClearStrokes; }
            set { _canClearStrokes = value; OnPropertyChanged(); }
        }

        private bool _canUndoStroke;
        public bool CanUndoStroke
        {
            get { return _canUndoStroke; }
            set { _canUndoStroke = value; OnPropertyChanged(); }
        }

        private bool _canRedoStroke;
        public bool CanRedoStroke
        {
            get { return _canRedoStroke; }
            set { _canRedoStroke = value; OnPropertyChanged(); }
        }



        //A new stroke object named MyStroke is created. MyStroke is added to the StrokeCollection of the InkPresenter named MyIP
        private void OnIP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InkPad.CaptureMouse();
            StylusPointCollection MyStylusPointCollection = new StylusPointCollection();
            MyStylusPointCollection.Add(e.StylusDevice.GetStylusPoints(InkPad));
            LastStroke = new Stroke(MyStylusPointCollection);
            InkPad.Strokes.Add(LastStroke);
            CanUndoStroke = true;
            CanClearStrokes = true;

            CanRedoStroke = false;
            nextStrokes.Clear();
        }

        private void OnIP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            InkPad.ReleaseMouseCapture();
        }

        //StylusPoint objects are collected from the MouseEventArgs and added to MyStroke. 
        private void OnIP_MouseMove(object sender, MouseEventArgs e)
        {
            if (LastStroke != null && InkPad.IsMouseCaptured)
            {
                LastStroke.StylusPoints.Add(e.StylusDevice.GetStylusPoints(InkPad));
            }
        }

        //MyStroke is completed
        private void OnIP_LostMouseCapture(object sender, MouseEventArgs e)
        {

        }

        private void OnClearInkPad(object sender, RoutedEventArgs e)
        {
            LastStroke = null;
            nextStrokes.Clear();
            InkPad.Strokes.Clear();
            CanClearStrokes = false;
            CanUndoStroke = false;
            CanRedoStroke = false;
        }

        private void OnUndoLastStroke(object sender, RoutedEventArgs e)
        {
            var strokes = InkPad.Strokes;
            if(strokes.Count > 0)
            {
                nextStrokes.Push(strokes[strokes.Count - 1]);
                strokes.RemoveAt(strokes.Count - 1);
                CanRedoStroke = true;
            }
            if(strokes.Count == 0)
            {
                CanUndoStroke = false;
            }
        }

        private void OnRedoLastStroke(object sender, RoutedEventArgs e)
        {
            if(nextStrokes.Count > 0)
            {
                InkPad.Strokes.Add(nextStrokes.Pop());
                CanUndoStroke = true;
            }
            if(nextStrokes.Count == 0)
            {
                CanRedoStroke = false;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
