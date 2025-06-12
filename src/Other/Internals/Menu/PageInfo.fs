namespace OpenSilver.Samples.Showcase

type PageInfo() =
    interface IMenuElement
    member val Name = "" with get, set
    member val Path = "" with get, set
    member val IsVisibleInMenu = false with get, set
