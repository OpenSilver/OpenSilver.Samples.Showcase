Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("input", "selection")>
    Partial Public Class TimePicker_Demo
        Inherits ContentControl

        Public Sub New()
            InitializeComponent()

            popupModes.ItemsSource = New Object() {
                New PopupMode With {
                    .Name = "List",
                    .Popup = timePicker.Popup
                },
                New PopupMode With {
                    .Name = "Range",
                    .Popup = New RangeTimePickerPopup()
                }
            }
        End Sub

        Private Class PopupMode
            Public Property Name As String
            Public Property Popup As TimePickerPopup
        End Class
    End Class

End Namespace
