namespace OpenSilver.Samples.Showcase

open System
open System.Windows.Data
open System.Globalization

type KilometersToMilesConverter() =
    interface IValueConverter with
        member this.Convert(value, targetType, parameter, culture) =
            let mutable result: obj = ""
            match value with
            | null -> result
            | _ ->
                let mutable kilometers = 0
                if Int32.TryParse(value.ToString(), &kilometers) then
                    result <- (int)((float)kilometers * 0.62137119)
                result

        member this.ConvertBack(value, targetType, parameter, culture) =
            let mutable result: obj = ""
            match value with
            | null -> result
            | _ ->
                let mutable miles = 0
                if Int32.TryParse(value.ToString(), &miles) then
                    result <- (int)((float)miles * 1.6093440)
                result
