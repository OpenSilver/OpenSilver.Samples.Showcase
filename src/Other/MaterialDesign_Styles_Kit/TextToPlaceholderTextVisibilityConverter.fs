﻿namespace MaterialDesign_Styles_Kit

open System
open System.Windows
open System.Windows.Data
open System.Globalization

type TextToPlaceholderTextVisibilityConverter() =
    interface IValueConverter with
        member this.Convert(value, targetType, parameter, language) =
            match value with
            | :? string as str ->
                if String.IsNullOrEmpty(str) then Visibility.Visible
                else Visibility.Collapsed
            | _ -> raise <| InvalidOperationException("The IValueConverter expects a value of type String.")

        member this.ConvertBack(value, targetType, parameter, language) =
            raise <| NotImplementedException()
