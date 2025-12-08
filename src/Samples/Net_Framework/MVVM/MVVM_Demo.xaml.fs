namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("model", "view", "viewmodel", "datagrid", "dataform", "master-details", "binding", "command", "collection", "validation")>]
type MVVM_Demo() as this =
    inherit MVVM_DemoXaml()

    do
        this.InitializeComponent()
