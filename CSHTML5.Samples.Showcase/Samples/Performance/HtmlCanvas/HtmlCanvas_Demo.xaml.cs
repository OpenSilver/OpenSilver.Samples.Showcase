using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestPerformance;
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
    public partial class HtmlCanvas_Demo : UserControl
    {
        int _lastTickCount = 0;
        private readonly int[] SpriteCount = new int[] { 5, 10, 25, 50, 100, 250, 500, 1000, 2500, 5000, 10000, 25000, 50000, 100000, 250000, 500000, 1000000 };

        // Define variables to calculate the average "Frames Per Second":
        int _counterToCalculateAverageFPS = 0;
        const int NumberOfSecondsOverWhichToCalculateAverageFPS = 2;
        int _tickCountToCalculateAverageFPS = Environment.TickCount;

        public HtmlCanvas_Demo()
        {
            this.InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!CSHTML5.Interop.IsRunningInTheSimulator)
            {
                // Load the initial sprites:
                ComboBoxToChooseNumberOfSprites.SelectedIndex = 1; // This will raise the "SelectionChanged" event, which loads the sprites.

                // Start the main drawing loop:
                _lastTickCount = Environment.TickCount;
                MainLoop();
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
                MessageBox.Show("This demo is too slow to run in the Simulator. Please run in the browser instead.");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected element in the ComboBox:
            int selectedIndex = ((ComboBox)sender).SelectedIndex;

            // Get the number of sprites:
            int spriteCount = SpriteCount[selectedIndex];

            // Load the sprites:
            LoadSprites(spriteCount);
        }

        private void LoadSprites(int spriteCount)
        {
            Random rand = new Random();
            double canvasWidth = HtmlCanvas1.ActualWidth;
            double canvasHeight = HtmlCanvas1.ActualHeight;

            // Remove the previous sprites, if any:
            HtmlCanvas1.Children.Clear();

            // Create the sprites:
            for (int i = 0; i < spriteCount; i += 1)
            {
                // Create a new sprite:
                var sprite = new MainSprite(i);

                // Set its position randomly:
                sprite.X = rand.Next((int)canvasWidth - (int)sprite.Width);
                sprite.Y = rand.Next((int)canvasHeight - (int)sprite.Height);

                // Set its velocity randomly:
                sprite.VelocityX = rand.Next(-10, 10);
                sprite.VelocityY = rand.Next(-10, 10);

                // Add the sprite to the HtmlCanvas:
                HtmlCanvas1.Children.Add(sprite);
            }
        }

        private void MainLoop()
        {
            // Determine the time elapsed since the last redraw:
            int newTickCount = Environment.TickCount;
            int timeElapsedInMilliseconds = (newTickCount - _lastTickCount);
            _lastTickCount = newTickCount;

            // Determine the size of the window in pixels:
            double canvasWidth = HtmlCanvas1.ActualWidth;
            double canvasHeight = HtmlCanvas1.ActualHeight;

            // Move the sprites:
            foreach (var child in HtmlCanvas1.Children)
            {
                double timeFactor = timeElapsedInMilliseconds / 30d; // This is intended to adjust the movement so that, no matter the frames per second, the items always move at the same speed.

                var sprite = (MainSprite)child;
                sprite.Move(sprite.VelocityX * timeFactor, sprite.VelocityY * timeFactor);

                if (sprite.X < 0)
                {
                    sprite.X = 0;
                    sprite.VelocityX = Math.Abs(sprite.VelocityX);
                }
                else if (sprite.X > canvasWidth)
                {
                    sprite.X = canvasWidth;
                    sprite.VelocityX = -Math.Abs(sprite.VelocityX);
                }
                if (sprite.Y < 0)
                {
                    sprite.Y = 0;
                    sprite.VelocityY = Math.Abs(sprite.VelocityY);
                }
                else if (sprite.Y > canvasHeight)
                {
                    sprite.Y = canvasHeight;
                    sprite.VelocityY = -Math.Abs(sprite.VelocityY);
                }
            }

            // Redraw:
            HtmlCanvas1.Draw();

            // Re-enter this same method after a "Do Events":
            Dispatcher.BeginInvoke(MainLoop);

            // Calculate the average "Frames Per Second":
            _counterToCalculateAverageFPS++;
            int tickCountToCalculateAverageFPS = Environment.TickCount; // In milliseconds
            double durationSinceLastFPSCalculation = (tickCountToCalculateAverageFPS - _tickCountToCalculateAverageFPS) / 1000d; // In seconds
            if (durationSinceLastFPSCalculation > NumberOfSecondsOverWhichToCalculateAverageFPS)
            {
                // Divide the number of frames by the time elapsed:
                double framesPerSecond = (_counterToCalculateAverageFPS / durationSinceLastFPSCalculation);
                FramesPerSecondTextBlock.Text = ((int)framesPerSecond).ToString();

                // Reset counter:
                _counterToCalculateAverageFPS = 0;
                _tickCountToCalculateAverageFPS = tickCountToCalculateAverageFPS;
            }
        }
    }
}