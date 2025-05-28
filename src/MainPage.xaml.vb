Imports System.Windows
Imports System.Windows.Browser
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Navigation
Imports OpenSilver.Animations
Imports OpenSilver.Themes.Modern

Namespace OpenSilver.Samples.Showcase
    Partial Public Class MainPage
        Inherits Page

        Public Sub New()
            InitializeComponent()

            Current = Me
            AddHandler Loaded, AddressOf MainPage_Loaded
            AddHandler SizeChanged, AddressOf MainPage_SizeChanged
            MenuListBox.ItemsSource = PageInfo.Pages
            UpdateThemeToggleFillColor()

            'Animations.Animation.SlowDownAnimationsForDebugging = 10.0
        End Sub

        Public Shared Property Current As MainPage

        Private Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs)
            If Not HtmlPage.Document.DocumentUri.OriginalString.Contains("#") Then
                MenuListBox.SelectedItem = PageInfo.LandingPageInfo
            End If
        End Sub

#Region "Navigation"

        Private Sub MenuListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            If Not (_skipMenuListBox_SelectionChanged AndAlso (e.AddedItems?.Count = 0)) Then
                Dim page = TryCast(e.AddedItems(0), PageInfo)
                If page IsNot Nothing Then
                    NavigateToPage(page.Path)
                End If
            End If
        End Sub

        Private Sub NavigateToPage(targetUri As String)
            If _currentState = CurrentState.SmallResolution_ShowMenu Then
                GoToState(CurrentState.SmallResolution_HideMenu)
            End If

            PageContainer.Source = New Uri(targetUri, UriKind.Relative)
            PageScrollViewer.ScrollToVerticalOffset(0)
        End Sub

        Private Sub PageContainer_Navigated(sender As Object, e As NavigationEventArgs)
            _skipMenuListBox_SelectionChanged = True
            Dim selectedPage As PageInfo = TryCast(MenuListBox.SelectedItem, PageInfo)
            Dim navigatedPage As PageInfo = PageInfo.Pages.FirstOrDefault(Function(x) x.Path = e.Uri.OriginalString)

            If navigatedPage IsNot selectedPage Then
                MenuListBox.SelectedItem = navigatedPage
            End If

            _skipMenuListBox_SelectionChanged = False
        End Sub

        Private Sub Logo_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
            MenuListBox.SelectedItem = PageInfo.LandingPageInfo
        End Sub

        Private _skipMenuListBox_SelectionChanged As Boolean

        Friend Sub StartSearch(searchTerms As String)
            _skipMenuListBox_SelectionChanged = True
            MenuListBox.SelectedItem = PageInfo.SearchPageInfo
            NavigateToPage($"/Search/{Uri.EscapeUriString(searchTerms)}")
            _skipMenuListBox_SelectionChanged = False
        End Sub

#End Region

