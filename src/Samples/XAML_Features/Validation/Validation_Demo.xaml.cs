using OpenSilver.Extensions.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

namespace OpenSilver.Samples.Showcase
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
            // Disable the "OK" button if there are validation errors. This method is called when validation
            // errors appear or disappear.
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
            // Force refresh validation of the "Name" field because the initial value of "Name" in the
            // ViewModel is String.Empty, and the validation process only happens when the user types
            // text inside the TextBox (note: this is the same behavior as in WPF and Silverlight):
            BindingExpression binding = NameTextBoxForValidation.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            // If there are no validation errors, display a message:
            if (!Validation.GetHasError(NameTextBoxForValidation)
                && !Validation.GetHasError(AgeTextBoxForValidation))
            {
                Person person = (Person)((Button)sender).DataContext;
                string str = "Name: \"" + person.Name + "\"" + Environment.NewLine + "Age: " + person.Age + ".";
                MessageBox.Show(str);
            }
            else
            {
                MessageBox.Show("Please fix the validation errors.");
            }
        }
    }
}
