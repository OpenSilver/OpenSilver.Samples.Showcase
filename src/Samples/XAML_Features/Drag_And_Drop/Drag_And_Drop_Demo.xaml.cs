using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CSHTML5.Samples.Showcase
{
    public partial class Drag_And_Drop_Demo : UserControl
    {
        bool _isPointerCaptured;
        double _pointerX;
        double _pointerY;
        double _objectLeft;
        double _objectTop;

        public Drag_And_Drop_Demo()
        {
            this.InitializeComponent();
        }

        void DragAndDropItem_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            UIElement uielement = (UIElement)sender;
            _pointerX = e.GetCurrentPoint(null).Position.X;
            _pointerY = e.GetCurrentPoint(null).Position.Y;
            _objectLeft = Canvas.GetLeft(uielement);
            _objectTop = Canvas.GetTop(uielement);
            uielement.CapturePointer(e.Pointer);
            _isPointerCaptured = true;
        }

        void DragAndDropItem_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            UIElement uielement = (UIElement)sender;
            if (_isPointerCaptured)
            {
                // Calculate the new position of the object:
                double deltaH = e.GetCurrentPoint(null).Position.X - _pointerX;
                double deltaV = e.GetCurrentPoint(null).Position.Y - _pointerY;
                _objectLeft = deltaH + _objectLeft;
                _objectTop = deltaV + _objectTop;

                // Update the object position:
                Canvas.SetLeft(uielement, _objectLeft);
                Canvas.SetTop(uielement, _objectTop);

                // Remember the pointer position:
                _pointerX = e.GetCurrentPoint(null).Position.X;
                _pointerY = e.GetCurrentPoint(null).Position.Y;
            }
        }

        void DragAndDropItem_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            UIElement uielement = (UIElement)sender;
            _isPointerCaptured = false;
            uielement.ReleasePointerCapture(e.Pointer);
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Drag_And_Drop_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Features/Drag_And_Drop/Drag_And_Drop_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Drag_And_Drop_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Features/Drag_And_Drop/Drag_And_Drop_Demo.xaml.cs"
                }
            });
        }

    }
}
