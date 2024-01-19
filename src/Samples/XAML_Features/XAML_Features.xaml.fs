namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
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

type Xaml_Features() as this =
    inherit Xaml_FeaturesXaml()
    
    do
        this.InitializeComponent()
#if OPENSILVER
        //this.Binding1Demo.Visibility <- Visibility.Collapsed
#endif
        this.MarkupExtensionsDemo.Visibility <- Visibility.Collapsed
        