namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open System.Windows

type Net_Framework() as this =
    inherit Net_FrameworkXaml()

    do
        this.InitializeComponent()

#if OPENSILVER
        // this.File_OpenDemo.Visibility <- Visibility.Collapsed
        // this.File_SaveDemo.Visibility <- Visibility.Collapsed
        // this.ZipDemo.Visibility <- Visibility.Collapsed
        // this.IsolatedStorageFileDemo.Visibility <- Visibility.Collapsed
        // this.IsolatedStorageSettingsDemo.Visibility <- Visibility.Collapsed
        this.JSON_SerializerDemo.Visibility <- Visibility.Collapsed
        this.GetRessourceStreamDemo.Visibility <- Visibility.Collapsed
        // this.FullScreenDemo.Visibility <- Visibility.Collapsed
        this.ConsoleDemo.Visibility <- Visibility.Collapsed
#endif
        this.RESXDemo.Visibility <- Visibility.Collapsed
