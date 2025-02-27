﻿using OpenSilver.Samples.Showcase.Search;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("input", "autocomplete", "auto-complete", "text", "entry", "search")]
    public partial class AutoCompleteBox_Demo : UserControl
    {
        public AutoCompleteBox_Demo()
        {
            this.InitializeComponent();

            string source = "Lorem Ipsum Dolor Sit Amet Consectetur Adipiscing Elit Ut Odio Urna Sollicitudin Sed Nunc Ut Pretium Convallis Enim Mauris A Dapibus Ipsum Nec Dignissim Lorem Curabitur Efficitur Sed Lorem Et Cursus Interdum Et Malesuada Fames Ac Ante Ipsum Primis In Faucibus Vestibulum Varius Vestibulum Pellentesque Ut Pretium Est At Quam Aliquam Porta Donec At Purus Velit In Tempus Ultricies Nisl Eget Sagittis Nam Molestie Odio Nunc Accumsan Rhoncus Mi Ornare Eu Mauris In Cursus Nunc Maecenas Placerat Rutrum Lorem Et Accumsan Justo Laoreet Vel Vivamus Condimentum Nisi Id Dui Iaculis Venenatis Ac Sed Massa Etiam Nec Condimentum Nunc Proin Hendrerit Tortor Sit Amet Consectetur Sollicitudin Curabitur Sit Amet Lectus Risus Mauris Sagittis Urna Ante Finibus Rhoncus Libero Venenatis Id Integer Pharetra Elit Id Ligula Fermentum Ullamcorper Sed Vitae Tristique Nisl Nunc Condimentum Vel Mi Quis Porttitor Proin A Eros Aliquam Dignissim Lorem Eu Ultricies Metus Donec Sit Amet Leo Odio Mauris Volutpat Turpis Nec Ligula Convallis Rutrum Duis Ornare Aliquet Sollicitudin Fusce Condimentum Porta Lorem Ut Laoreet Ipsum Rutrum Vel Integer Efficitur Consectetur Libero Venenatis Aliquam Leo Phasellus Interdum Est Viverra Sem Rhoncus Non Lacinia Tellus Luctus Duis Elit Nisl Euismod Sed Felis Sit Amet Volutpat Dignissim Lacus Ut Porta Ultricies Turpis Eget Feugiat Enim Placerat Nec Fusce A Mauris Ut Leo Ultrices Ullamcorper Phasellus Laoreet Justo Dolor Eu Fringilla Nunc Rhoncus Vitae Aenean Ut Massa Dui Sed Odio Sapien Cursus Sed Blandit A Malesuada Eu Nulla Mauris Ac Mauris Sit Amet Sem Cursus Consequat Sed Ut Tortor Donec Ullamcorper Pharetra Leo In Accumsan Vestibulum Ornare Odio Eu Quam Accumsan At Hendrerit Purus Porta Donec Urna Leo Gravida In Ligula Eget Porttitor Condimentum Neque Aliquam Leo Mi Porta In Nibh Quis Aliquet Congue Justo Suspendisse Feugiat Maximus Fermentum Aenean Quis Orci Ac Odio Efficitur Tincidunt Vel Id Odio Integer Et Lacinia Dui Mauris Efficitur Tincidunt Justo Sit Amet Gravida Arcu Dapibus At Aenean Suscipit Volutpat Faucibus Integer In Libero Leo";

            HashSet<string> words = new HashSet<string>(source.Split(' ').Where(word => word.Length >= 3));

            autoCompleteBox.ItemsSource = words;
        }
    }
}
