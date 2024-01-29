namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Windows
open System.Windows.Controls
open System.Windows.Controls.Primitives
open System.Windows.Data
open System.Windows.Input
open System.Windows.Markup
open System.Windows.Media
open System.Windows.Navigation

type XamlReader_Demo() as this =
    inherit XamlReader_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonAddFields_Click(sender: obj, e: RoutedEventArgs) =
        let xaml = @"<Grid xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" Margin=""5"">
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width=""*""/>
                            <ColumnDefinition Width=""20""/>
                            <ColumnDefinition Width=""*""/>
                        </Grid.ColumnDefinitions>
                        <Grid.RowDefinitions>
                            <RowDefinition Height=""Auto""/>
                            <RowDefinition Height=""5""/>
                            <RowDefinition Height=""Auto""/>
                        </Grid.RowDefinitions>
                        <TextBlock Text=""First name:"" VerticalAlignment = ""Center""/>
                        <TextBox Text=""John"" Grid.Column = ""2"" />
                        <TextBlock Text=""Last name:"" Grid.Row=""2"" VerticalAlignment=""Center""/>
                        <TextBox Text=""Doe"" Grid.Row=""2"" Grid.Column=""2""/>
                    </Grid>";
        let newContent = XamlReader.Load(xaml) :?> UIElement
        this.Container.Children.Add(newContent)

    member private this.ButtonClear_Click(sender: obj, e: RoutedEventArgs) =
        this.Container.Children.Clear()
