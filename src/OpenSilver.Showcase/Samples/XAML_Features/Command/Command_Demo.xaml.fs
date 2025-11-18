namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("interaction", "action", "behavior", "events", "binding", "mvvm", "icommand", "relaycommand")>]
type Command_Demo() as this =
    inherit Command_DemoXaml()

    do this.InitializeComponent()
