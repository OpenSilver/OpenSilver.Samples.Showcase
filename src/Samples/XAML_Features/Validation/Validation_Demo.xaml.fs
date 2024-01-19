namespace OpenSilver.Samples.Showcase

open OpenSilver.Extensions.FileSystem
open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Data
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

type Validation_Demo() as this =
    inherit Validation_DemoXaml()

    do
        this.InitializeComponent()
        this.ValidationBorder.DataContext <- Person()

    member private this.ValidationBorder_BindingValidationError(sender : obj, e : ValidationErrorEventArgs) =
        // Disable the "OK" button if there are validation errors. This method is called when validation
        // errors appear or disappear.
        let hasErrorName = Validation.GetHasError(this.NameTextBoxForValidation)
        let hasErrorAge = Validation.GetHasError(this.AgeTextBoxForValidation)
        this.ButtonOK.IsEnabled <- not (hasErrorName || hasErrorAge)

    member private this.ButtonOK_Click(sender : obj, e : RoutedEventArgs) =
        // Force refresh validation of the "Name" field because the initial value of "Name" in the
        // ViewModel is String.Empty, and the validation process only happens when the user types
        // text inside the TextBox (note: this is the same behavior as in WPF and Silverlight):
        let binding = this.NameTextBoxForValidation.GetBindingExpression(TextBox.TextProperty)
        binding.UpdateSource()

        // If there are no validation errors, display a message:
        if not (Validation.GetHasError(this.NameTextBoxForValidation) || Validation.GetHasError(this.AgeTextBoxForValidation)) then
            let person = (sender :?> Button).DataContext :?> Person
            let str = sprintf "Name: \"%s\"\nAge: %d." person.Name person.Age
            MessageBox.Show(str) |> ignore
        else
            MessageBox.Show("Please fix the validation errors.") |> ignore

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Validation_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Validation/Validation_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Validation_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Validation/Validation_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "Person.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Validation/Person.cs");
            ViewSourceButtonInfo(TabHeader = "Validation_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Validation/Validation_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "Person.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Validation/Person.vb");
            ViewSourceButtonInfo(TabHeader = "Validation_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Validation/Validation_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "Person.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Validation/Person.fs")
        ])
