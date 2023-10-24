Imports System.Windows.Controls

Namespace Global.OpenSilver.Extensions.Plotly
    '------------------------------------------------------------------------
    ' INTRODUCTION:
    '
    ' This is a C#-based wrapper around the JavaScript library "Plotly.js".
    ' With such a "wrapper", it is possible to use the JavaScript library directly from C#,
    ' as if it was a C# library.
    '
    ' Documentation of this concept of "wrapper" can be found at:
    ' http://opensilver.net/links/how-to-create-extensions.aspx
    ' and
    ' http://opensilver.net/links/how-to-call-javascript.aspx
    '
    ' WHERE CAN I FIND THE DOCUMENTATION OF THE PLOTLY LIBRARY?
    '
    ' Documentation of the JavaScript Plotly library can be found at:
    ' - Plotly API Reference: https://plot.ly/javascript/reference/
    ' - Plotly JS samples: https://plot.ly/javascript/#basic-charts 
    '------------------------------------------------------------------------


    Public Class ChartControl
        Inherits Canvas
        Private Shared JSLibraryWasLoaded As Boolean ' Remembers whether the JS library was loaded in order not to load it twice:

        Public Sub New()
            ' Set default values:
            Data = New Data()
            Layout = New Layout()
        End Sub

        ''' <summary>
        ''' Loads the "Plotly" JavaScript library if it is not already loaded.
        ''' THIS METHOD MUST BE CALLED BEFORE "RENDER"
        ''' </summary>
        Public Shared Async Function Initialize() As Task
            If Not JSLibraryWasLoaded Then
                ' Load the "typedarray.js" polyfill for IE compatibility:
                Await Interop.LoadJavaScriptFile("ms-appx:///OpenSilver.Samples.Showcase/Samples/Third_Party/Plotly_Charts/ChartControl/typedarray.js")

                ' Load the "plotly.js" library:
                Await Interop.LoadJavaScriptFile("ms-appx:///OpenSilver.Samples.Showcase/Samples/Third_Party/Plotly_Charts/ChartControl/plotly.min.js")

                ' Remember that the libraries have been loaded in order to not load them again:
                JSLibraryWasLoaded = True
            End If
        End Function

        ''' <summary>
        ''' Renders the chart.
        ''' </summary>
        Public Sub Render()
            ' Get a reference to the HTML DOM representation of the control (must be in the Visual Tree):
            Dim div = Interop.GetDiv(Me)

            ' Make sure that the Div has an ID, because "Plotly" requires an ID:
            Interop.ExecuteJavaScript("if (!$0.id) { $0.id = $1 }", div, Guid.NewGuid().ToString())

            ' Get the data, layout, and other JS objects:
            Dim jsData As Object = Data.ToJavaScriptObject()
            Dim jsLayout As Object = Layout.ToJavaScriptObject()

            ' Render the control:
            Interop.ExecuteJavaScript("
                Plotly.newPlot($0.id, $1, $2);
                ", div, jsData, jsLayout)
        End Sub

        Public Property Data As Data

        Public Property Layout As Layout
    End Class
End Namespace
