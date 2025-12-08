namespace OpenSilver.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Windows
open System.Windows.Controls

type ViewSourcePanel() as this =
    inherit ViewSourcePanelXaml()

    let mutable _sources : IEnumerable<ViewSourceButtonInfo> = null

    do this.InitializeComponent()

    member this.ViewSource(sources : IEnumerable<ViewSourceButtonInfo>) =
        _sources <- sources
        this.cSharpButton.IsChecked <- Nullable(true)

    member private this.UpdateTabs(sources : IEnumerable<ViewSourceButtonInfo>) =
        let selectedIndex = this.TabControl.SelectedIndex
        this.TabControl.Items.Clear()

        for viewSourceButtonInfo in sources do
            let tabItem = new TabItem(
                Header = viewSourceButtonInfo.GetHeader(),
                Content = new ControlToDisplayCodeHostedOnGitHub(FilePathOnGitHub = viewSourceButtonInfo.GetAbsoluteUrl()),
                DataContext = viewSourceButtonInfo
            )
            this.TabControl.Items.Add(tabItem)

        if selectedIndex >= 0 && this.TabControl.Items.Count > selectedIndex then
            this.TabControl.SelectedIndex <- selectedIndex

    member private this.GetCSharpSources() = this.GetSources([| ".vb"; ".fs" |])
    member private this.GetVBNETSources() = this.GetSources([| ".cs"; ".fs" |])
    member private this.GetFSharpSources() = this.GetSources([| ".cs"; ".vb" |])

    member private this.GetSources([<ParamArray>] extensionsToIgnore : string[]) =
        _sources.Where(fun x -> 
            String.IsNullOrEmpty(x.FileName) || 
            not (extensionsToIgnore.Contains(
                match Path.GetExtension(x.FileName) with
                | null -> ""
                | ext -> ext.ToLower()
            ))
        )

    member private this.OnCSharpRadioButtonChecked(sender : obj, e : RoutedEventArgs) =
        this.UpdateTabs(this.GetCSharpSources())

    member private this.OnVBNETRadioButtonChecked(sender : obj, e : RoutedEventArgs) =
        this.UpdateTabs(this.GetVBNETSources())

    member private this.OnFSharpRadioButtonChecked(sender : obj, e : RoutedEventArgs) =
        this.UpdateTabs(this.GetFSharpSources())
