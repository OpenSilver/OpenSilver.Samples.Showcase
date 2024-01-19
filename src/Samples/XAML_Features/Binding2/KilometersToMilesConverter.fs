namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Linq
open System.Text
open System.Threading.Tasks
#if SLMIGRATION
open System.Windows
open System.Windows.Data
open System.Globalization
#else
open Windows.UI.Xaml.Data
#endif

type KilometersToMilesConverter() =
    interface IValueConverter with
#if SLMIGRATION
        member this.Convert(value, targetType, parameter, culture) =
#else
        member this.Convert(value, targetType, parameter, language : obj) =
#endif
            let mutable result: obj = ""
            match value with
            | null -> result
            | _ ->
                let mutable kilometers = 0
                if Int32.TryParse(value.ToString(), &kilometers) then
                    result <- (int)((float)kilometers * 0.62137119)
                result

#if SLMIGRATION
        member this.ConvertBack(value, targetType, parameter, culture) =
#else
        member this.ConvertBack(value, targetType, parameter, language : obj) =
#endif
            let mutable result: obj = ""
            match value with
            | null -> result
            | _ ->
                let mutable miles = 0
                if Int32.TryParse(value.ToString(), &miles) then
                    result <- (int)((float)miles * 1.6093440)
                result
