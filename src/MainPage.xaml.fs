namespace OpenSilver.Samples.Showcase

open System
open System.Windows.Browser
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Navigation
open System.Windows.Media.Animation
open OpenSilver.Animations

type CurrentState =
    | Unset                                 // Initial value
    | LargeResolution_SeeBothMenuAndPage    // This corresponds to tablets and other devices with high resolution. In this case we see both the menu and the page.
    | SmallResolution_ShowMenu              // This corresponds to smartphones and other devices with low resolution. In this case we see the menu.
    | SmallResolution_HideMenu              // This corresponds to smartphones and other devices with low resolution. In this case we do not see the menu.

type MainPage() as this =
    inherit MainPageXaml()

    let mutable currentState : CurrentState = CurrentState.Unset
    let mutable _skipMenuListBox_SelectionChanged = false
    let mutable _nativeApiButtonBackgroundBrush: SolidColorBrush = null
    
    do
        this.InitializeComponent()
        
        this.Loaded.Add(fun _ -> this.MainPage_Loaded() |> ignore)
        this.SizeChanged.Add(fun args -> this.MainPage_SizeChanged(args))
        this.MenuTreeView.ItemsSource <- Pages.AllPagesAndCategories
        this.MenuTreeView.SelectedItemChanged.Add(fun args -> this.MenuTreeView_SelectedItemChanged(args))
        this.UpdateThemeToggleFillColor()

    member this.NavigateToPage(targetUri: string) =
        //Hide the menu:
        if currentState = CurrentState.SmallResolution_ShowMenu then
            this.GoToState(CurrentState.SmallResolution_HideMenu)

        // Navigate to the target page:
        let uri : Uri = new Uri(targetUri, UriKind.Relative)
        this.PageContainer.Source <- uri;

        // Scroll to top:
        this.PageScrollViewer.ScrollToVerticalOffset(0.0)
        
        member private this.MainPage_Loaded() =
            async {
                // Navigate to the "Welcome" page by default:
                if not (HtmlPage.Document.DocumentUri.OriginalString.Contains("#")) then
                    do! TreeViewHelpers.SelectItemInTreeViewAsync(this.MenuTreeView, Pages.LandingPageInfo) |> Async.AwaitTask |> Async.Ignore
            } |> Async.StartAsTask

        member private this.PageContainer_Navigated (sender: obj) (e: NavigationEventArgs) =
            async {
                _skipMenuListBox_SelectionChanged <- true
                let selectedPage = this.MenuTreeView.SelectedItem :?> PageInfo
                let navigatedPage = Pages.AllPages |> Seq.tryFind (fun x -> x.Path = e.Uri.OriginalString)
                match navigatedPage with
                | Some page when not (obj.ReferenceEquals(page, selectedPage)) ->
                    do! TreeViewHelpers.SelectItemInTreeViewAsync(this.MenuTreeView, page) |> Async.AwaitTask |> Async.Ignore
                | _ -> ()
                _skipMenuListBox_SelectionChanged <- false
            } |> Async.StartAsTask |> ignore
        
        member private this.Logo_MouseLeftButtonDown(sender: obj, e: System.Windows.Input.MouseButtonEventArgs) =
            async {
                do! TreeViewHelpers.SelectItemInTreeViewAsync(this.MenuTreeView, Pages.LandingPageInfo) |> Async.AwaitTask |> Async.Ignore
            } |> Async.StartAsTask |> ignore
        
        member private this.MenuTreeView_SelectedItemChanged(e: RoutedPropertyChangedEventArgs<obj>) =
            if not _skipMenuListBox_SelectionChanged &&
               e.NewValue <> e.OldValue &&
               (e.NewValue :? PageInfo) then
                let page = e.NewValue :?> PageInfo
                this.NavigateToPage(page.Path)
            
        member internal this.StartSearch(searchTerms: string) =
            async {
                _skipMenuListBox_SelectionChanged <- true
                do! TreeViewHelpers.SelectItemInTreeViewAsync(this.MenuTreeView, Pages.SearchPageInfo) |> Async.AwaitTask |> Async.Ignore
                this.NavigateToPage($"/Search/{Uri.EscapeUriString(searchTerms)}")
                _skipMenuListBox_SelectionChanged <- false
            } |> Async.StartAsTask

