using CSHTML5.Extensions.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CSHTML5.Samples.Showcase
{
    public partial class Validation_Demo : UserControl
    {
        public Validation_Demo()
        {
            this.InitializeComponent();
            ValidationBorder.DataContext = new Person();
        }

        private void ValidationBorder_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (Validation.GetHasError(NameTextBoxForValidation) || Validation.GetHasError(AgeTextBoxForValidation))
            {
                ButtonOK.IsEnabled = false;
            }
            else
            {
                ButtonOK.IsEnabled = true;
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Person person = (Person)((Button)sender).DataContext;
            string str = "Name: \"" + person.Name + "\"" + Environment.NewLine + "Age: " + person.Age + ".";
            MessageBox.Show(str);
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Validation_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Features/Validation/Validation_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Validation_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Features/Validation/Validation_Demo.xaml.cs"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Person.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/XAML_Features/Validation/Person.cs"
                }
            });
        }

    }
}
