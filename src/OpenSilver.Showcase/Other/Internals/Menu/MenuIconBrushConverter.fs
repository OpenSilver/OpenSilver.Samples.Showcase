namespace OpenSilver.Showcase

open System
open System.Globalization
open System.Windows
open System.Windows.Controls
open System.Windows.Data
open System.Windows.Media

type MenuIconBrushConverter() =
    interface IMultiValueConverter with
        member _.Convert(values: obj[], targetType: Type, parameter: obj, culture: CultureInfo) : obj =
            let tvi = values.[0] :?> TreeViewItem // ancestor container
            let isSelected =
                match values.[1] with
                | :? bool as b -> Nullable(b)
                | :? Nullable<bool> as nb -> nb
                | _ -> Nullable()

            if isNull tvi then
                DependencyProperty.UnsetValue
            elif isSelected.HasValue && isSelected.Value then
                match tvi.TryFindResource("Theme_TextOnPrimaryBrush") with
                | :? Brush as brush -> brush :> obj
                | _ -> Brushes.White :> obj
            else
                match tvi.DataContext with
                | :? PageInfo as pi when not (isNull pi.IconBrush) -> pi.IconBrush :> obj
                | _ -> DependencyProperty.UnsetValue

        member _.ConvertBack(value: obj, targetTypes: Type[], parameter: obj, culture: CultureInfo) : obj[] =
            raise (NotSupportedException())
