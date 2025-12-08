namespace OpenSilver.Showcase

open System.Windows

[<OpenSilver.Showcase.Search.SearchKeywords("monaco", "editor", "code", "javascript", "c#", "xaml", "js", "interop")>]
type MonacoEditor_Demo() as this =
    inherit MonacoEditor_DemoXaml()
    
    let onCSharpRadioButtonChecked (sender: obj) (e: RoutedEventArgs) =
        this.monacoEditor.Language <- "csharp"
        this.monacoEditor.Code <- """using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace App1;

public partial class MainPage : Page
{
    private int _clickCount;

    public MainPage()
    {
        InitializeComponent();
    }
}"""
        
    let onXamlRadioButtonChecked (sender: obj) (e: RoutedEventArgs) =
        this.monacoEditor.Language <- "xml"
        this.monacoEditor.Code <- """<Page x:Class="App1.MainPage"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
      xmlns:local="clr-namespace:App1"
      xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
      mc:Ignorable="d"
      Foreground="{DynamicResource Theme_TextBrush}">

    <Grid Background="{DynamicResource Theme_BackgroundBrush}">
        <TextBlock Text="Hello, World!" FontSize="23" Margin="32,32,0,0" HorizontalAlignment="Left" VerticalAlignment="Top"/>
    </Grid>
</Page>"""
    
    do 
        this.InitializeComponent()
        this.csharpRadioButton.Checked.AddHandler(RoutedEventHandler(onCSharpRadioButtonChecked))
        this.xamlRadioButton.Checked.AddHandler(RoutedEventHandler(onXamlRadioButtonChecked))
        this.csharpRadioButton.IsChecked <- System.Nullable<bool>(true)
