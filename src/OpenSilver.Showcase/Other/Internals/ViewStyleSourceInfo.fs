namespace OpenSilver.Showcase

type ViewStyleSourceInfo() =
    inherit ViewSourceButtonInfo()
    do
        base.Repository <- "OpenSilver"
        base.Branch <- "master"
        base.RelativePath <- "src/Runtime/Runtime/Themes"
        base.FileName <- "generic.xaml"
