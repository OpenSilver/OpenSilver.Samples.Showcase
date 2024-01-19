namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows.Controls
#else
open Windows.Foundation
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Navigation
#endif

type Welcome() as this =
    inherit WelcomeXaml()

    do
        this.InitializeComponent()
        
#if OPENSILVER
        this.IntroductionTextBlock.Text <- "This app was written in XAML and C# (or VB.NET or F#), and compiled to WebAssembly using OpenSilver."
#endif
