using OpenSilver.Samples.Showcase.Search;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("properties", "dependency property", "binding", "XAML", "UI")]
    public partial class DependencyProperties_Demo : UserControl
    {
        public DependencyProperties_Demo()
        {
            this.InitializeComponent();
        }

        public int MySampleDependencyProperty
        {
            get { return (int)GetValue(MySampleDependencyPropertyProperty); }
            set { SetValue(MySampleDependencyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySampleDependencyProperty. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySampleDependencyPropertyProperty =
            DependencyProperty.Register(
                "MySampleDependencyProperty", // This is the name of the DependencyProperty
                typeof(int), // This is the type of the DependencyProperty
                typeof(DependencyProperties_Demo), // This is the type of the class that declares the DependencyProperty
                new PropertyMetadata(0, DependencyProperty_Changed) // This is the default value of the DependencyProperty and the callback when the value changes
                {
                    CallPropertyChangedWhenLoadedIntoVisualTree = WhenToCallPropertyChangedEnum.Never
                }); 

        private static void DependencyProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show("The DependencyProperty value has changed!");
        }

        public void Decrementation_Click(object sender, RoutedEventArgs e)
        {
            MySampleDependencyProperty--;
        }

        public void Incrementation_Click(object sender, RoutedEventArgs e)
        {
            MySampleDependencyProperty++;
        }
    }
}
