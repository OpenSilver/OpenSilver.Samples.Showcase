namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Other
open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Windows
open System.Windows.Controls
open System.Windows.Controls.DataVisualization.Charting
open System.Windows.Controls.Primitives
open System.Windows.Data
open System.Windows.Input
open System.Windows.Media
open System.Windows.Navigation

type LineSeries_Demo() as this =
    inherit LineSeries_DemoXaml()

    do
        this.InitializeComponent()
        
        this.ChairsSeries.ItemsSource <- Sales.Chairs;
        this.TablesSeries.ItemsSource <- Sales.Tables;
