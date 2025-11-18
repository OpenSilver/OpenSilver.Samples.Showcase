namespace MaterialDesign_Styles_Kit

open System
open System.Windows
open System.Windows.Data
open System.Globalization

type DoubleToCornerRadiusConverter() =
    interface IValueConverter with
        member this.Convert(value, targetType, parameter, language) =
            match value with
            | :? double as d -> CornerRadius(d) :> obj
            | _ -> value

        member this.ConvertBack(value, targetType, parameter, language) =
            raise (NotImplementedException() :> exn)
