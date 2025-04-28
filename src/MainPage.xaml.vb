Imports System.Windows
Imports System.Windows.Browser
Imports System.Windows.Controls
Imports System.Windows.Markup
Imports System.Windows.Media
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
        End Sub

        Public Shared Property Current As MainPage

        Private Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs)
            ' Navigate to the "Welcome" page by default:
            If Not HtmlPage.Document.DocumentUri.OriginalString.Contains("#") Then
                MenuListBox.SelectedItem = PageInfo.LandingPageInfo
            End If
        End Sub

#Region "Navigation"

        Private Sub MenuListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            If Not (_skipMenuListBox_SelectionChanged AndAlso (e.AddedItems?.Count = 0)) Then
                Dim page As PageInfo = TryCast(e.AddedItems(0), PageInfo)
                If page IsNot Nothing Then
                    NavigateToPage(page.Path)
                End If
            End If
        End Sub

        Private Sub NavigateToPage(targetUri As String)
            If _currentState = CurrentState.SmallResolution_ShowMenu Then
                GoToState(CurrentState.SmallResolution_HideMenu)
            End If

            ' Navigate to the target page:
            Dim uri As New Uri(targetUri, UriKind.Relative)
            PageContainer.Source = uri

            ' Scroll to top:
            ScrollViewer1.ScrollToVerticalOffset(0)
        End Sub

        Private Sub ButtonBackwards_Click(sender As Object, e As RoutedEventArgs)
            If PageContainer.CanGoBack Then
                PageContainer.GoBack()
            End If
        End Sub

        Private Sub ButtonForward_Click(sender As Object, e As RoutedEventArgs)
            If PageContainer.CanGoForward Then
                PageContainer.GoForward()
            End If
        End Sub

        Private Sub Logo_MouseLeftButtonDown(sender As Object, e As Input.MouseButtonEventArgs)
            MenuListBox.SelectedItem = PageInfo.LandingPageInfo
        End Sub

        Private Sub PageContainer_Navigated(sender As Object, e As Navigation.NavigationEventArgs)
            ButtonBackwards.IsEnabled = PageContainer.CanGoBack
            ButtonForward.IsEnabled = PageContainer.CanGoForward
        End Sub

        Private _skipMenuListBox_SelectionChanged As Boolean

        Friend Sub StartSearch(searchTerms As String)
            _skipMenuListBox_SelectionChanged = True
            MenuListBox.SelectedItem = PageInfo.SearchPageInfo
            NavigateToPage($"/Search/{Uri.EscapeUriString(searchTerms)}")
            _skipMenuListBox_SelectionChanged = False
        End Sub

#End Region

#Region "Show/hide source code"

        Public Sub ViewSourceCode(controlThatDisplaysTheSourceCode As UIElement)
            ' Open the Source Code Pane
            If SourceCodePane.Visibility = Visibility.Collapsed Then
                RowThatContainsThePage.Height = New GridLength(0.5, GridUnitType.Star)
                RowThatContainsTheGridSplitter.Height = New GridLength(5)
                RowThatContainsTheSourceCodePane.Height = New GridLength(0.5, GridUnitType.Star)
                GridSplitter1.Visibility = Visibility.Visible
                SourceCodePane.Visibility = Visibility.Visible
            End If

            ' Display the source code
            PlaceWhereSourceCodeWillBeDisplayed.Child = controlThatDisplaysTheSourceCode
        End Sub

        Private Sub ButtonToCloseSourceCode_Click(sender As Object, e As RoutedEventArgs)
            ' Close the Source Code Pane
            PlaceWhereSourceCodeWillBeDisplayed.Child = Nothing
            GridSplitter1.Visibility = Visibility.Collapsed
            SourceCodePane.Visibility = Visibility.Collapsed
            RowThatContainsThePage.Height = New GridLength(1, GridUnitType.Star)
            RowThatContainsTheGridSplitter.Height = New GridLength(0)
            RowThatContainsTheSourceCodePane.Height = New GridLength(0)
        End Sub

#End Region

