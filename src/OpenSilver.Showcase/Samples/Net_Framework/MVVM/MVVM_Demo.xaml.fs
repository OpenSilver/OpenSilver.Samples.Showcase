namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("model", "view", "viewmodel", "datagrid", "dataform", "master-details", "binding", "command", "collection", "validation")>]
type MVVM_Demo() as this =
    inherit MVVM_DemoXaml()

    do
        this.InitializeComponent()
