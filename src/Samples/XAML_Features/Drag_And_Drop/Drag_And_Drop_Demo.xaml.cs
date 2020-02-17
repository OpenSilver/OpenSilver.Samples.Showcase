using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

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

#if SLMIGRATION
        void DragAndDropItem_PointerPressed(object sender, MouseButtonEventArgs e)
#else
        void DragAndDropItem_PointerPressed(object sender, PointerRoutedEventArgs e)
#endif
        {
            UIElement uielement = (UIElement)sender;
            _objectLeft = Canvas.GetLeft(uielement);
            _objectTop = Canvas.GetTop(uielement);

#if SLMIGRATION
            _pointerX = e.GetPosition(null).X;
            _pointerY = e.GetPosition(null).Y;
            uielement.CaptureMouse();
#else
            _pointerX = e.GetCurrentPoint(null).Position.X;
            _pointerY = e.GetCurrentPoint(null).Position.Y;
            uielement.CapturePointer(e.Pointer);
#endif
            _isPointerCaptured = true;
        }

#if SLMIGRATION
        void DragAndDropItem_PointerMoved(object sender, MouseEventArgs e)
#else
        void DragAndDropItem_PointerMoved(object sender, PointerRoutedEventArgs e)
#endif
        {
            UIElement uielement = (UIElement)sender;
            if (_isPointerCaptured)
            {
                // Calculate the new position of the object:
#if SLMIGRATION
                double deltaH = e.GetPosition(null).X - _pointerX;
                double deltaV = e.GetPosition(null).Y - _pointerY;
#else
                double deltaH = e.GetCurrentPoint(null).Position.X - _pointerX;
                double deltaV = e.GetCurrentPoint(null).Position.Y - _pointerY;
#endif
                _objectLeft = deltaH + _objectLeft;
                _objectTop = deltaV + _objectTop;

                // Update the object position:
                Canvas.SetLeft(uielement, _objectLeft);
                Canvas.SetTop(uielement, _objectTop);

                // Remember the pointer position:
#if SLMIGRATION
                _pointerX = e.GetPosition(null).X;
                _pointerY = e.GetPosition(null).Y;
#else
                _pointerX = e.GetCurrentPoint(null).Position.X;
                _pointerY = e.GetCurrentPoint(null).Position.Y;
#endif
            }
        }

#if SLMIGRATION
        void DragAndDropItem_PointerReleased(object sender, MouseButtonEventArgs e)
#else
        void DragAndDropItem_PointerReleased(object sender, PointerRoutedEventArgs e)
#endif
        {
            UIElement uielement = (UIElement)sender;
            _isPointerCaptured = false;
#if SLMIGRATION
            uielement.ReleaseMouseCapture();
#else
            uielement.ReleasePointerCapture(e.Pointer);
#endif
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Drag_And_Drop_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Features/Drag_And_Drop/Drag_And_Drop_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Drag_And_Drop_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/src/Samples/XAML_Features/Drag_And_Drop/Drag_And_Drop_Demo.xaml.cs"
                }
            });
        }

    }
}
