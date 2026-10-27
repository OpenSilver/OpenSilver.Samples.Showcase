namespace OpenSilver.Showcase

open System
open System.Windows.Browser
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Navigation

type CurrentState =
    | Unset                                 // Initial value
    | LargeResolution_SeeBothMenuAndPage    // This corresponds to tablets and other devices with high resolution. In this case we see both the menu and the page.
    | SmallResolution_ShowMenu              // This corresponds to smartphones and other devices with low resolution. In this case we see the menu.
    | SmallResolution_HideMenu              // This corresponds to smartphones and other devices with low resolution. In this case we do not see the menu.

type MainPage() as this =
    inherit MainPageXaml()

    let mutable currentState : CurrentState = CurrentState.Unset
    let mutable _skipMenu_SelectionChanged = false
    let mutable _nativeApiButtonBackgroundBrush: SolidColorBrush = null
    
    do
        this.InitializeComponent()
        
        this.Loaded.Add(fun _ -> this.MainPage_Loaded() |> ignore)
        this.SizeChanged.Add(fun args -> this.MainPage_SizeChanged(args))
        this.MenuTreeView.ItemsSource <- Pages.AllPagesAndCategories
        this.UpdateThemeToggleFillColor()
        ViewSourceButton.ShowSourceCode <- this.ViewSourceCode

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
            // Navigate to the "Welcome" page by default. The menu selects it when it appears: until
            // then, it is out of the layout (see "OnPageOpen=PushOthers" in the XAML), so its items
            // don't exist yet.
            if not (HtmlPage.Document.DocumentUri.OriginalString.Contains("#")) then
                this.NavigateToPage(Pages.LandingPageInfo.Path)

        member private this.PageContainer_Navigated (sender: obj) (e: NavigationEventArgs) =
            async {
                _skipMenu_SelectionChanged <- true
                let selectedPage = this.MenuTreeView.SelectedItem :?> PageInfo
                let navigatedPage = Pages.AllPages |> Seq.tryFind (fun x -> x.Path = e.Uri.OriginalString)
                match navigatedPage with
                | Some page when not (obj.ReferenceEquals(page, selectedPage)) ->
                    do! TreeViewHelpers.SelectItemInTreeViewAsync(this.MenuTreeView, page) |> Async.AwaitTask |> Async.Ignore
                | _ -> ()
                _skipMenu_SelectionChanged <- false
            } |> Async.StartAsTask |> ignore
        
        member private this.Logo_MouseLeftButtonDown(sender: obj, e: System.Windows.Input.MouseButtonEventArgs) =
            async {
                do! TreeViewHelpers.SelectItemInTreeViewAsync(this.MenuTreeView, Pages.LandingPageInfo) |> Async.AwaitTask |> Async.Ignore
            } |> Async.StartAsTask |> ignore

        member private this.MenuTreeView_SelectedItemChanged(sender: obj, e: RoutedPropertyChangedEventArgs<obj>) =
            if not _skipMenu_SelectionChanged &&
               e.NewValue <> e.OldValue &&
               (e.NewValue :? PageInfo) then
                let page = e.NewValue :?> PageInfo
                this.NavigateToPage(page.Path)

            
        member internal this.StartSearch(searchTerms: string) =
            async {
                _skipMenu_SelectionChanged <- true
                do! TreeViewHelpers.SelectItemInTreeViewAsync(this.MenuTreeView, Pages.SearchPageInfo) |> Async.AwaitTask |> Async.Ignore
                this.NavigateToPage($"/Search/{Uri.EscapeUriString(searchTerms)}")
                _skipMenu_SelectionChanged <- false
            } |> Async.StartAsTask

//#region Show/hide source code

    member public this.ViewSourceCode(controlThatDisplaysTheSourceCode: UIElement) =
        // Open the Source Code Pane, which is the place where the source code will be displayed.
        // It slides up from the bottom while the page makes room for it (see "Animate.Appear" in the XAML):
        if this.SourceCodePane.Visibility = Visibility.Collapsed then
            this.RowThatContainsTheGridSplitter.Height <- GridLength(5.0, GridUnitType.Pixel)
            this.RowThatContainsTheSourceCodePane.Height <- GridLength(1.0, GridUnitType.Star)
            this.GridSplitter1.Visibility <- Visibility.Visible
            this.SourceCodePane.Visibility <- Visibility.Visible

        // Display the source code:
        this.PlaceWhereSourceCodeWillBeDisplayed.Child <- controlThatDisplaysTheSourceCode

    member private this.ButtonToCloseSourceCode_Click(sender: obj, e: RoutedEventArgs) =
        // Close the Source Code Pane.
        // It slides down while the page fills its place (see "Animate.Disappear" in the XAML):
        this.GridSplitter1.Visibility <- Visibility.Collapsed
        this.SourceCodePane.Visibility <- Visibility.Collapsed
        this.PlaceWhereSourceCodeWillBeDisplayed.Child <- null

        // Reset the row heights to their original values (the user may have dragged the splitter):
        this.RowThatContainsThePage.Height <- GridLength(1.0, GridUnitType.Star)
        this.RowThatContainsTheGridSplitter.Height <- GridLength(0.0, GridUnitType.Pixel)
        this.RowThatContainsTheSourceCodePane.Height <- GridLength(0.0, GridUnitType.Star)

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
        // Raised while the page loads, for the IsChecked of the light theme, before
        // DarkThemeRadioButton exists: the page already starts with the light theme.
        if not (isNull this.DarkThemeRadioButton) then
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
