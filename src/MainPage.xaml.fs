namespace OpenSilver.Samples.Showcase

open System
open System.Windows.Browser
open System.Windows
open System.Windows.Controls
open System.Windows.Media

type CurrentState =
    | Unset                     // Initial value
    | LargeResolution_SeeBothMenuAndPage    // This corresponds to tablets and other devices with high resolution. In this case we see both the menu and the page.
    | SmallResolution_ShowMenu              // This corresponds to smartphones and other devices with low resolution. In this case we see the menu.
    | SmallResolution_HideMenu              // This corresponds to smartphones and other devices with low resolution. In this case we do not see the menu.

type MainPage() as this =
    inherit MainPageXaml()

    let mutable currentState : CurrentState = CurrentState.Unset

    do
        this.InitializeComponent()
        
#if OPENSILVER
        this.ThirdPartyButton.Visibility <- Visibility.Collapsed
        this.ThirdPartyHomeButton.Visibility <- Visibility.Visible
#endif

        ViewSourceButtonHelper.OnViewSourceRequested <- Some(this.ViewSourceCode)

        this.Loaded.Add(fun _ -> this.MainPage_Loaded())
        this.SizeChanged.Add(fun args -> this.MainPage_SizeChanged(args))

#if OPENSILVER
        this.TitleImage.Visibility <- Visibility.Collapsed
        this.TitleTextBlock.Text <- @"OPENSILVER
SHOWCASE";
        this.TitleTextBlock.TextAlignment <- TextAlignment.Center
        this.TitleTextBlock.HorizontalAlignment <- HorizontalAlignment.Center
#endif

    member this.NavigateToPage(targetUri: string) =
        //Hide the menu:
        if currentState = CurrentState.SmallResolution_ShowMenu then
            this.GoToState(CurrentState.SmallResolution_HideMenu)

        // Navigate to the target page:
        let uri : Uri = new Uri(targetUri, UriKind.Relative)
        this.PageContainer.Source <- uri;

        // Scroll to top:
        this.ScrollViewer1.ScrollToVerticalOffset(0.0)

    member private this.MainPage_Loaded() =
        // Navigate to the "Welcome" page by default:
        if not (HtmlPage.Document.DocumentUri.OriginalString.Contains("#")) then
            this.NavigateToPage("/Welcome")

    member this.ButtonXamlControls_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/XAML_Controls")

    member this.ButtonXamlFeatures_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/XAML_Features")

    member this.ButtonNetFramework_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Net_Framework")

    member this.ButtonClientServer_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Client_Server")

    member this.ButtonInterop_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Interop_Samples")

    member this.ButtonCharts_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Charts")

    member this.ButtonPerformance_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Performance")

    member this.ButtonMaterialDesign_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Material_Design")

    member this.ButtonPlotlyCharts_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Third_Party/Plotly_Charts/Plotly_Charts_Demo")

    member this.ButtonSyncfusionEssentialJS1Spreadsheet_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Third_Party/Syncfusion_EssentialJS1/Spreadsheet/Spreadsheet_Demo")

    member this.ButtonSyncfusionEssentialJS1RichTextEditor_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Third_Party/Syncfusion_EssentialJS1/RichTextEditor/RichTextEditor_Demo")

    member this.ButtonTelerikKendoUIGrid_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Third_Party/Telerik_KendoUI/Grid/Grid_Demo")

    member this.ButtonTelerikKendoUIEditor_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Third_Party/Telerik_KendoUI/Editor/Editor_Demo")

    member this.ButtonDevExtremeDataGrid_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Third_Party/DevExtreme/DataGrid/DataGrid_Demo")

    member this.ButtonHome_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Welcome")

    member this.ThirdParty_Click(sender: obj, e: RoutedEventArgs) =
        this.NavigateToPage("/Third_Party")

    member private this.ButtonBackwards_Click(sender: obj, e: RoutedEventArgs) =
        if this.PageContainer.CanGoBack then
            this.PageContainer.GoBack()

    member private this.ButtonForward_Click(sender: obj, e: RoutedEventArgs) =
        if this.PageContainer.CanGoForward then
            this.PageContainer.GoForward()

    member private this.PageContainer_Navigated(sender: obj, e: System.Windows.Navigation.NavigationEventArgs) =
        this.ButtonBackwards.IsEnabled <- this.PageContainer.CanGoBack
        this.ButtonForward.IsEnabled <- this.PageContainer.CanGoForward

