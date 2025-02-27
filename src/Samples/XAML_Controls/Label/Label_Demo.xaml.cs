using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("text", "label", "description", "content", "UI")]
    public partial class Label_Demo : UserControl
    {
        public Label_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonViewMore_Click(object sender, RoutedEventArgs e)
        {
            //ChildWindowHelper.ShowChildWindow(new Button_Demo_More());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked the button!");
        }
    }
}
