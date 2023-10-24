using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class Printing_Demo : UserControl
    {
        public Printing_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            var invoiceToPrint = new Invoice();
            CSHTML5.Native.Html.Printing.PrintManager.Print(invoiceToPrint);
        }
    }
}
