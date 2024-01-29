namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type Xaml_Features() as this =
    inherit Xaml_FeaturesXaml()
    
    do
        this.InitializeComponent()
#if OPENSILVER
        //this.Binding1Demo.Visibility <- Visibility.Collapsed
#endif
        this.MarkupExtensionsDemo.Visibility <- Visibility.Collapsed
        