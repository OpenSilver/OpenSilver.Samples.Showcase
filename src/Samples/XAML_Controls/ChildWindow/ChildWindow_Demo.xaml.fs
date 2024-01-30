namespace OpenSilver.Samples.Showcase

open PreviewOnWinRT
open System
open System.Windows
open System.Windows.Controls

type ChildWindow_Demo() as this =
    inherit ChildWindow_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonTestChildWindow_Click(sender : obj, e : RoutedEventArgs) =
        let loginWnd = LoginWindow()
        loginWnd.Closed.Add(fun args -> this.loginWnd_Closed(loginWnd, args))
        loginWnd.Show()

    member private this.loginWnd_Closed(sender : obj, e : EventArgs) =
        let lw = sender :?> LoginWindow
        if lw.DialogResult.HasValue && lw.DialogResult.Value = true && lw.NameBox.Text <> String.Empty then
            this.TextBlockForTestingChildWindow.Text <- "Hello " + lw.NameBox.Text
        else
            this.TextBlockForTestingChildWindow.Text <- "Login cancelled."
