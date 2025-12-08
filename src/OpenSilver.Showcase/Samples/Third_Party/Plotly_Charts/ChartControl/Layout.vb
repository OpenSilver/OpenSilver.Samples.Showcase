Namespace Global.OpenSilver.Extensions.Plotly
    Public Class Layout
        Public Sub New()
            ' Set default values:
            BarMode = BarMode.Group
            ShowLegend = True
            BarGap = 0.0
            Width = 600
            Height = 600
        End Sub

        Public Property Title As String

        Public Property BarMode As BarMode

        Public Property Xaxis As Axis

        Public Property Yaxis As Axis

        Public Property Annotations As List(Of Annotation)

        Public Property ShowLegend As Boolean

        Public Property Font As Font

        Public Property BarGap As Double 'Not used?

        Public Property Legend As Legend

        Public Property BarGroupGap As Double

        Public Property PaperBgColor As String

        Public Property PlotBgColor As String

        Public Property Width As Integer

        Public Property Height As Integer

        Public Function ToJavaScriptObject() As Object
            Dim jsLayout = Interop.ExecuteJavaScript("new Object()")

            Interop.ExecuteJavaScript("
                    $0['barmode'] = $1;
                    ", jsLayout, BarMode.ToString().ToLower())

            If Not String.IsNullOrEmpty(Title) Then
                Interop.ExecuteJavaScript("$0['title'] = $1;", jsLayout, Title)
            End If

            If Xaxis IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['xaxis'] = $1;", jsLayout, Xaxis.ToJavascriptObject())
            End If

            If Yaxis IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['yaxis'] = $1;", jsLayout, Yaxis.ToJavascriptObject())

            End If

            If Annotations IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['annotations'] = [];", jsLayout)

                For Each Annotation In Annotations
                    Interop.ExecuteJavaScript("$0['annotations'].push($1);", jsLayout, Annotation.ToJavaScriptObject())
                Next
            End If

            If Font IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['font'] = $1;", jsLayout, Font.ToJavaScriptObject())
            End If

            Interop.ExecuteJavaScript("$0['showlegend'] = $1;", jsLayout, Unbox(ShowLegend))

            If Legend IsNot Nothing Then
                Interop.ExecuteJavaScript("$0['legend'] = $1;", jsLayout, Legend.ToJavaScriptObject())
            End If

            Interop.ExecuteJavaScript("$0['bargroupgap'] = $1;", jsLayout, Unbox(BarGroupGap))

            If Not String.IsNullOrEmpty(PaperBgColor) Then
                Interop.ExecuteJavaScript("$0['paper_bgcolor'] = $1;", jsLayout, PaperBgColor)
            End If

            If Not String.IsNullOrEmpty(PlotBgColor) Then
                Interop.ExecuteJavaScript("$0['plot_bgcolor'] = $1;", jsLayout, PlotBgColor)
            End If

            Interop.ExecuteJavaScript("$0['width'] = $1", jsLayout, Unbox(Width))
            Interop.ExecuteJavaScript("$0['height'] = $1", jsLayout, Unbox(Height))

            Return jsLayout
        End Function
    End Class
End Namespace
