namespace OpenSilver.Showcase

open System
open System.Globalization
open System.Windows
open System.Windows.Data

type StringEmptyToVisibilityConverter() =
    interface IValueConverter with
        member _.Convert(value: obj, targetType: Type, parameter: obj, culture: CultureInfo) : obj =
            match value with
            | :? string as str when not (String.IsNullOrEmpty(str)) -> box Visibility.Collapsed
            | _ -> box Visibility.Visible

        member _.ConvertBack(value: obj, targetType: Type, parameter: obj, culture: CultureInfo) : obj =
            raise (NotImplementedException())
