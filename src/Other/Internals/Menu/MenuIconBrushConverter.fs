namespace OpenSilver.Samples.Showcase

open System
open System.Globalization
open System.Windows
open System.Windows.Controls
open System.Windows.Data
open System.Windows.Media

type MenuIconBrushConverter() =
    interface IMultiValueConverter with
        member this.Convert(values: obj[], targetType: Type, parameter: obj, culture: CultureInfo) : obj =
            let tvi = values.[0] :?> TreeViewItem
            let isSelected = values.[1] :?> Nullable<bool>

            //if isNull tvi then
            //    DependencyProperty.UnsetValue :> obj
            //elif isSelected.HasValue && isSelected.Value then
            //    let brush = tvi.TryFindResource("Theme_TextOnPrimaryBrush") :?> Brush
            //    if isNull brush then Brushes.White :> obj else brush :> obj
            //else
            //    let pageInfo = tvi.DataContext :?> PageInfo
            //    if isNull pageInfo || isNull (pageInfo.IconBrush) then
            //        DependencyProperty.UnsetValue :> obj
            //    else
            //        pageInfo.IconBrush :> obj
            DependencyProperty.UnsetValue

        member this.ConvertBack(value: obj, targetTypes: Type[], parameter: obj, culture: CultureInfo) : obj[] =
            raise (NotSupportedException())