//#region Show/hide source code

    member public this.ViewSourceCode(controlThatDisplaysTheSourceCode: UIElement) =
        // Open the Source Code Pane, which is the place where the source code will be displayed:
        if this.SourceCodePane.Visibility = Visibility.Collapsed then
            // Make the pane and grid splitter visible
            this.GridSplitter1.Visibility <- Visibility.Visible
            this.SourceCodePane.Visibility <- Visibility.Visible

            // Animate the appearance of the Source Code Pane and the Grid Splitter:
            let easing = CubicEase(EasingMode = EasingMode.EaseOut)

            let animatorForGridSplitter = 
                new PropertyAnimator(
                    this.RowThatContainsTheGridSplitter,
                    RowDefinition.HeightProperty,
                    fun progress -> GridLength(progress * 5.0, GridUnitType.Pixel)
                )
            animatorForGridSplitter.Duration <- TimeSpan.FromMilliseconds(500.0)
            animatorForGridSplitter.EasingFunction <- easing
            animatorForGridSplitter.Begin()

            let animatorForSourceCodePane = 
                new PropertyAnimator(
                    this.RowThatContainsTheSourceCodePane,
                    RowDefinition.HeightProperty,
                    fun progress -> GridLength(progress * 1.0, GridUnitType.Star)
                )
            animatorForSourceCodePane.Duration <- TimeSpan.FromMilliseconds(500.0)
            animatorForSourceCodePane.EasingFunction <- easing
            animatorForSourceCodePane.Begin()

        // Display the source code:
        this.PlaceWhereSourceCodeWillBeDisplayed.Child <- controlThatDisplaysTheSourceCode

    member private this.ButtonToCloseSourceCode_Click(sender: obj, e: RoutedEventArgs) =
        // Close the Source Code Pane
        let mutable initialStarHeightForRowThatContainsTheSourceCodePane = 0.5
        if this.RowThatContainsTheSourceCodePane.Height.GridUnitType = GridUnitType.Star then
            initialStarHeightForRowThatContainsTheSourceCodePane <- this.RowThatContainsTheSourceCodePane.Height.Value

        // Create animations with easing
        let easing = CubicEase(EasingMode = EasingMode.EaseIn)

        let animatorForGridSplitter = 
            new PropertyAnimator(
                this.RowThatContainsTheGridSplitter,
                RowDefinition.HeightProperty,
                fun progress -> GridLength((1.0 - progress) * 5.0, GridUnitType.Pixel)
            )
        animatorForGridSplitter.Duration <- TimeSpan.FromMilliseconds(300.0)
        animatorForGridSplitter.EasingFunction <- easing
        animatorForGridSplitter.Begin()

        let animatorForSourceCodePane = 
            new PropertyAnimator(
                this.RowThatContainsTheSourceCodePane,
                RowDefinition.HeightProperty,
                fun progress -> GridLength(initialStarHeightForRowThatContainsTheSourceCodePane - (progress * initialStarHeightForRowThatContainsTheSourceCodePane), GridUnitType.Star)
            )
        animatorForSourceCodePane.Duration <- TimeSpan.FromMilliseconds(500.0)
        animatorForSourceCodePane.EasingFunction <- easing
        animatorForSourceCodePane.Begin()

        // Completion handler
        animatorForSourceCodePane.Completed.Add (fun _ ->
            async {
                do! Async.Sleep 300

                // Clean up
                this.PlaceWhereSourceCodeWillBeDisplayed.Child <- null
                this.GridSplitter1.Visibility <- Visibility.Collapsed
                this.SourceCodePane.Visibility <- Visibility.Collapsed

                this.RowThatContainsThePage.Height <- GridLength(1.0, GridUnitType.Star)
                this.RowThatContainsTheGridSplitter.Height <- GridLength(0.0, GridUnitType.Pixel)
                this.RowThatContainsTheSourceCodePane.Height <- GridLength(0.0, GridUnitType.Star)

                animatorForGridSplitter.Dispose()
                animatorForSourceCodePane.Dispose()
            } |> Async.Start
        )

//#endregion