#Region "Source Code View"

        Public Sub ViewSourceCode(control As UIElement)
            If SourceCodePane.Visibility = Visibility.Collapsed Then
                ' Make the pane and grid splitter visible
                GridSplitter1.Visibility = Visibility.Visible
                SourceCodePane.Visibility = Visibility.Visible

                ' Animate the appearance of the Source Code Pane and the Grid Splitter:
                Dim easing As New CubicEase With {
                    .EasingMode = EasingMode.EaseOut
                }

                Dim animatorForGridSplitter As New PropertyAnimator(
                    RowThatContainsTheGridSplitter,
                    RowDefinition.HeightProperty,
                    Function(progress) New GridLength(progress * 5.0, GridUnitType.Pixel)
                ) With {
                    .Duration = TimeSpan.FromMilliseconds(500),
                    .EasingFunction = easing
                }
                animatorForGridSplitter.Begin()

                Dim animatorForSourceCodePane As New PropertyAnimator(
                    RowThatContainsTheSourceCodePane,
                    RowDefinition.HeightProperty,
                    Function(progress) New GridLength(progress * 1.0, GridUnitType.Star)
                ) With {
                    .Duration = TimeSpan.FromMilliseconds(500),
                    .EasingFunction = easing
                }
                animatorForSourceCodePane.Begin()
            End If

            PlaceWhereSourceCodeWillBeDisplayed.Child = control
        End Sub

        Private Sub ButtonToCloseSourceCode_Click(sender As Object, e As RoutedEventArgs)
            ' Close the Source Code Pane
            Dim initialStarHeightForRowThatContainsTheSourceCodePane As Double = 0.5
            If RowThatContainsTheSourceCodePane.Height.GridUnitType = GridUnitType.Star Then
                initialStarHeightForRowThatContainsTheSourceCodePane = RowThatContainsTheSourceCodePane.Height.Value
            End If

            ' Create animations with easing
            Dim easing As New CubicEase With {
                .EasingMode = EasingMode.EaseIn
            }

            Dim animatorForGridSplitter As New PropertyAnimator(
                RowThatContainsTheGridSplitter,
                RowDefinition.HeightProperty,
                Function(progress) New GridLength((1.0 - progress) * 5.0, GridUnitType.Pixel)
            ) With {
                .Duration = TimeSpan.FromMilliseconds(300),
                .EasingFunction = easing
            }
            animatorForGridSplitter.Begin()

            Dim animatorForSourceCodePane As New PropertyAnimator(
                RowThatContainsTheSourceCodePane,
                RowDefinition.HeightProperty,
                Function(progress) New GridLength(initialStarHeightForRowThatContainsTheSourceCodePane - (progress * initialStarHeightForRowThatContainsTheSourceCodePane), GridUnitType.Star)
            ) With {
                .Duration = TimeSpan.FromMilliseconds(500),
                .EasingFunction = easing
            }
            animatorForSourceCodePane.Begin()

            ' Set up completion handler
            AddHandler animatorForSourceCodePane.Completed, Async Sub(s As Object, args As EventArgs)
                                                                Await Task.Delay(300)

                                                                ' Clean up when animation completes
                                                                PlaceWhereSourceCodeWillBeDisplayed.Child = Nothing
                                                                GridSplitter1.Visibility = Visibility.Collapsed
                                                                SourceCodePane.Visibility = Visibility.Collapsed

                                                                ' Reset the row heights
                                                                RowThatContainsThePage.Height = New GridLength(1.0, GridUnitType.Star)
                                                                RowThatContainsTheGridSplitter.Height = New GridLength(0.0, GridUnitType.Pixel)
                                                                RowThatContainsTheSourceCodePane.Height = New GridLength(0.0, GridUnitType.Star)

                                                                ' Dispose
                                                                animatorForGridSplitter.Dispose()
                                                                animatorForSourceCodePane.Dispose()
                                                            End Sub

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
                    CType(PageContainer.RenderTransform, TranslateTransform).X = 0
                    CType(MenuBorder.RenderTransform, TranslateTransform).X = 0
                Else
                    ButtonToHideOrShowMenu.Visibility = Visibility.Visible
                    PageContainer.Margin = New Thickness(0, 50, 0, 0)
                    Grid.SetColumn(PageScrollViewer, 0)
                    Grid.SetColumnSpan(PageScrollViewer, 2)

                    If newState = CurrentState.SmallResolution_ShowMenu Then
                        MenuContainer.Visibility = Visibility.Visible
                        CType(PageContainer.RenderTransform, TranslateTransform).X = 240
                    Else
                        MenuContainer.Visibility = Visibility.Collapsed
                        CType(PageContainer.RenderTransform, TranslateTransform).X = 0
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
                Else
                    NativeApiButtonBackgroundBrush.Color = lightColor
                    theme.CurrentPalette = ModernTheme.Palettes.Light
                    LogoOpenSilverLight.Opacity = 1
                    LogoOpenSilverDark.Opacity = 0
                    LogoShowcaseLight.Opacity = 1
                    LogoShowcaseDark.Opacity = 0
                    BackgroundImageLight.Opacity = 1
                    BackgroundImageDark.Opacity = 0
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
