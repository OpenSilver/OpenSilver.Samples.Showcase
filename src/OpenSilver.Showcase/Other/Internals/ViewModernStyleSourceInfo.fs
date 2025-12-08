namespace OpenSilver.Showcase

type ViewModernStyleSourceInfo() =
    inherit ViewSourceButtonInfo()
    do
        base.Repository <- "OpenSilver.Themes.Modern"
        base.Branch <- "master"
        base.RelativePath <- "src/OpenSilver.Themes.Modern/OpenSilver.Themes.Modern/Themes"
        base.FileName <- "OpenSilver.xaml"
        base.Notes <- "To use the style, you need to reference the OpenSilver.Themes.Modern package."
