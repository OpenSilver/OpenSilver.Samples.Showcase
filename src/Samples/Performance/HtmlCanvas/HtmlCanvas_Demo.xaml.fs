namespace OpenSilver.Samples.Showcase

open TestPerformance
open System
open System.Windows
open System.Windows.Controls
open System.Threading.Tasks

type HtmlCanvas_Demo() as this =
    inherit HtmlCanvas_DemoXaml()

    let mutable _lastTickCount = 0
    let SpriteCount = [|5; 10; 25; 50; 100; 250; 500; 1000; 2500|]
    let mutable _loaded = false
    let mutable _counterToCalculateAverageFPS = 0
    let NumberOfSecondsOverWhichToCalculateAverageFPS = 2
    let mutable _tickCountToCalculateAverageFPS = Environment.TickCount

    do
        this.InitializeComponent()
        this.Unloaded.Add(fun args -> this.HtmlCanvas_Demo_Unloaded(this, args))

    member private this.HtmlCanvas_Demo_Loaded(sender: obj, e: RoutedEventArgs) =
        async {
            if not OpenSilver.Interop.IsRunningInTheSimulator then
                // Load the initial sprites:
                this.ComboBoxToChooseNumberOfSprites.SelectedIndex <- 1 // This will raise the "SelectionChanged" event, which loads the sprites.

                // Start the main drawing loop:
                _lastTickCount <- Environment.TickCount
                _loaded <- true
                do! this.MainLoopAsync()
            else
                this.Visibility <- Visibility.Collapsed
                MessageBox.Show("The Simulator is too slow to run this demo. Please run the demo in the browser instead.") |> ignore
        } |> Async.Start

    member private this.HtmlCanvas_Demo_Unloaded(sender : obj, e : RoutedEventArgs) =
        _loaded <- false; // This results in the main loop exiting.

    member this.ComboBox_SelectionChanged(sender: obj, e: SelectionChangedEventArgs) =
        // Get the selected element in the ComboBox:
        let selectedIndex = (sender :?> ComboBox).SelectedIndex

        // Get the number of sprites:
        let spriteCount = SpriteCount.[selectedIndex]

        // Load the sprites:
        this.LoadSprites(spriteCount)

    member this.LoadSprites(spriteCount: int) =
        let rand = Random()
        let canvasWidth = this.HtmlCanvas1.ActualWidth
        let canvasHeight = this.HtmlCanvas1.ActualHeight

        // Remove the previous sprites, if any:
        this.HtmlCanvas1.Children.Clear()

        // Create the sprites:
        for i in 0 .. spriteCount - 1 do
            // Create a new sprite:
            let sprite = MainSprite(i)

            // Set its position randomly:
            sprite.X <- float (rand.Next(Math.Abs(int canvasWidth - int sprite.Width)))
            sprite.Y <- float (rand.Next(Math.Abs(int canvasHeight - int sprite.Height)))

            // Set its velocity randomly:
            sprite.VelocityX <- float (rand.Next(-10, 10))
            sprite.VelocityY <- float (rand.Next(-10, 10))

            // Add the sprite to the HtmlCanvas:
            this.HtmlCanvas1.Children.Add(sprite)

    member this.MainLoopAsync() =
        async {
            if not _loaded then
                ()
        
            // Determine the time elapsed since the last redraw:
            let newTickCount = Environment.TickCount
            let timeElapsedInMilliseconds = (newTickCount - _lastTickCount)
            _lastTickCount <- newTickCount

            // Determine the size of the window in pixels:
            let canvasWidth = this.HtmlCanvas1.ActualWidth
            let canvasHeight = this.HtmlCanvas1.ActualHeight

            // Move the sprites:
            for child in this.HtmlCanvas1.Children do
                let timeFactor = float timeElapsedInMilliseconds / 30.0 // This is intended to adjust the movement so that, no matter the frames per second, the items always move at the same speed.

                let sprite = child :?> MainSprite
                sprite.Move(sprite.VelocityX * timeFactor, sprite.VelocityY * timeFactor)

                if sprite.X < 0.0 then
                    sprite.X <- 0.0
                    sprite.VelocityX <- abs sprite.VelocityX
                elif sprite.X > canvasWidth then
                    sprite.X <- canvasWidth
                    sprite.VelocityX <- -abs sprite.VelocityX
                if sprite.Y < 0.0 then
                    sprite.Y <- 0.0
                    sprite.VelocityY <- abs sprite.VelocityY
                elif sprite.Y > canvasHeight then
                    sprite.Y <- canvasHeight
                    sprite.VelocityY <- -abs sprite.VelocityY

            // Redraw:
            this.HtmlCanvas1.Draw()

            // Calculate the average "Frames Per Second":
            _counterToCalculateAverageFPS <- _counterToCalculateAverageFPS + 1
            let tickCountToCalculateAverageFPS = Environment.TickCount // In milliseconds
            let durationSinceLastFPSCalculation = float (tickCountToCalculateAverageFPS - _tickCountToCalculateAverageFPS) / 1000.0 // In seconds
            if durationSinceLastFPSCalculation > float NumberOfSecondsOverWhichToCalculateAverageFPS then
                // Divide the number of frames by the time elapsed:
                let framesPerSecond = float _counterToCalculateAverageFPS / durationSinceLastFPSCalculation
                this.FramesPerSecondTextBlock.Text <- string (int framesPerSecond)

                // Reset counter:
                _counterToCalculateAverageFPS <- 0
                _tickCountToCalculateAverageFPS <- tickCountToCalculateAverageFPS

            do! Task.Delay(1) |> Async.AwaitTask
            do! this.MainLoopAsync()
        }
