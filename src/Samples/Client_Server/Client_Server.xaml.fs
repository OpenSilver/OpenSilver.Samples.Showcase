namespace OpenSilver.Samples.Showcase

open System.Windows.Controls

type Client_Server() as this =
    inherit Client_ServerXaml()
    
    do
        this.InitializeComponent()

#if OPENSILVER
        //this.REST_WebClientDemo.Visibility <- System.Windows.Visibility.Collapsed
#endif