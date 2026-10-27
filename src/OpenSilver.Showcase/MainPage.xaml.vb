Imports System.Windows
Imports System.Windows.Browser
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Navigation
Imports OpenSilver.Animations
Imports OpenSilver.Themes.Modern

Namespace OpenSilver.Showcase
    Partial Public Class MainPage
        Inherits Page

        Public Sub New()
            InitializeComponent()

            Current = Me
            AddHandler Loaded, AddressOf MainPage_Loaded
            AddHandler SizeChanged, AddressOf MainPage_SizeChanged
            MenuTreeView.ItemsSource = Pages.AllPagesAndCategories
            UpdateThemeToggleFillColor()
            'AnimationSettings.Current.Speed = 0.1 ' ten times slower
        End Sub

        Public Shared Property Current As MainPage

        Private Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs)
            ' Navigate to the "Welcome" page by default. The menu selects it when it appears: until
            ' then, it is out of the layout (see "OnPageOpen=PushOthers" in the XAML), so its items
            ' don't exist yet.
            If Not HtmlPage.Document.DocumentUri.OriginalString.Contains("#") Then
                NavigateToPage(Pages.LandingPageInfo.Path)
            End If
        End Sub

#Region "Navigation"
        Private Sub MenuTreeView_SelectedItemChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object))
            If Not _skipMenu_SelectionChanged AndAlso
               e.NewValue IsNot e.OldValue AndAlso
               TypeOf e.NewValue Is PageInfo Then
                Dim page As PageInfo = DirectCast(e.NewValue, PageInfo)
                NavigateToPage(page.Path)
            End If
        End Sub

        Private Sub NavigateToPage(targetUri As String)
            If _currentState = CurrentState.SmallResolution_ShowMenu Then
                GoToState(CurrentState.SmallResolution_HideMenu)
            End If

            PageContainer.Source = New Uri(targetUri, UriKind.Relative)
            PageScrollViewer.ScrollToVerticalOffset(0)
        End Sub

        Private Async Sub PageContainer_Navigated(sender As Object, e As NavigationEventArgs)
            _skipMenu_SelectionChanged = True
            Dim selectedPage As PageInfo = TryCast(MenuTreeView.SelectedItem, PageInfo)
            Dim navigatedPage As PageInfo = Pages.AllPages.FirstOrDefault(Function(x) x.Path = e.Uri.OriginalString)

            If navigatedPage IsNot selectedPage Then
                Await TreeViewHelpers.SelectItemInTreeViewAsync(MenuTreeView, navigatedPage)
            End If

            _skipMenu_SelectionChanged = False
        End Sub

        Private Async Sub Logo_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
            Await TreeViewHelpers.SelectItemInTreeViewAsync(MenuTreeView, Pages.LandingPageInfo)
        End Sub

        Private _skipMenu_SelectionChanged As Boolean

        Friend Async Function StartSearch(searchTerms As String) As Task
            _skipMenu_SelectionChanged = True
            Await TreeViewHelpers.SelectItemInTreeViewAsync(MenuTreeView, Pages.SearchPageInfo)
            NavigateToPage($"/Search/{Uri.EscapeUriString(searchTerms)}")
            _skipMenu_SelectionChanged = False
        End Function

#End Region

#Region "Source Code View"

        Public Sub ViewSourceCode(control As UIElement)
            ' Open the Source Code Pane.
            ' It slides up from the bottom while the page makes room for it (see "Animate.Appear" in the XAML):
            If SourceCodePane.Visibility = Visibility.Collapsed Then
                RowThatContainsTheGridSplitter.Height = New GridLength(5.0, GridUnitType.Pixel)
                RowThatContainsTheSourceCodePane.Height = New GridLength(1.0, GridUnitType.Star)
                GridSplitter1.Visibility = Visibility.Visible
                SourceCodePane.Visibility = Visibility.Visible
            End If

            PlaceWhereSourceCodeWillBeDisplayed.Child = control
        End Sub

        Private Sub ButtonToCloseSourceCode_Click(sender As Object, e As RoutedEventArgs)
            ' Close the Source Code Pane.
            ' It slides down while the page fills its place (see "Animate.Disappear" in the XAML):
            GridSplitter1.Visibility = Visibility.Collapsed
            SourceCodePane.Visibility = Visibility.Collapsed
            PlaceWhereSourceCodeWillBeDisplayed.Child = Nothing

            ' Reset the row heights (the user may have dragged the splitter)
            RowThatContainsThePage.Height = New GridLength(1.0, GridUnitType.Star)
            RowThatContainsTheGridSplitter.Height = New GridLength(0.0, GridUnitType.Pixel)
            RowThatContainsTheSourceCodePane.Height = New GridLength(0.0, GridUnitType.Star)
        End Sub

#End Region

