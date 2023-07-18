using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestPerformance;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
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

namespace OpenSilver.Samples.Showcase
{
    public partial class HtmlCanvas_Demo : UserControl
    {
        int _lastTickCount = 0;
        private readonly int[] SpriteCount = new int[] { 5, 10, 25, 50, 100, 250, 500, 1000, 2500 };
        bool _loaded = false;

        // Define variables to calculate the average "Frames Per Second":
        int _counterToCalculateAverageFPS = 0;
        const int NumberOfSecondsOverWhichToCalculateAverageFPS = 2;
        int _tickCountToCalculateAverageFPS = Environment.TickCount;

        public HtmlCanvas_Demo()
        {
            this.InitializeComponent();

            this.Unloaded += HtmlCanvas_Demo_Unloaded;
        }

        private async void HtmlCanvas_Demo_Loaded(object sender, RoutedEventArgs e)
        {
#if OPENSILVER
            if (!OpenSilver.Interop.IsRunningInTheSimulator_WorkAround)
#else
            if (!OpenSilver.Interop.IsRunningInTheSimulator)
#endif
            {
                // Load the initial sprites:
                ComboBoxToChooseNumberOfSprites.SelectedIndex = 1; // This will raise the "SelectionChanged" event, which loads the sprites.

                // Start the main drawing loop:
                _lastTickCount = Environment.TickCount;
                _loaded = true;
                await MainLoopAsync();
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
                MessageBox.Show("The Simulator is too slow to run this demo. Please run the demo in the browser instead.");
            }
        }

        void HtmlCanvas_Demo_Unloaded(object sender, RoutedEventArgs e)
        {
            _loaded = false; // This results in the main loop exiting.
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
                sprite.X = rand.Next(Math.Abs((int)canvasWidth - (int)sprite.Width));
                sprite.Y = rand.Next(Math.Abs((int)canvasHeight - (int)sprite.Height));

                // Set its velocity randomly:
                sprite.VelocityX = rand.Next(-10, 10);
                sprite.VelocityY = rand.Next(-10, 10);

                // Add the sprite to the HtmlCanvas:
                HtmlCanvas1.Children.Add(sprite);
            }
        }

        private async Task MainLoopAsync()
        {
            if (!_loaded) return;

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

            await Task.Delay(1);
            await MainLoopAsync();
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "HtmlCanvas_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Performance/HtmlCanvas/HtmlCanvas_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "HtmlCanvas_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Performance/HtmlCanvas/HtmlCanvas_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "MainSprite.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Performance/HtmlCanvas/MainSprite.cs"
                }
            });
        }

    }
}