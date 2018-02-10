using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHTML5.Native.Html.Controls;
using Windows.UI;

namespace TestPerformance
{
    public class MainSprite : ContainerElement
    {
        public double VelocityX;
        public double VelocityY;

        public MainSprite(int index)
        {
            Random rand = new Random(index);

            // Set a random background color:
            //this.FillColor = Color.FromArgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));

            // Set the size:
            this.Width = 38d + Math.Log10(index) * 8d;
            this.Height = 30d;

            // Add the small logo:
            var logo = new ImageElement()
            {
                Source = "ms-appx:///Other/smallBall.png",
                Width = 16,
                Height = 16,
                X = 5,
                Y = 6
            };
            this.Children.Add(logo);

            // Display the index:
            var text = new TextElement()
            {
                Text = index.ToString(),
                FontHeight = 14d,
                X = 29,
                Y = 6
            };
            this.Children.Add(text);
        }
    }
}