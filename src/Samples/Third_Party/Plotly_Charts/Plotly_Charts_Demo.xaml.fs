namespace OpenSilver.Samples.Showcase

open OpenSilver.Extensions.Plotly
open System.Windows
open System.Windows.Controls

type Plotly_Charts_Demo() as this =
    inherit Plotly_Charts_DemoXaml()

    do
        this.InitializeComponent()
        this.Loaded.Add(fun args -> this.Plotly_Charts_Demo_Loaded(this, args))

    member private this.Plotly_Charts_Demo_Loaded(sender : obj, e : RoutedEventArgs) =
        //------------
        // Call the ChartControl.Initialize method to ensure that the Plotly JavaScript library has been loaded:
        //------------
        this.LoadingPleaseWaitMessage.Visibility <- Visibility.Visible
        ChartControl.Initialize() |> Async.RunSynchronously
        this.LoadingPleaseWaitMessage.Visibility <- Visibility.Collapsed

        // BAR CHART (STACKED)
        this.ChartControl1.Layout <- Layout(Title = "Bar Chart", BarMode = BarMode.Stack, Width = 320, Height = 320)
        this.ChartControl1.Data <- Data(
            Traces = [
                Trace(
                    Name = "Test",
                    X = [ "giraffes"; "orangutans"; "monkeys" ],
                    Y = [ box 20; box 14; box 23 ],
                    Type = TraceType.Bar
                )
                Trace(
                    Name = "Test2",
                    X = [ "giraffes"; "orangutans"; "monkeys" ],
                    Y = [ box 12; box 16; box 23 ],
                    Type = TraceType.Bar
                )
            ]
        )
        this.ChartControl1.Render()

        // SCATTER CHART
        this.ChartControl2.Layout <- Layout(Title = "Scatter Chart", Width = 320, Height = 320)
        this.ChartControl2.Data <- Data(
            Traces = [
                Trace(X = [1; 2; 3; 4], Y = [10; 15; 13; 17], Type = TraceType.Scatter)
                Trace(X = [2; 3; 4; 5], Y = [16; 5; 11; 9], Type = TraceType.Scatter, Mode = (TraceMode.Markers ||| TraceMode.Text),
                    Text = Some ["B-a"; "B-b"; "B-c"; "B-d"; "B-e"],
                    Marker = Marker(Size = 14))
                Trace(X = [1; 2; 3; 4], Y = [12; 9; 15; 12], Type = TraceType.Scatter, Mode = (TraceMode.Lines ||| TraceMode.Markers))
            ])
        this.ChartControl2.Render()

        // BASIC PIE CHART
        this.ChartControl3.Layout <- Layout(Title = "Pie Chart", Width = 320, Height = 320)
        this.ChartControl3.Data <- Data(
            Traces = [
                Trace(Values = [19; 26; 55], Labels = ["Item 1"; "Item 2"; "Item 3"], Type = TraceType.Pie)
            ])
        this.ChartControl3.Render()

        // WATERFALL BAR
        this.ChartControl4.Layout <- Layout(
            Title = "Waterfall Bar [Profit 2015]",
            BarMode = BarMode.Stack,
            PlotBgColor = "rgba(245, 246, 249, 1)",
            ShowLegend = false,
            Annotations = [],
            Width = 320,
            Height = 320)
        let xData = ["Total\nrevenue" :> obj; "Fixed\ncosts" :> obj; "Variable\ncosts" :> obj; "Total costs" :> obj; "Total" :> obj]
        let yData = [500; 590; 400; 400; 340]
        let textList = ["$690K"; "$-120K"; "$-200K"; "$-320K"; "$370K"]
        this.ChartControl4.Data <- Data(
            Traces = [
                Trace(X = xData, Y = [0; 570; 370; 370; 0], Marker = Marker(Color = Some "rgba(1, 1, 1, 0.0)"), Type = TraceType.Bar)
                Trace(X = xData, Y = [690; 0; 0; 0; 0], Type = TraceType.Bar,
                    Marker = Marker(Color = Some "rgba(55, 128, 191, 0.7)", Line = Line(Color = "rgba(55, 128, 191, 1.0)", Width = 2)))
                Trace(X = xData, Y = [0; 120; 200; 320; 0], Type = TraceType.Bar,
                    Marker = Marker(Color = Some "rgba(219, 64, 82, 0.7)", Line = Line(Color = "rgba(219, 64, 82, 1.0)", Width = 2)))
                Trace(X = xData, Y = [0; 0; 0; 0; 370], Type = TraceType.Bar,
                    Marker = Marker(Color = Some "rgba(50, 171, 96, 0.7)", Line = Line(Color = "rgba(50, 171, 96, 1.0)", Width = 2)))
            ])
        for i = 0 to 4 do
            let result =
                Annotation(
                    X = xData.[i],
                    Y = yData.[i],
                    Text = textList.[i],
                    Font = Font(Family = "Arial", Size = 10, Color = "rgba(245, 246, 249, 1)"),
                    ShowArrow = false)
            this.ChartControl4.Layout.Annotations <- this.ChartControl4.Layout.Annotations @ [result]
        this.ChartControl4.Render()

        // BAR CHART WITH RELATIVE BARMODE
        this.ChartControl5.Layout <- Layout(
            BarMode = BarMode.Relative,
            Xaxis = Axis(Title = "X axis"),
            Yaxis = Axis(Title = "Y axis"),
            Title = "Relative Barmode",
            Width = 300,
            Height = 300)
        this.ChartControl5.Data <- Data(
            Traces = [
                Trace(X = [1; 2; 3; 4], Y = [1; 4; 9; 16], Name = "Trace1", Type = TraceType.Bar)
                Trace(X = [1; 2; 3; 4], Y = [6; -8; -4.5; 8], Name = "Trace2", Type = TraceType.Bar)
                Trace(X = [1; 2; 3; 4], Y = [-15; -3; 4.5; -8], Name = "Trace3", Type = TraceType.Bar)
                Trace(X = [1; 2; 3; 4], Y = [-1; 3; -3; -4], Name = "Trace4", Type = TraceType.Bar)
            ])
        this.ChartControl5.Render()

        // DONUT CHART
        this.ChartControl6.Layout <- Layout(
            Title = "Donut Chart [1990-2011]",
            Annotations = [
                Annotation(Font = Font(Size = 16), ShowArrow = false, Text = "CO2", X = 0.5, Y = 0.5)
            ],
            Width = 320,
            Height = 320)
        this.ChartControl6.Data <- Data(
            Traces = [
                Trace(
                    Values = [27; 11; 25; 8; 3; 25],
                    Labels = ["US"; "China"; "EU"; "Russia"; "India"; "Other"],
                    Text = Some "CO2",
                    TextPosition = "inside",
                    Domain = Domain(X = [0.0; 1.0]),
                    Name = "CO2 Emissions",
                    HoverInfo = "label+percent+name",
                    Hole = 0.4,
                    Type = TraceType.Pie)
            ])
        this.ChartControl6.Render()
