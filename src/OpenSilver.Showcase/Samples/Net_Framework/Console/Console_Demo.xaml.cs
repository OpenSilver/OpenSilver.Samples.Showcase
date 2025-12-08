using OpenSilver.Showcase.Search;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase;

[SearchKeywords("console", "output", "debugging", "logging")]
public partial class Console_Demo : UserControl
{
    public Console_Demo()
    {
        InitializeComponent();
    }

    private void OnWriteToConsoleButtonClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine($"Console test message {DateTime.Now}");
    }

    private void OnWriteToDebugButtonClick(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine($"Debug test message {DateTime.Now}");
    }
}
