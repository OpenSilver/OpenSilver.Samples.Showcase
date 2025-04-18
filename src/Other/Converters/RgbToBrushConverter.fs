namespace OpenSilver.Samples.Showcase

open System
open System.Globalization
open System.Windows.Data
open System.Windows.Media

/// Converts three RGB values to a SolidColorBrush.
type RgbToBrushConverter() =
    interface IMultiValueConverter with
        member _.Convert(values: obj[], targetType: Type, parameter: obj, culture: CultureInfo) : obj =
            match values with
            | [| :? double as r; :? double as g; :? double as b |] ->
                let color = Color.FromRgb(byte r, byte g, byte b)
                SolidColorBrush(color) :> obj
            | _ ->
                SolidColorBrush(Colors.Black) :> obj

        member _.ConvertBack(value: obj, targetTypes: Type[], parameter: obj, culture: CultureInfo) : obj[] =
            raise (NotImplementedException())
