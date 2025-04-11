namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("interaction", "action", "behavior", "events", "binding", "mvvm", "icommand", "relaycommand")>]
type Command_Demo() as this =
    inherit Command_DemoXaml()

    do this.InitializeComponent()
