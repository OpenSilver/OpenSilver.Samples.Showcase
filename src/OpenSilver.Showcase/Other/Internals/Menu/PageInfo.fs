namespace OpenSilver.Showcase

open System.Windows.Media

type PageInfo() =
    interface IMenuElement
    member val Name: string = null with get, set
    member val Path: string = null with get, set
    member val Icon: string = null with get, set
    member val IconBrush: Brush = null with get, set
    member val IsVisibleInMenu: bool = true with get, set
