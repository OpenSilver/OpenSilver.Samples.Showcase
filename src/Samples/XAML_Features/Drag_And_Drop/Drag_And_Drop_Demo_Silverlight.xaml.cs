using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OpenSilver.Samples.Showcase
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

        void DragAndDropItem_PointerPressed(object sender, MouseButtonEventArgs e)
        {
            UIElement uielement = (UIElement)sender;
            _objectLeft = Canvas.GetLeft(uielement);
            _objectTop = Canvas.GetTop(uielement);

            _pointerX = e.GetPosition(null).X;
            _pointerY = e.GetPosition(null).Y;
            uielement.CaptureMouse();
            _isPointerCaptured = true;
        }

        void DragAndDropItem_PointerMoved(object sender, MouseEventArgs e)
        {
            UIElement uielement = (UIElement)sender;
            if (_isPointerCaptured)
            {
                // Calculate the new position of the object:
                double deltaH = e.GetPosition(null).X - _pointerX;
                double deltaV = e.GetPosition(null).Y - _pointerY;
                _objectLeft = deltaH + _objectLeft;
                _objectTop = deltaV + _objectTop;

                // Update the object position:
                Canvas.SetLeft(uielement, _objectLeft);
                Canvas.SetTop(uielement, _objectTop);

                // Remember the pointer position:
                _pointerX = e.GetPosition(null).X;
                _pointerY = e.GetPosition(null).Y;
            }
        }

        void DragAndDropItem_PointerReleased(object sender, MouseButtonEventArgs e)
        {
            UIElement uielement = (UIElement)sender;
            _isPointerCaptured = false;
            uielement.ReleaseMouseCapture();
        }
    }
}
