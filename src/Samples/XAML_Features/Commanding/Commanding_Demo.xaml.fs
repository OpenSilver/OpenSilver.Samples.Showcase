namespace OpenSilver.Samples.Showcase

open System
open System.Windows.Input
open System.Windows
open System.Windows.Controls

type TestICommandClass() =
    let canExecuteChanged = new Event<EventHandler, EventArgs>()

    interface ICommand with
        member this.CanExecute parameter = true

        member this.Execute parameter =
            MessageBox.Show("Yay!") |> ignore

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

type TestICommandClass2() =
    let canExecuteChanged = new Event<EventHandler, EventArgs>()

    interface ICommand with
        member this.CanExecute parameter =
            match parameter with
            | :? TextBox -> true
            | _ -> false

        member this.Execute parameter =
            match parameter with
            | :? TextBox as textBox -> textBox.Text <- "Boo!"
            | _ -> ()

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

type TestICommandClass3() =
    let canExecuteChanged = new Event<EventHandler, EventArgs>()

    interface ICommand with
        member this.CanExecute parameter = true

        member this.Execute parameter =
            MessageBox.Show("Wow!") |> ignore

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish


type TestCommandInTextBlock(messageTextTextBlock: TextBlock) =
    let mutable _messageTextTextBlock = messageTextTextBlock
    let canExecuteChanged = new Event<EventHandler, EventArgs>()

    interface ICommand with
        member this.CanExecute parameter =
            match parameter with
            | :? string -> true
            | _ -> false

        member this.Execute parameter =
            match parameter with
            | :? string as str -> _messageTextTextBlock.Text <- str
            | _ -> ()

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

type TestCommandInMessageBox() =
    let canExecuteChanged = new Event<EventHandler, EventArgs>()

    interface ICommand with
        member this.CanExecute parameter =
            match parameter with
            | :? string -> true
            | _ -> false

        member this.Execute parameter =
            match parameter with
            | :? string as str -> MessageBox.Show(str) |> ignore
            | _ -> ()

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

type Commanding_Demo() as this =
    inherit Commanding_DemoXaml()

    let mutable _myICommand : ICommand = null

    let PrepareICommandTest () =
        let items = ["MessageBox Yay!"; "TextBox Boo!"; "MessageBox Wow!"]
        this.MyComboBoxForICommand.ItemsSource <- items
        this.MyComboBoxForICommand.SelectedIndex <- 0

        let itemsForCommandTest = ["Display in TextBlock"; "Display in MessageBox"]
        this.MyComboBoxForCommandTest.ItemsSource <- itemsForCommandTest
        this.MyComboBoxForCommandTest.SelectedIndex <- 0
        this.MyButtonForTestCommand.Command <- TestCommandInTextBlock(this.MessageTextBlock)

    do
        this.InitializeComponent()

        PrepareICommandTest()

    member this.ButtonTestICommand_Click(sender : obj, e : RoutedEventArgs) =
        if _myICommand <> null && _myICommand.CanExecute(this.MessageTextTextBox) then
            _myICommand.Execute(this.MessageTextTextBox)

    member this.ComboBoxForCommandTest_SelectionChanged(sender : obj, e : SelectionChangedEventArgs) =
        let comboBox = sender :?> ComboBox
        match comboBox.SelectedIndex with
        | 0 -> this.MyButtonForTestCommand.Command <- TestCommandInTextBlock(this.MessageTextBlock)
        | _ -> this.MyButtonForTestCommand.Command <- TestCommandInMessageBox()

    member this.MyComboBoxForICommand_SelectionChanged(sender : obj, e : SelectionChangedEventArgs) =
        let comboBox = sender :?> ComboBox
        _myICommand <-
            match comboBox.SelectedIndex with
            | 0 -> TestICommandClass() :> ICommand
            | 1 -> TestICommandClass2() :> ICommand
            | 2 -> TestICommandClass3() :> ICommand
            | _ -> TestICommandClass() :> ICommand