//#region States management

    //This region contains all that we use to make the menu on the left disappear when the screen is too small.

    member this.GoToState(newState: CurrentState) =
        if newState <> currentState then
            match newState with
            | LargeResolution_SeeBothMenuAndPage ->
                // Hide the button to hide/show the menu:
                this.ButtonToHideOrShowMenu.Visibility <- Visibility.Collapsed
                
                // Remove the top margin of the page (it's for the menu button on mobile):
                this.PageContainer.Margin <- new Thickness(0, 0, 0, 0)

                // Ensure the page stays in the second column, to the right of the menu:
                Grid.SetColumn(this.PageScrollViewer, 1)
                Grid.SetColumnSpan(this.PageScrollViewer, 1)

                // Show the menu:
                this.MenuContainer.Visibility <- Visibility.Visible

                // Set the translation of the frame to 0:
                (this.PageScrollViewer.RenderTransform :?> TranslateTransform).X <- 0.0

                // Set the translation of the border to 0:
                (this.MenuBorder.RenderTransform :?> TranslateTransform).X <- 0.0

            | _ ->
                // Revert the changes that are specific to the CurrentState.LargeResolution_SeeBothMenuAndPage state.

                // Show the button to hide/show the menu:
                this.ButtonToHideOrShowMenu.Visibility <- Visibility.Visible

                // Add some top margin to the page for the menu button:
                this.PageContainer.Margin <- new Thickness(0, 50, 0, 0)

                // Ensure the page is shown full-screen, not the right of the menu:
                Grid.SetColumn(this.PageScrollViewer, 0)
                Grid.SetColumnSpan(this.PageScrollViewer, 2)

                // Show the menu:
                this.MenuContainer.Visibility <- Visibility.Visible

                let mutable margin = this.PageContainer.Margin
                margin.Left <- 0.0
                this.PageContainer.Margin <- margin

                match newState with
                | SmallResolution_ShowMenu ->
                    // Show the menu:
                    this.MenuContainer.Visibility <- Visibility.Visible

                    // Translate the page to the right, for a nicer effect:
                    (this.PageScrollViewer.RenderTransform :?> TranslateTransform).X <- 240.0

                | _ ->
                    // Hide the menu:
                    this.MenuContainer.Visibility <- Visibility.Collapsed

                    // Translate the page back to its original position:
                    (this.PageScrollViewer.RenderTransform :?> TranslateTransform).X <- 0.0

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


    member this.NativeApiButtonBackgroundBrush
        with get() =
            if isNull _nativeApiButtonBackgroundBrush then
                _nativeApiButtonBackgroundBrush <-
                    this.Resources.["NativeApiButtonBackground"] :?> SolidColorBrush
            _nativeApiButtonBackgroundBrush


    member private this.ThemeToggle_RadioButton_Checked(sender: obj, _e: RoutedEventArgs) =
        let isDark = (this.DarkThemeRadioButton.IsChecked = Nullable(true))

        match Application.Current.Theme with
        | :? OpenSilver.Themes.Modern.ModernTheme as theme ->
            let nativeBrush = this.NativeApiButtonBackgroundBrush
            if isDark then
                nativeBrush.Color <- Color.FromRgb(60uy, 60uy, 60uy)
                theme.CurrentPalette <- OpenSilver.Themes.Modern.ModernTheme.Palettes.Dark
                this.LogoOpenSilverDark.Opacity <- 1
                this.LogoOpenSilverLight.Opacity <- 0
                this.LogoShowcaseDark.Opacity <- 1
                this.LogoShowcaseLight.Opacity <- 0
                this.BackgroundImageDark.Opacity <- 1
                this.BackgroundImageLight.Opacity <- 0
                this.LogoGitHubDark.Opacity <- 1
                this.LogoGitHubLight.Opacity <- 0
            else
                nativeBrush.Color <- Color.FromRgb(221uy, 221uy, 221uy)
                theme.CurrentPalette <- OpenSilver.Themes.Modern.ModernTheme.Palettes.Light
                this.LogoOpenSilverLight.Opacity <- 1
                this.LogoOpenSilverDark.Opacity <- 0
                this.LogoShowcaseLight.Opacity <- 1
                this.LogoShowcaseDark.Opacity <- 0
                this.BackgroundImageLight.Opacity <- 1
                this.BackgroundImageDark.Opacity <- 0
                this.LogoGitHubLight.Opacity <- 1
                this.LogoGitHubDark.Opacity <- 0

            this.UpdateThemeToggleFillColor()

            if this.SourceCodePane.Visibility = Visibility.Visible then
                match this.PlaceWhereSourceCodeWillBeDisplayed.Child with
                | :? TabControl as tabControl ->
                    match tabControl.SelectedItem with
                    | :? TabItem as tabItem ->
                        match tabItem.Content with
                        | :? ControlToDisplayCodeHostedOnGitHub as githubControl ->
                            githubControl.Refresh()
                        | _ -> ()
                    | _ -> ()
                | _ -> ()
        | _ -> ()

    member this.UpdateThemeToggleFillColor() =
        let brush = this.DarkThemeRadioButton.Foreground :?> SolidColorBrush
        let color = if isNull brush then Nullable() else Nullable(brush.Color)
        this.darkThemeImage.FillColor <- color
        this.lightThemeImage.FillColor <- color
