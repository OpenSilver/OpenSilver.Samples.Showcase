namespace PreviewOnWinRT

open System
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
#endif

type LoginWindow() as this =
    inherit LoginWindowXaml()
    
    do
        this.InitializeComponent()

    member private this.OKButton_Click(sender : obj, e : RoutedEventArgs) =
        this.DialogResult <- true

    member private this.CancelButton_Click(sender : obj, e : RoutedEventArgs) =
        this.DialogResult <- false

    member private this.LoginWindow_Closing(sender : obj, e : System.ComponentModel.CancelEventArgs) =
        if this.DialogResult.HasValue && this.DialogResult.Value = true && (String.IsNullOrEmpty this.nameBox.Text || String.IsNullOrEmpty this.passwordBox.Password) then
            e.Cancel <- true
            let cw = new ChildWindow(Content = "Please Enter your name and password or click Cancel")
            cw.Show()

    // Properties
    member this.NameBox = this.nameBox
    member this.PasswordBox = this.passwordBox
