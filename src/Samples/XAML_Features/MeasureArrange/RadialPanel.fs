namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Linq
open System.Text
open System.Threading.Tasks
open System.Windows.Controls
open System.Windows

//code taken from: https://jobijoy.blogspot.com/2008/04/simple-radial-panel-for-wpf-and.html
type RadialPanel() =
    inherit Panel()

    // MeasureOverride: Measure each child and give as much room as they want
    override this.MeasureOverride(availableSize: Size) =
        for elem in this.Children do
            // Give Infinite size as the available size for all the children
            elem.Measure(Size(Double.PositiveInfinity, Double.PositiveInfinity))
        base.MeasureOverride(availableSize)

    // ArrangeOverride: Arrange all children based on the geometric equations for the circle
    override this.ArrangeOverride(finalSize: Size) =
        if this.Children.Count = 0 then finalSize
        else
            let mutable angle = 0.0

            // Degrees converted to Radian by multiplying with PI / 180
            let incrementalAngularSpace = (360.0 / float this.Children.Count) * (Math.PI / 180.0)

            // An approximate radii based on the available size
            let radiusX = finalSize.Width / 2.4
            let radiusY = finalSize.Height / 2.4

            for elem in this.Children do
                // Calculate the point on the circle for the element
                let childPoint = Point(Math.Cos(angle) * radiusX, -Math.Sin(angle) * radiusY)

                // Offsetting the point to the Available rectangular area which is finalSize
                let actualChildPoint = Point(finalSize.Width / 2.0 + childPoint.X - elem.DesiredSize.Width / 2.0, finalSize.Height / 2.0 + childPoint.Y - elem.DesiredSize.Height / 2.0)

                // Call Arrange method on the child element by giving the calculated point as the placementPoint
                elem.Arrange(Rect(actualChildPoint.X, actualChildPoint.Y, elem.DesiredSize.Width, elem.DesiredSize.Height))

                // Calculate the new angle for the next element
                angle <- angle + incrementalAngularSpace

            finalSize
