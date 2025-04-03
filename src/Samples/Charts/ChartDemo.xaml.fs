namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls

type public ChartDemo() as this =
    inherit ChartDemoXaml()

    // Constants
    let MinLargeWidth = 700.0
    let CardMargin = 50.0
    let BigCardWidth = 645.0
    let BigCardHeight = 385.0
    let SmallCardHeight = 300.0

    // Mutable state
    let mutable isWideScreen: bool option = None
    let mutable lastCardWidth: float = 0.0

    do
        this.InitializeComponent()

    override this.INTERNAL_OnAttachedToVisualTree() =
        let parent = this.Parent :?> FrameworkElement
        this.UpdateAdaptiveSize(parent.ActualWidth)

        parent.SizeChanged.Add(fun args ->
            this.UpdateAdaptiveSize(args.NewSize.Width)
        )

        base.INTERNAL_OnAttachedToVisualTree()

    member private this.UpdateAdaptiveSize(screenWidth: float) =
        if screenWidth >= MinLargeWidth then
            if isWideScreen <> Some true then
                isWideScreen <- Some true
                this.UpdateCardSize(BigCardWidth, BigCardHeight)
        else
            let cardWidth = Math.Max(screenWidth - CardMargin, CardMargin * 2.0)
            if isWideScreen <> Some false || Math.Abs(lastCardWidth - cardWidth) > CardMargin / 2.0 then
                this.UpdateCardSize(cardWidth, SmallCardHeight)
            isWideScreen <- Some false

    member private this.UpdateCardSize(cardWidth: float, cardHeight: float) =
        lastCardWidth <- cardWidth
        this.Width <- cardWidth
        this.Height <- cardHeight