//#region Show/hide source code

    member public this.ViewSourceCode(controlThatDisplaysTheSourceCode: UIElement) =
        // Open the Source Code Pane, which is the place where the source code will be displayed:
        if this.SourceCodePane.Visibility = Visibility.Collapsed then
            this.RowThatContainsThePage.Height <- new GridLength(0.5, GridUnitType.Star)
            this.RowThatContainsTheGridSplitter.Height <- new GridLength(5.0)
            this.RowThatContainsTheSourceCodePane.Height <- new GridLength(0.5, GridUnitType.Star)
            //this.GridSplitter1.ResizeDirection <- GridSplitter.GridResizeDirection.Rows
            this.GridSplitter1.Visibility <- Visibility.Visible
            this.SourceCodePane.Visibility <- Visibility.Visible

        // Display the source code:
        this.PlaceWhereSourceCodeWillBeDisplayed.Child <- controlThatDisplaysTheSourceCode

    member private this.ButtonToCloseSourceCode_Click(sender: obj, e: RoutedEventArgs) =
        // Close the Source Code Pane, which is the place where the source code is displayed:
        this.PlaceWhereSourceCodeWillBeDisplayed.Child <- null
        this.GridSplitter1.Visibility <- Visibility.Collapsed
        this.SourceCodePane.Visibility <- Visibility.Collapsed
        this.RowThatContainsThePage.Height <- new GridLength(1.0, GridUnitType.Star)
        this.RowThatContainsTheGridSplitter.Height <- new GridLength(0.0)
        this.RowThatContainsTheSourceCodePane.Height <- new GridLength(0.0)

//#endregion

//#region States management

    //This region contains all that we use to make the menu on the left disappear when the screen is too small.

    member this.GoToState(newState: CurrentState) =
        if newState <> currentState then
            match newState with
            | LargeResolution_SeeBothMenuAndPage ->
                // Hide the button to hide/show the menu:
                this.ButtonToHideOrShowMenu.Visibility <- Visibility.Collapsed
                this.PageContainer.Margin <- new Thickness(0.0, 0.0, 0.0, 30.0)

                // Set the translation of the frame to 0:
                (this.PageContainer.RenderTransform :?> TranslateTransform).X <- 0.0

                // Set the margin of the frame to 180 (which is the size of the menu):
                let mutable margin = this.PageContainer.Margin
                margin.Left <- 180.0
                this.PageContainer.Margin <- margin

                // Set the translation of the border to 0:
                (this.MenuBorder.RenderTransform :?> TranslateTransform).X <- 0.0

            | _ ->
                // Revert the changes that are specific to the CurrentState.LargeResolution_SeeBothMenuAndPage state.

                // Show the button to hide/show the menu:
                this.ButtonToHideOrShowMenu.Visibility <- Visibility.Visible
                this.PageContainer.Margin <- new Thickness(0.0, 50.0, 0.0, 30.0)

                let mutable margin = this.PageContainer.Margin
                margin.Left <- 0.0
                this.PageContainer.Margin <- margin

                match newState with
                | SmallResolution_ShowMenu ->
                    // Show the menu:
                    (this.PageContainer.RenderTransform :?> TranslateTransform).X <- 180.0
                    (this.ButtonToHideOrShowMenu.RenderTransform :?> TranslateTransform).X <- 180.0
                    (this.MenuBorder.RenderTransform :?> TranslateTransform).X <- 0.0

                | _ ->
                    // Hide the menu:
                    (this.PageContainer.RenderTransform :?> TranslateTransform).X <- 0.0
                    (this.ButtonToHideOrShowMenu.RenderTransform :?> TranslateTransform).X <- 0.0
                    (this.MenuBorder.RenderTransform :?> TranslateTransform).X <- -180.0

            currentState <- newState

    member private this.MainPage_SizeChanged(e:SizeChangedEventArgs) =
       this.UpdateMenuDispositionBasedOnDisplaySize()

    member this.UpdateMenuDispositionBasedOnDisplaySize() =
        // note: another way to get the display width is commented below:
        // Rect windowBounds = Window.Current.Bounds;
        // double displayWidth = windowBounds.Width;

        let actualWidth = this.ActualWidth
        if not (Double.IsNaN(actualWidth) && actualWidth > 560.0) then
            this.GoToState LargeResolution_SeeBothMenuAndPage
        elif currentState = LargeResolution_SeeBothMenuAndPage || currentState = Unset then
            this.GoToState SmallResolution_HideMenu

    member this.ButtonToHideOrShowMenu_Click(sender: obj, e: RoutedEventArgs) =
        if currentState = SmallResolution_ShowMenu then
            this.GoToState SmallResolution_HideMenu
        elif currentState = SmallResolution_HideMenu then
            this.GoToState SmallResolution_ShowMenu
        else
            // Not supposed to happen because the button is not visible when in large resolution mode.
            ()
//#endregion
