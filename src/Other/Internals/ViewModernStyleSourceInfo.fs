namespace OpenSilver.Samples.Showcase

type ViewModernStyleSourceInfo() =
    inherit ViewSourceButtonInfo()
    do
        base.Repository <- "OpenSilver.Themes.Modern"
        base.Branch <- "master"
        base.RelativePath <- "src/OpenSilver.Themes.Modern/OpenSilver.Themes.Modern/Themes"
        base.FileName <- "OpenSilver.xaml"
