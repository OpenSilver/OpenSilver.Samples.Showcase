Imports System
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase

    'code inspired from: https://jobijoy.blogspot.com/2008/04/simple-radial-panel-for-wpf-and.html
    Public Class RadialPanel
        Inherits Panel

        'Measure each children and give as much room as they want 
        Protected Overrides Function MeasureOverride(ByVal availableSize As Size) As Size
            For Each elem As UIElement In Children
                'Give Infinite size as the avaiable size for all the children
                elem.Measure(New Size(Double.PositiveInfinity, Double.PositiveInfinity))
            Next

            Return MyBase.MeasureOverride(availableSize)
        End Function

        'Arrange all children based on the geometric equations for the circle.
        Protected Overrides Function ArrangeOverride(ByVal finalSize As Size) As Size
            If Children.Count = 0 Then Return finalSize

            Dim _angle As Double = 0
            'Degrees converted to Radian by multiplying with PI/180
            Dim _incrementalAngularSpace As Double = (360.0 / Children.Count) * (Math.PI / 180)

            'An approximate radii based on the avialable size , obviously a better approach is needed here.
            Dim radiusX As Double = finalSize.Width / 2.4
            Dim radiusY As Double = finalSize.Height / 2.4

            For Each elem As UIElement In Children
                'Calculate the point on the circle for the element
                Dim childPoint As Point = New Point(Math.Cos(_angle) * radiusX, -Math.Sin(_angle) * radiusY)
                'Offsetting the point to the Avalable rectangular area which is FinalSize.
                Dim actualChildPoint As Point = New Point(finalSize.Width / 2 + childPoint.X - elem.DesiredSize.Width / 2, finalSize.Height / 2 + childPoint.Y - elem.DesiredSize.Height / 2)

                'Call Arrange method on the child element by giving the calculated point as the placementPoint.
                elem.Arrange(New Rect(actualChildPoint.X, actualChildPoint.Y, elem.DesiredSize.Width, elem.DesiredSize.Height))

                'Calculate the new _angle for the next element
                _angle += _incrementalAngularSpace
            Next

            Return finalSize
        End Function
    End Class
End Namespace