#Region "Menu State"

        Private Enum CurrentState
            Unset
            LargeResolution_SeeBothMenuAndPage
            SmallResolution_ShowMenu
            SmallResolution_HideMenu
        End Enum

        Private _currentState As CurrentState

        Private Sub GoToState(newState As CurrentState)
            If newState <> _currentState Then
                If newState = CurrentState.LargeResolution_SeeBothMenuAndPage Then
                    ButtonToHideOrShowMenu.Visibility = Visibility.Collapsed
                    PageContainer.Margin = New Thickness(0)
                    Grid.SetColumn(PageScrollViewer, 1)
                    Grid.SetColumnSpan(PageScrollViewer, 1)
                    MenuContainer.Visibility = Visibility.Visible
                    CType(PageScrollViewer.RenderTransform, TranslateTransform).X = 0
                    CType(MenuBorder.RenderTransform, TranslateTransform).X = 0
                Else
                    ButtonToHideOrShowMenu.Visibility = Visibility.Visible
                    PageContainer.Margin = New Thickness(0, 50, 0, 0)
                    Grid.SetColumn(PageScrollViewer, 0)
                    Grid.SetColumnSpan(PageScrollViewer, 2)

                    If newState = CurrentState.SmallResolution_ShowMenu Then
                        MenuContainer.Visibility = Visibility.Visible
                        CType(PageScrollViewer.RenderTransform, TranslateTransform).X = 240
                    Else
                        MenuContainer.Visibility = Visibility.Collapsed
                        CType(PageScrollViewer.RenderTransform, TranslateTransform).X = 0
                    End If
                End If
                _currentState = newState
            End If
        End Sub

        Private Sub MainPage_SizeChanged(sender As Object, e As SizeChangedEventArgs)
            UpdateMenuDispositionBasedOnDisplaySize()
        End Sub

        Private Sub UpdateMenuDispositionBasedOnDisplaySize()
            Dim actualWidth = Me.ActualWidth
            If Not Double.IsNaN(actualWidth) AndAlso actualWidth > 560 Then
                GoToState(CurrentState.LargeResolution_SeeBothMenuAndPage)
            ElseIf _currentState = CurrentState.LargeResolution_SeeBothMenuAndPage OrElse _currentState = CurrentState.Unset Then
                GoToState(CurrentState.SmallResolution_HideMenu)
            End If
        End Sub

        Private Sub ButtonToHideOrShowMenu_Click(sender As Object, e As RoutedEventArgs)
            If _currentState = CurrentState.SmallResolution_ShowMenu Then
                GoToState(CurrentState.SmallResolution_HideMenu)
            ElseIf _currentState = CurrentState.SmallResolution_HideMenu Then
                GoToState(CurrentState.SmallResolution_ShowMenu)
            End If
        End Sub

#End Region

#Region "Theme Switching"

        Private _nativeApiButtonBackgroundBrush As SolidColorBrush
        Public ReadOnly Property NativeApiButtonBackgroundBrush As SolidColorBrush
            Get
                If _nativeApiButtonBackgroundBrush Is Nothing Then
                    _nativeApiButtonBackgroundBrush = TryCast(Me.Resources("NativeApiButtonBackground"), SolidColorBrush)
                End If
                Return _nativeApiButtonBackgroundBrush
            End Get
        End Property

        Private lightColor As Color = Color.FromRgb(221, 221, 221)
        Private darkColor As Color = Color.FromRgb(60, 60, 60)

        Private Sub ThemeToggle_RadioButton_Checked(sender As Object, e As RoutedEventArgs)
            ' Raised while the page loads, for the IsChecked of the light theme, before
            ' DarkThemeRadioButton exists: the page already starts with the light theme.
            If DarkThemeRadioButton Is Nothing Then
                Return
            End If

            Dim isDark = (DarkThemeRadioButton.IsChecked = True)
            Dim theme = TryCast(Application.Current.Theme, ModernTheme)
            If theme IsNot Nothing Then
                If isDark Then
                    NativeApiButtonBackgroundBrush.Color = darkColor
                    theme.CurrentPalette = ModernTheme.Palettes.Dark
                    LogoOpenSilverDark.Opacity = 1
                    LogoOpenSilverLight.Opacity = 0
                    LogoShowcaseDark.Opacity = 1
                    LogoShowcaseLight.Opacity = 0
                    BackgroundImageDark.Opacity = 1
                    BackgroundImageLight.Opacity = 0
                    LogoGitHubDark.Opacity = 1
                    LogoGitHubLight.Opacity = 0
                Else
                    NativeApiButtonBackgroundBrush.Color = lightColor
                    theme.CurrentPalette = ModernTheme.Palettes.Light
                    LogoOpenSilverLight.Opacity = 1
                    LogoOpenSilverDark.Opacity = 0
                    LogoShowcaseLight.Opacity = 1
                    LogoShowcaseDark.Opacity = 0
                    BackgroundImageLight.Opacity = 1
                    BackgroundImageDark.Opacity = 0
                    LogoGitHubLight.Opacity = 1
                    LogoGitHubDark.Opacity = 0
                End If

                If SourceCodePane.Visibility = Visibility.Visible AndAlso
                    TypeOf PlaceWhereSourceCodeWillBeDisplayed.Child Is TabControl AndAlso
                    TypeOf CType(PlaceWhereSourceCodeWillBeDisplayed.Child, TabControl).SelectedItem Is TabItem AndAlso
                    TypeOf CType(CType(PlaceWhereSourceCodeWillBeDisplayed.Child, TabControl).SelectedItem, TabItem).Content Is ControlToDisplayCodeHostedOnGitHub Then
                    CType(CType(CType(PlaceWhereSourceCodeWillBeDisplayed.Child, TabControl).SelectedItem, TabItem).Content, ControlToDisplayCodeHostedOnGitHub).Refresh()
                End If
            End If

            UpdateThemeToggleFillColor()
        End Sub

        Private Sub UpdateThemeToggleFillColor()
            Dim color = TryCast(DarkThemeRadioButton.Foreground, SolidColorBrush)?.Color
            lightThemeImage.FillColor = color
            darkThemeImage.FillColor = color
        End Sub

#End Region

    End Class
End Namespace
