namespace OpenSilver.Samples.Showcase

open System.Windows.Controls

type Welcome() as this =
    inherit WelcomeXaml()

    do
        this.InitializeComponent()
        
#if OPENSILVER
        this.IntroductionTextBlock.Text <- "This app was written in XAML and C# (or VB.NET or F#), and compiled to WebAssembly using OpenSilver."
#endif
