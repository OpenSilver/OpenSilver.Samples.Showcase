Imports System.Globalization
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("input", "counter", "control", "buttonspinner")>
    Partial Public Class UpDownBase_Demo
        Inherits ContentControl

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Friend Class IntUpDown
        Inherits UpDownBase(Of Integer)

        '''<summary>
        ''' Called by OnSpin when the spin direction is SpinDirection.Increase.
        '''</summary>
        Protected Overrides Sub OnIncrement()
            Value = (Value + 1) Mod 10
        End Sub

        '''<summary>
        ''' Called by OnSpin when the spin direction is SpinDirection.Decrease.
        '''</summary>
        Protected Overrides Sub OnDecrement()
            Value = (Value - 1) Mod 10
        End Sub

        '''<summary>
        ''' Called by ApplyValue to parse user input.
        '''</summary>
        Protected Overrides Function ParseValue(text As String) As Integer
            Return Integer.Parse(text, CultureInfo.CurrentCulture)
        End Function

        '''<summary>
        ''' Called to render Value for Text template part to display.
        '''</summary>
        Protected Overrides Function FormatValue() As String
            Return Value.ToString(CultureInfo.CurrentCulture)
        End Function
    End Class

    Friend Class DateTimeUpDown
        Inherits UpDownBase(Of DateTime)

        '''<summary>
        ''' Called by OnSpin when the spin direction is SpinDirection.Increase.
        '''</summary>
        Protected Overrides Sub OnIncrement()
            Try
                Value = Value.AddHours(1)
            Catch ex As ArgumentOutOfRangeException
                Value = DateTime.MinValue
            End Try
        End Sub

        '''<summary>
        ''' Called by OnSpin when the spin direction is SpinDirection.Decrease.
        '''</summary>
        Protected Overrides Sub OnDecrement()
            Try
                Value = Value.AddHours(-1)
            Catch ex As ArgumentOutOfRangeException
                Value = DateTime.MaxValue
            End Try
        End Sub

        '''<summary>
        ''' Called by ApplyValue to parse user input.
        '''</summary>
        Protected Overrides Function ParseValue(text As String) As DateTime
            Return DateTime.Parse(text, CultureInfo.CurrentCulture)
        End Function

        '''<summary>
        ''' Called to render Value for Text template part to display.
        '''</summary>
        Protected Overrides Function FormatValue() As String
            Return Value.ToShortTimeString()
        End Function
    End Class

    Friend Class StringUpDown
        Inherits UpDownBase(Of String)

        '''<summary>
        ''' Internal constructor.
        '''</summary>
        Friend Sub New()
            Value = "0"
        End Sub

        '''<summary>
        ''' Called by OnSpin when the spin direction is SpinDirection.Increase.
        '''</summary>
        Protected Overrides Sub OnIncrement()
            Dim value As String = If(String.IsNullOrEmpty(value), "0", value)
            If value.Length >= 10 Then
                value = "0"
            Else
                value &= value.Length.ToString(CultureInfo.CurrentCulture)
            End If
            value = value
        End Sub

        '''<summary>
        ''' Called by OnSpin when the spin direction is SpinDirection.Decrease.
        '''</summary>
        Protected Overrides Sub OnDecrement()
            Dim value As String = If(String.IsNullOrEmpty(value), "0", value)
            If value.Length <= 1 Then
                value = "0123456789"
            Else
                value = value.Substring(0, value.Length - 1)
            End If
            value = value
        End Sub

        '''<summary>
        ''' Called by ApplyValue to parse user input.
        '''</summary>
        Protected Overrides Function ParseValue(text As String) As String
            If String.IsNullOrEmpty(text) Then
                text = "0"
            End If

            If text.Length > 10 Then
                text = text.Substring(0, 10)
            End If

            Return text
        End Function

        '''<summary>
        ''' Called to render Value for Text template part to display.
        '''</summary>
        Protected Overrides Function FormatValue() As String
            Return If(String.IsNullOrEmpty(Value), "0", Value)
        End Function
    End Class

End Namespace
