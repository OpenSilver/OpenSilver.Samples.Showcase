namespace OpenSilver.Samples.Showcase

open System.Collections.ObjectModel
open System.Windows.Media

type PageCategoryInfo() =
    interface IMenuElement
    member val Name = "" with get, set
    member val Foreground : Brush = null with get, set
    member val Pages = new ObservableCollection<PageInfo>() with get, set
