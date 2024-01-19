namespace MaterialDesign_Styles_Kit

open System
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Data
open System.Globalization
#else
open Windows.UI.Xaml
open Windows.UI.Xaml.Data
#endif

type DoubleToCornerRadiusConverter() =
    interface IValueConverter with
        member this.Convert(value, targetType, parameter, language) =
            match value with
            | :? double as d -> CornerRadius(d) :> obj
            | _ -> value

        member this.ConvertBack(value, targetType, parameter, language) =
            raise (NotImplementedException() :> exn)
