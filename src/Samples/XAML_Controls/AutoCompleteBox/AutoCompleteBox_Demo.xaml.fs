namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
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

type AutoCompleteBox_Demo() as this =
    inherit AutoCompleteBox_DemoXaml()

    do
        this.InitializeComponent()
        
        let source = "Lorem Ipsum Dolor Sit Amet Consectetur Adipiscing Elit Ut Odio Urna Sollicitudin Sed Nunc Ut Pretium Convallis Enim Mauris A Dapibus Ipsum Nec Dignissim Lorem Curabitur Efficitur Sed Lorem Et Cursus Interdum Et Malesuada Fames Ac Ante Ipsum Primis In Faucibus Vestibulum Varius Vestibulum Pellentesque Ut Pretium Est At Quam Aliquam Porta Donec At Purus Velit In Tempus Ultricies Nisl Eget Sagittis Nam Molestie Odio Nunc Accumsan Rhoncus Mi Ornare Eu Mauris In Cursus Nunc Maecenas Placerat Rutrum Lorem Et Accumsan Justo Laoreet Vel Vivamus Condimentum Nisi Id Dui Iaculis Venenatis Ac Sed Massa Etiam Nec Condimentum Nunc Proin Hendrerit Tortor Sit Amet Consectetur Sollicitudin Curabitur Sit Amet Lectus Risus Mauris Sagittis Urna Ante Finibus Rhoncus Libero Venenatis Id Integer Pharetra Elit Id Ligula Fermentum Ullamcorper Sed Vitae Tristique Nisl Nunc Condimentum Vel Mi Quis Porttitor Proin A Eros Aliquam Dignissim Lorem Eu Ultricies Metus Donec Sit Amet Leo Odio Mauris Volutpat Turpis Nec Ligula Convallis Rutrum Duis Ornare Aliquet Sollicitudin Fusce Condimentum Porta Lorem Ut Laoreet Ipsum Rutrum Vel Integer Efficitur Consectetur Libero Venenatis Aliquam Leo Phasellus Interdum Est Viverra Sem Rhoncus Non Lacinia Tellus Luctus Duis Elit Nisl Euismod Sed Felis Sit Amet Volutpat Dignissim Lacus Ut Porta Ultricies Turpis Eget Feugiat Enim Placerat Nec Fusce A Mauris Ut Leo Ultrices Ullamcorper Phasellus Laoreet Justo Dolor Eu Fringilla Nunc Rhoncus Vitae Aenean Ut Massa Dui Sed Odio Sapien Cursus Sed Blandit A Malesuada Eu Nulla Mauris Ac Mauris Sit Amet Sem Cursus Consequat Sed Ut Tortor Donec Ullamcorper Pharetra Leo In Accumsan Vestibulum Ornare Odio Eu Quam Accumsan At Hendrerit Purus Porta Donec Urna Leo Gravida In Ligula Eget Porttitor Condimentum Neque Aliquam Leo Mi Porta In Nibh Quis Aliquet Congue Justo Suspendisse Feugiat Maximus Fermentum Aenean Quis Orci Ac Odio Efficitur Tincidunt Vel Id Odio Integer Et Lacinia Dui Mauris Efficitur Tincidunt Justo Sit Amet Gravida Arcu Dapibus At Aenean Suscipit Volutpat Faucibus Integer In Libero Leo"
        let words = source.Split(' ')
                  |> Seq.filter (fun word -> String.length word >= 3)
                  |> HashSet<string>

        this.autoCompleteBox.ItemsSource <- words

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "AutoCompleteBox_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/AutoCompleteBox/AutoCompleteBox_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "AutoCompleteBox_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/AutoCompleteBox/AutoCompleteBox_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "AutoCompleteBox_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/AutoCompleteBox/AutoCompleteBox_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "AutoCompleteBox_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/AutoCompleteBox/AutoCompleteBox_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "DefaultAutoCompleteBoxStyle.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/AutoCompleteBox/Styles/DefaultAutoCompleteBoxStyle.xaml")
        ])
