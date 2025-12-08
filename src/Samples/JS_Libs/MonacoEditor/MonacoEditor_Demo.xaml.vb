Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("monaco", "editor", "code", "javascript", "c#", "xaml", "js", "interop")>
    Partial Public Class MonacoEditor_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            AddHandler csharpRadioButton.Checked, AddressOf OnCSharpRadioButtonChecked
            AddHandler xamlRadioButton.Checked, AddressOf OnXamlRadioButtonChecked

            csharpRadioButton.IsChecked = True
        End Sub

        Private Sub OnCSharpRadioButtonChecked(sender As Object, e As RoutedEventArgs)
            monacoEditor.Language = "csharp"
            monacoEditor.Code = "using System;
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
}"
        End Sub

        Private Sub OnXamlRadioButtonChecked(sender As Object, e As RoutedEventArgs)
            monacoEditor.Language = "xml"
            monacoEditor.Code = "<Page x:Class=""App1.MainPage""
      xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
      xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
      xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
      xmlns:local=""clr-namespace:App1""
      xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
      mc:Ignorable=""d""
      Foreground=""{DynamicResource Theme_TextBrush}"">

    <Grid Background=""{DynamicResource Theme_BackgroundBrush}"">
        <TextBlock Text=""Hello, World!"" FontSize=""23"" Margin=""32,32,0,0"" HorizontalAlignment=""Left"" VerticalAlignment=""Top""/>
    </Grid>
</Page>"
        End Sub
    End Class

End Namespace
