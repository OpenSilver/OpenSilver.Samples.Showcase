namespace TestPerformance

open System
open CSHTML5.Native.Html.Controls

type MainSprite(index: int) as this =
    inherit ContainerElement()

    let rand = Random(index)

    do
        // Set a random background color:
        // this.FillColor <- Color.FromArgb(byte rand.Next(256), byte rand.Next(256), byte rand.Next(256), byte rand.Next(256))

        // Set the size:
        this.Width <- 38.0 + Math.Log10(float(index + 1)) * 8.0
        this.Height <- 30.0

        // Add the small logo:
        let logo =
            new ImageElement(
                Source = "ms-appx:///Other/smallBall.png",
                Width = 16.0,
                Height = 16.0,
                X = 5.0,
                Y = 6.0)
        this.Children.Add(logo)

        // Display the index:
        let text =
            new TextElement(
                Text = index.ToString(),
                FontHeight = 14.0,
                X = 29.0,
                Y = 6.0)
        this.Children.Add(text)
    member val VelocityX = 0.0 with get, set
    member val VelocityY = 0.0 with get, set