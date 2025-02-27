using OpenSilver.Samples.Showcase.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("XAML", "runtime", "parsing", "UI", "dynamic")]
    public partial class XamlReader_Demo : UserControl
    {
        public XamlReader_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonAddFields_Click(object sender, RoutedEventArgs e)
        {
            string xaml =
@"<Grid xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" Margin=""5"">
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
            object newContent = XamlReader.Load(xaml);
            // Add the newly created XAML controls to the visual tree:
            Container.Children.Add(newContent as UIElement);
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
        }
    }
}
