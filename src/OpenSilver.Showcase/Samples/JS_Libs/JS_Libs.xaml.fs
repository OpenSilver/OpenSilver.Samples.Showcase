namespace OpenSilver.Showcase

open System.Windows.Controls

type JS_Libs() as this =
    inherit JS_LibsXaml()
    do this.InitializeComponent()
