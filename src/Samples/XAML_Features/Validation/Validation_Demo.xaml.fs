namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls
open System.Windows.Data

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
