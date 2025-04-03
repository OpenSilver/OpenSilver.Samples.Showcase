namespace OpenSilver.Samples.Showcase

open System
open System.Globalization
open System.Linq
open System.Windows
open System.Windows.Data

type EnumToListConverter() =
    interface IValueConverter with

        member _.Convert(value: obj, targetType: Type, parameter: obj, culture: CultureInfo): obj =
            match parameter with
            | :? Type as t -> EnumToListConverter.GetValues(t)
            | :? DependencyProperty as dp -> EnumToListConverter.GetValues(dp.PropertyType)
            | _ -> null

        member _.ConvertBack(_value: obj, _targetType: Type, _parameter: obj, _culture: CultureInfo): obj =
            raise (NotImplementedException())

    static member private GetValues(enumType: Type) : obj =
        let underlyingType = Nullable.GetUnderlyingType(enumType)
        let enumType = if underlyingType <> null then underlyingType else enumType
        if enumType.IsEnum then
            let values = Enum.GetValues(enumType).Cast<obj>()
            if underlyingType <> null then
                Seq.append [ null :> obj ] values |> Seq.toArray :> obj
            else
                values |> Seq.toArray :> obj
        else
            null
