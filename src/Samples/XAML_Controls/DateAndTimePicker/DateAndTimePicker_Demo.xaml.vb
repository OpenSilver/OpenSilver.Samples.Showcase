Imports System.Collections.Generic
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
#Else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#End If

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class DateAndTimePicker_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub RegisterEvent_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Me.TimePicker.Value IsNot Nothing AndAlso Me.DatePicker.SelectedDate IsNot Nothing Then
                Call MessageBox.Show("Your event is confirmed at " & Me.TimePicker.Value.Value.ToShortTimeString() & " on " & Me.DatePicker.SelectedDate.Value.ToShortDateString())
            Else
                MessageBox.Show("Please enter a Date and a Time")
            End If
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DateAndTimePicker_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DateAndTimePicker/DateAndTimePicker_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DateAndTimePickerd_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DateAndTimePicker/DateAndTimePicker_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "DateAndTimePickerd_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/DateAndTimePicker/DateAndTimePicker_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace
