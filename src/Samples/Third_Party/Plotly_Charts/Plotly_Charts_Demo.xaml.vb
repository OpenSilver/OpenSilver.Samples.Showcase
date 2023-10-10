Imports OpenSilver.Extensions.Plotly
Imports System
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
    Partial Public Class Plotly_Charts_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            AddHandler Loaded, AddressOf Plotly_Charts_Demo_Loaded
        End Sub

        Private Async Sub Plotly_Charts_Demo_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            '------------
            ' Call the ChartControl.Initialize method to ensure that the Plotly JavaScript library has been loaded:
            '------------
            Me.LoadingPleaseWaitMessage.Visibility = Visibility.Visible
            Await ChartControl.Initialize()
            Me.LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed

            '------------
            ' BAR CHART (STACKED)
            '------------
            Me.ChartControl1.Layout = New Layout() With {
    .Title = "Bar Chart",
    .BarMode = BarMode.Stack,
    .Width = 320,
    .Height = 320
}
            Me.ChartControl1.Data = New Data() With {
                    .Traces = New List(Of Trace)() From {
                            New Trace() With {
            .Name = "Test",
            .X = New List(Of Object)() From {
                "giraffes",
                "orangutans",
                "monkeys"
            },
            .Y = New List(Of Object)() From {
                20,
                14,
                23
            },
            .Type = TraceType.Bar
        },
                            New Trace() With {
            .Name = "Test2",
            .X = New List(Of Object)() From {
                "giraffes",
                "orangutans",
                "monkeys"
            },
            .Y = New List(Of Object)() From {
                12,
                16,
                23
            },
            .Type = TraceType.Bar
        }
    }
}
            Me.ChartControl1.Render()

            '------------
            ' SCATTER CHART
            '------------
            Me.ChartControl2.Layout = New Layout() With {
    .Title = "Scatter Chart",
    .Width = 320,
    .Height = 320
}
            Me.ChartControl2.Data = New Data() With {
                    .Traces = New List(Of Trace)() From {
                            New Trace() With {
            .X = New List(Of Object)() From {
                1,
                2,
                3,
                4
            },
            .Y = New List(Of Object)() From {
                10,
                15,
                13,
                17
            },
            .Type = TraceType.Scatter
        },
                            New Trace() With {
            .X = New List(Of Object)() From {
                2,
                3,
                4,
                5
            },
            .Y = New List(Of Object)() From {
                16,
                5,
                11,
                9
            },
            .Type = TraceType.Scatter,
            .Mode = TraceMode.Markers Or TraceMode.Text,
            .Text = New List(Of String)() From {
                "B-a",
                "B-b",
                "B-c",
                "B-d",
                "B-e"
            },
                                    .Marker = New Marker() With {
                .Size = 14
            }
        },
                            New Trace() With {
            .X = New List(Of Object)() From {
                1,
                2,
                3,
                4
            },
            .Y = New List(Of Object)() From {
                12,
                9,
                15,
                12
            },
            .Type = TraceType.Scatter,
            .Mode = TraceMode.Lines Or TraceMode.Markers
        }
    }
}
            Me.ChartControl2.Render()

            '------------
            ' BASIC PIE CHART
            '------------
            Me.ChartControl3.Layout = New Layout() With {
    .Title = "Pie Chart",
    .Width = 320,
    .Height = 320
}
            Me.ChartControl3.Data = New Data() With {
                    .Traces = New List(Of Trace)() From {
                            New Trace() With {
            .Values = New List(Of Object)() From {
                19,
                26,
                55
            },
            .Labels = New List(Of String)() From {
                "Item 1",
                "Item 2",
                "Item 3"
            },
            .Type = TraceType.Pie
        }
    }
}
            Me.ChartControl3.Render()

            '------------
            ' WATERFALL BAR
            '------------
            Me.ChartControl4.Layout = New Layout() With {
    .Title = "Waterfall Bar [Profit 2015]",
    .BarMode = BarMode.Stack,
    .PlotBgColor = "rgba(245, 246, 249, 1)",
    .ShowLegend = False,
    .Annotations = New List(Of Annotation)(),
    .Width = 320,
    .Height = 320
}

            Dim XData = New List(Of Object)() From {
                "Total" & Microsoft.VisualBasic.Constants.vbLf & "revenue",
                "Fixed" & Microsoft.VisualBasic.Constants.vbLf & "costs",
                "Variable" & Microsoft.VisualBasic.Constants.vbLf & "costs",
                "Total costs",
                "Total"
            }
            Dim YData = New List(Of Object)() From {
                500,
                590,
                400,
                400,
                340
            }
            Dim TextList = New List(Of String)() From {
                "$690K",
                "$-120K",
                "$-200K",
                "$-320K",
                "$370K"
            }

            Me.ChartControl4.Data = New Data() With {
                    .Traces = New List(Of Trace)() From {
                            New Trace() With {
            .X = XData,
            .Y = New List(Of Object) From {
                0,
                570,
                370,
                370,
                0
            },
                                    .Marker = New Marker() With {
                .Color = "rgba(1, 1, 1, 0.0)"
            },
            .Type = TraceType.Bar
        },
                            New Trace() With {
            .X = XData,
            .Y = New List(Of Object)() From {
                690,
                0,
                0,
                0,
                0
            },
            .Type = TraceType.Bar,
                                    .Marker = New Marker() With {
                .Color = "rgba(55, 128, 191, 0.7)",
                                            .Line = New Line() With {
                    .Color = "rgba(55, 128, 191, 1.0)",
                    .Width = 2
                }
            }
        },
                            New Trace() With {
            .X = XData,
            .Y = New List(Of Object)() From {
                0,
                120,
                200,
                320,
                0
            },
            .Type = TraceType.Bar,
                                    .Marker = New Marker() With {
                .Color = "rgba(219, 64, 82, 0.7)",
                                            .Line = New Line() With {
                    .Color = "rgba(219, 64, 82, 1.0)",
                    .Width = 2
                }
            }
        },
                            New Trace() With {
            .X = XData,
            .Y = New List(Of Object)() From {
                0,
                0,
                0,
                0,
                370
            },
            .Type = TraceType.Bar,
                                    .Marker = New Marker() With {
                .Color = "rgba(50, 171, 96, 0.7)",
                                            .Line = New Line() With {
                    .Color = "rgba(50, 171, 96, 1.0)",
                    .Width = 2
                }
            }
        }
    }
}
            For i = 0 To 4
                Dim result = New Annotation() With {
    .X = XData(i),
    .Y = YData(i),
    .Text = TextList(i),
                        .Font = New Font() With {
        .Family = "Arial",
        .Size = 10,
        .Color = "rgba(245, 246, 249, 1)"
    },
    .ShowArrow = False
}
                Me.ChartControl4.Layout.Annotations.Add(result)
            Next
            Me.ChartControl4.Render()

            '------------
            ' BAR CHART WITH RELATIVE BARMODE
            '------------
            Me.ChartControl5.Layout = New Layout() With {
    .BarMode = BarMode.Relative,
    .Xaxis = New Axis() With {
        .Title = "X axis"
    },
    .Yaxis = New Axis() With {
        .Title = "Y axis"
    },
    .Title = "Relative Barmode",
    .Width = 300,
    .Height = 300
}
            Me.ChartControl5.Data = New Data() With {
                    .Traces = New List(Of Trace)() From {
                            New Trace() With {
            .X = New List(Of Object)() From {
                1,
                2,
                3,
                4
            },
            .Y = New List(Of Object)() From {
                1,
                4,
                9,
                16
            },
            .Name = "Trace1",
            .Type = TraceType.Bar
        },
                            New Trace() With {
            .X = New List(Of Object)() From {
                1,
                2,
                3,
                4
            },
            .Y = New List(Of Object)() From {
                6,
                -8,
                -4.5,
                8
            },
            .Name = "Trace2",
            .Type = TraceType.Bar
        },
                            New Trace() With {
            .X = New List(Of Object)() From {
                1,
                2,
                3,
                4
            },
            .Y = New List(Of Object)() From {
                -15,
                -3,
                4.5,
                -8
            },
            .Name = "Trace3",
            .Type = TraceType.Bar
        },
                            New Trace() With {
            .X = New List(Of Object)() From {
                1,
                2,
                3,
                4
            },
            .Y = New List(Of Object)() From {
                -1,
                3,
                -3,
                -4
            },
            .Name = "Trace4",
            .Type = TraceType.Bar
        }
    }
}
            Me.ChartControl5.Render()

            '------------
            ' DONUT CHART
            '------------
            Me.ChartControl6.Layout = New Layout() With {
    .Title = "Donut Chart [1990-2011]",
                    .Annotations = New List(Of Annotation)() From {
                            New Annotation() With {
            .Font = New Font() With {
                .Size = 16
            },
            .ShowArrow = False,
            .Text = "CO2",
            .X = 0.5,
            .Y = 0.5
        }
    },
    .Width = 320,
    .Height = 320
}
            Me.ChartControl6.Data = New Data() With {
                    .Traces = New List(Of Trace)() From {
                            New Trace() With {
            .Values = New List(Of Object)() From {
                27,
                11,
                25,
                8,
                3,
                25
            },
            .Labels = New List(Of String)() From {
                "US",
                "China",
                "EU",
                "Russia",
                "India",
                "Other"
            },
            .Text = "CO2",
            .TextPosition = "inside",
                                    .Domain = New Domain() With {
                .X = New List(Of Double)() From {
                    0,
                    1
                }
            },
            .Name = "CO2 Emissions",
            .HoverInfo = "label+percent+name",
            .Hole = 0.4,
            .Type = TraceType.Pie
        }
    }
}
            Me.ChartControl6.Render()
        End Sub

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Plotly_Charts_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Third_Party/Plotly_Charts/Plotly_Charts_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Plotly_Charts_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Third_Party/Plotly_Charts/Plotly_Charts_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "Plotly_Charts_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Third_Party/Plotly_Charts/Plotly_Charts_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace
