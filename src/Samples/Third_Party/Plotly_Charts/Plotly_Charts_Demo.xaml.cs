using OpenSilver.Extensions.Plotly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

namespace OpenSilver.Samples.Showcase
{
    public partial class Plotly_Charts_Demo : UserControl
    {
        public Plotly_Charts_Demo()
        {
            this.InitializeComponent();

            this.Loaded += Plotly_Charts_Demo_Loaded;
        }

        private async void Plotly_Charts_Demo_Loaded(object sender, RoutedEventArgs e)
        {
            //------------
            // Call the ChartControl.Initialize method to ensure that the Plotly JavaScript library has been loaded:
            //------------
            LoadingPleaseWaitMessage.Visibility = Visibility.Visible;
            await ChartControl.Initialize();
            LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed;

            //------------
            // BAR CHART (STACKED)
            //------------
            ChartControl1.Layout = new Layout()
            {
                Title = "Bar Chart",
                BarMode = BarMode.Stack,
                Width = 320,
                Height = 320
            };
            ChartControl1.Data = new Data()
            {
                Traces = new List<Trace>()
                {
                    new Trace()
                    {
                        Name = "Test",
                        X = new List<object>() { "giraffes", "orangutans", "monkeys" },
                        Y = new List<object>() { 20, 14, 23 },
                        Type = TraceType.Bar
                    },
                    new Trace()
                    {
                        Name = "Test2",
                        X = new List<object>() { "giraffes", "orangutans", "monkeys" },
                        Y = new List<object>() { 12, 16, 23 },
                        Type = TraceType.Bar
                    }
                }
            };
            ChartControl1.Render();

            //------------
            // SCATTER CHART
            //------------
            ChartControl2.Layout = new Layout()
            {
                Title = "Scatter Chart",
                Width = 320,
                Height = 320
            };
            ChartControl2.Data = new Data()
            {
                Traces = new List<Trace>()
                {
                    new Trace()
                    {
                        X = new List<object>() { 1, 2, 3, 4 },
                        Y = new List<object>() { 10, 15, 13, 17 },
                        Type = TraceType.Scatter,
                    },
                    new Trace()
                    {
                        X = new List<object>() { 2, 3, 4, 5 },
                        Y = new List<object>() { 16, 5, 11, 9 },
                        Type = TraceType.Scatter,
                        Mode = TraceMode.Markers | TraceMode.Text,
                        Text = new List<string>() { "B-a", "B-b", "B-c", "B-d", "B-e" },
                        Marker = new Marker()
                        {
                            Size = 14
                        }
                    },
                    new Trace()
                    {
                        X = new List<object>() { 1, 2, 3, 4 },
                        Y = new List<object>() { 12, 9, 15, 12 },
                        Type = TraceType.Scatter,
                        Mode = TraceMode.Lines | TraceMode.Markers
                    },
                }
            };
            ChartControl2.Render();

            //------------
            // BASIC PIE CHART
            //------------
            ChartControl3.Layout = new Layout()
            {
                Title = "Pie Chart",
                Width = 320,
                Height = 320
            };
            ChartControl3.Data = new Data()
            {
                Traces = new List<Trace>()
                {
                    new Trace()
                    {
                        Values = new List<object>() { 19, 26, 55 },
                        Labels = new List<string>() { "Item 1", "Item 2", "Item 3" },
                        Type = TraceType.Pie
                    }
                }
            };
            ChartControl3.Render();

            //------------
            // WATERFALL BAR
            //------------
            ChartControl4.Layout = new Layout()
            {
                Title = "Waterfall Bar [Profit 2015]",
                BarMode = BarMode.Stack,
                PlotBgColor = "rgba(245, 246, 249, 1)",
                ShowLegend = false,
                Annotations = new List<Annotation>(),
                Width = 320,
                Height = 320,
            };

            var XData = new List<object>() { "Total\nrevenue", "Fixed\ncosts", "Variable\ncosts", "Total costs", "Total" };
            var YData = new List<object>() { 500, 590, 400, 400, 340 };
            var TextList = new List<string>() { "$690K", "$-120K", "$-200K", "$-320K", "$370K" };

            ChartControl4.Data = new Data()
            {
                Traces = new List<Trace>()
                {
                    new Trace()
                    {
                        X = XData,
                        Y = new List<object> { 0, 570, 370, 370, 0 },
                        Marker = new Marker()
                        {
                            Color = "rgba(1, 1, 1, 0.0)"
                        },
                        Type = TraceType.Bar
                    },
                    new Trace()
                    {
                        X = XData,
                        Y = new List<object>() { 690, 0, 0, 0, 0 },
                        Type = TraceType.Bar,
                        Marker = new Marker()
                        {
                            Color = "rgba(55, 128, 191, 0.7)",
                            Line = new Line()
                            {
                                Color = "rgba(55, 128, 191, 1.0)",
                                Width = 2
                            }
                        }
                    },
                    new Trace()
                    {
                        X = XData,
                        Y = new List<object>() { 0, 120, 200, 320, 0 },
                        Type = TraceType.Bar,
                        Marker = new Marker()
                        {
                            Color = "rgba(219, 64, 82, 0.7)",
                            Line = new Line()
                            {
                                Color = "rgba(219, 64, 82, 1.0)",
                                Width = 2
                            }
                        }
                    },
                    new Trace()
                    {
                        X = XData,
                        Y = new List<object>() { 0, 0, 0, 0, 370 },
                        Type = TraceType.Bar,
                        Marker = new Marker()
                        {
                            Color = "rgba(50, 171, 96, 0.7)",
                            Line = new Line()
                            {
                                Color = "rgba(50, 171, 96, 1.0)",
                                Width = 2
                            }
                        }
                    }
                }
            };
            for (int i = 0; i < 5; i++)
            {
                var result = new Annotation()
                {
                    X = XData[i],
                    Y = YData[i],
                    Text = TextList[i],
                    Font = new Font()
                    {
                        Family = "Arial",
                        Size = 10,
                        Color = "rgba(245, 246, 249, 1)"
                    },
                    ShowArrow = false
                };
                ChartControl4.Layout.Annotations.Add(result);
            }
            ChartControl4.Render();

            //------------
            // BAR CHART WITH RELATIVE BARMODE
            //------------
            ChartControl5.Layout = new Layout()
            {
                BarMode = BarMode.Relative,
                Xaxis = new Axis() { Title = "X axis" },
                Yaxis = new Axis() { Title = "Y axis" },
                Title = "Relative Barmode",
                Width = 300,
                Height = 300
            };
            ChartControl5.Data = new Data()
            {
                Traces = new List<Trace>()
                {
                    new Trace()
                    {
                        X = new List<object>() { 1, 2, 3, 4 },
                        Y = new List<object>() { 1, 4, 9, 16 },
                        Name = "Trace1",
                        Type = TraceType.Bar
                    },
                    new Trace()
                    {
                        X = new List<object>() { 1, 2, 3, 4 },
                        Y = new List<object>() { 6, -8, -4.5, 8 },
                        Name = "Trace2",
                        Type = TraceType.Bar
                    },
                    new Trace()
                    {
                        X = new List<object>() { 1, 2, 3, 4 },
                        Y = new List<object>() { -15, -3, 4.5, -8 },
                        Name = "Trace3",
                        Type = TraceType.Bar
                    },
                    new Trace()
                    {
                        X = new List<object>() { 1, 2, 3, 4 },
                        Y = new List<object>() { -1, 3, -3, -4 },
                        Name = "Trace4",
                        Type = TraceType.Bar
                    }
                }
            };
            ChartControl5.Render();

            //------------
            // DONUT CHART
            //------------
            ChartControl6.Layout = new Layout()
            {
                Title = "Donut Chart [1990-2011]",
                Annotations = new List<Annotation>()
                {
                    new Annotation()
                    {
                        Font = new Font() { Size = 16 },
                        ShowArrow = false,
                        Text = "CO2",
                        X = 0.5,
                        Y = 0.5
                    }
                },
                Width = 320,
                Height = 320
            };
            ChartControl6.Data = new Data()
            {
                Traces = new List<Trace>()
                {
                    new Trace()
                    {
                        Values = new List<object>() { 27, 11, 25, 8, 3, 25 },
                        Labels = new List<string>() { "US", "China", "EU", "Russia", "India", "Other" },
                        Text = "CO2",
                        TextPosition = "inside",
                        Domain = new Domain()
                        {
                            X = new List<double>() { 0, 1 }
                        },
                        Name = "CO2 Emissions",
                        HoverInfo = "label+percent+name",
                        Hole = 0.4,
                        Type = TraceType.Pie
                    }
                }
            };
            ChartControl6.Render();
        }
    }
}
