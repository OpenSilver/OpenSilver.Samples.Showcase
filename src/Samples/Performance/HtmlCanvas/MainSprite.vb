Imports System
Imports CSHTML5.Native.Html.Controls
#If Not SLMIGRATION
using Windows.UI;
#End If

Namespace Global.TestPerformance
    Public Class MainSprite
        Inherits ContainerElement
        Public VelocityX As Double
        Public VelocityY As Double

        Public Sub New(ByVal index As Integer)
            Dim rand As Random = New Random(index)

            ' Set a random background color:
            'this.FillColor = Color.FromArgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));

            ' Set the size:
            Width = 38.0R + Math.Log10(index + 1) * 8.0R
            Height = 30.0R

            ' Add the small logo:
            Dim logo = New ImageElement() With {
    .Source = "ms-appx:///Other/smallBall.png",
    .Width = 16,
    .Height = 16,
    .X = 5,
    .Y = 6
}
            Children.Add(logo)

            ' Display the index:
            Dim text = New TextElement() With {
    .Text = index.ToString(),
    .FontHeight = 14.0R,
    .X = 29,
    .Y = 6
}
            Children.Add(text)
        End Sub
    End Class
End Namespace