#Region "States Management"

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
                    PageContainer.Margin = New Thickness(20, 0, 0, 30)
                    CType(PageContainer.RenderTransform, TranslateTransform).X = 0
                    Dim margin As Thickness = PageContainer.Margin
                    margin.Left += MenuBorder.Width
                    PageContainer.Margin = margin
                    CType(MenuBorder.RenderTransform, TranslateTransform).X = 0
                Else
                    ButtonToHideOrShowMenu.Visibility = Visibility.Visible
                    PageContainer.Margin = New Thickness(0, 50, 0, 30)
                    Dim margin As Thickness = PageContainer.Margin
                    margin.Left = 0
                    PageContainer.Margin = margin

                    If newState = CurrentState.SmallResolution_ShowMenu Then
                        CType(PageContainer.RenderTransform, TranslateTransform).X = 180
                        CType(ButtonToHideOrShowMenu.RenderTransform, TranslateTransform).X = 180
                        CType(MenuBorder.RenderTransform, TranslateTransform).X = 0
                    Else
                        CType(PageContainer.RenderTransform, TranslateTransform).X = 0
                        CType(ButtonToHideOrShowMenu.RenderTransform, TranslateTransform).X = 0
                        CType(MenuBorder.RenderTransform, TranslateTransform).X = -180
                    End If
                End If
                _currentState = newState
            End If
        End Sub

        Private Sub MainPage_SizeChanged(sender As Object, e As SizeChangedEventArgs)
            UpdateMenuDispositionBasedOnDisplaySize()
        End Sub

        Private Sub UpdateMenuDispositionBasedOnDisplaySize()
            Dim actualWidth As Double = Me.ActualWidth
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

#Region "Themes Switch"

        Private _nativeApiButtonBackgroundBrush As SolidColorBrush

        Public ReadOnly Property NativeApiButtonBackgroundBrush As SolidColorBrush
            Get
                If _nativeApiButtonBackgroundBrush Is Nothing Then
                    _nativeApiButtonBackgroundBrush = TryCast(Me.Resources("NativeApiButtonBackground"), SolidColorBrush)
                End If
                Return _nativeApiButtonBackgroundBrush
            End Get
        End Property

        Private ReadOnly lightColor As Color = Color.FromRgb(221, 221, 221)
        Private ReadOnly darkColor As Color = Color.FromRgb(60, 60, 60)

        Private Sub ThemeToggle_RadioButton_Checked(sender As Object, e As RoutedEventArgs)
            Dim isDark As Boolean = (DarkThemeRadioButton.IsChecked = True)
            If TypeOf Application.Current.Theme Is ModernTheme Then
                Dim theme = DirectCast(Application.Current.Theme, ModernTheme)
                If isDark Then
                    NativeApiButtonBackgroundBrush.Color = darkColor
                    theme.CurrentPalette = ModernTheme.Palettes.Dark
                    LogoBackgroundDark.Opacity = 1
                    LogoBackgroundLight.Opacity = 0
                Else
                    NativeApiButtonBackgroundBrush.Color = lightColor
                    theme.CurrentPalette = ModernTheme.Palettes.Light
                    LogoBackgroundLight.Opacity = 1
                    LogoBackgroundDark.Opacity = 0
                End If

                If SourceCodePane.Visibility = Visibility.Visible Then
                    Dim tabControl = TryCast(PlaceWhereSourceCodeWillBeDisplayed.Child, TabControl)
                    If tabControl IsNot Nothing Then
                        Dim tabItem = TryCast(tabControl.SelectedItem, TabItem)
                        If tabItem IsNot Nothing Then
                            Dim gitHubControl = TryCast(tabItem.Content, ControlToDisplayCodeHostedOnGitHub)
                            gitHubControl?.Refresh()
                        End If
                    End If
                End If

                BackgroundLayer.Background = Theme_LoadBackgroundGradient()
            End If

            UpdateThemeToggleFillColor()
        End Sub

        Private Sub UpdateThemeToggleFillColor()
            Dim colorBrush As SolidColorBrush = TryCast(DarkThemeRadioButton.Foreground, SolidColorBrush)
            If colorBrush IsNot Nothing Then
                lightThemeImage.FillColor = colorBrush.Color
                darkThemeImage.FillColor = colorBrush.Color
            End If

        End Sub

        Private Function Theme_LoadBackgroundGradient() As Brush
            Return CType(XamlReader.Load(
                "<LinearGradientBrush xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' StartPoint='0.5,0' EndPoint='0.5,1'>" &
                "<GradientStop Color='{DynamicResource Theme_ContainerBackgroundColor}' Offset='0'/>" &
                "<GradientStop Color='{DynamicResource Theme_BackgroundColor}' Offset='0.45'/>" &
                "<GradientStop Color='{DynamicResource Theme_BackgroundColor}' Offset='0.55'/>" &
                "<GradientStop Color='{DynamicResource Theme_ContainerBackgroundColor}' Offset='1'/>" &
                "</LinearGradientBrush>"
            ), Brush)
        End Function

#End Region

    End Class
End Namespace
