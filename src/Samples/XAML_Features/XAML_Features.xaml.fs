namespace OpenSilver.Samples.Showcase

open System.Windows

type Xaml_Features() as this =
    inherit Xaml_FeaturesXaml()
    
    do
        this.InitializeComponent()

        this.MarkupExtensionsDemo.Visibility <- Visibility.Collapsed
        