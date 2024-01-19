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

type Client_Server() as this =
    inherit Client_ServerXaml()
    
    do
        this.InitializeComponent()

#if OPENSILVER
        //this.REST_WebClientDemo.Visibility <- System.Windows.Visibility.Collapsed
#endif