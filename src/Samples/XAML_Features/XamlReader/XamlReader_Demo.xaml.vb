Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Markup

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("XAML", "runtime", "parsing", "UI", "dynamic")>
    Partial Public Class XamlReader_Demo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonAddFields_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim xaml As String = "<Grid xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" Margin=""5"">
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
                    </Grid>"
            Dim newContent As Object = XamlReader.Load(xaml)
            'Add the newly created XAML controls to the visual tree:
            Container.Children.Add(TryCast(newContent, UIElement))
        End Sub

        Private Sub ButtonClear_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Container.Children.Clear()
        End Sub
    End Class
End Namespace
