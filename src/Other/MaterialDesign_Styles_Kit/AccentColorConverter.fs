namespace MaterialDesign_Styles_Kit

open System
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Data
open System.Windows.Media
open System.Globalization
#else
open Windows.UI
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Media
#endif

type AccentColorConverter() =
    interface IValueConverter with
        member this.Convert(value, targetType, parameter, language) =
            if value :? SolidColorBrush then
                let oldColor = (value :?> SolidColorBrush).Color
                let newR = if oldColor.R > byte(40) then byte (oldColor.R - byte(40)) else byte 0
                let newG = if oldColor.G > byte(40) then byte (oldColor.G - byte(40)) else byte 0
                let newB = if oldColor.B > byte(40) then byte (oldColor.B - byte(40)) else byte 0
                let newColor = Color.FromArgb(oldColor.A, newR, newG, newB)
                new SolidColorBrush(newColor) :> obj
            else
                value

        member this.ConvertBack(value, targetType, parameter, language) =
            raise (NotImplementedException())
