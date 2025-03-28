Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    Partial Public Class ChartDemo
        Inherits ContentControl

        Private Const MinLargeWidth As Integer = 700
        Private Const CardMargin As Integer = 50
        Private Const BigCardWidth As Integer = 645
        Private Const BigCardHeight As Integer = 385
        Private Const SmallCardHeight As Integer = 300

        Private _isWideScreen As Boolean?
        Private _lastCardWidth As Double

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub INTERNAL_OnAttachedToVisualTree()
            Dim parent = TryCast(Me.Parent, FrameworkElement)
            UpdateAdaptiveSize(parent.ActualWidth)
            AddHandler parent.SizeChanged, Sub(s, e) UpdateAdaptiveSize(e.NewSize.Width)

            MyBase.INTERNAL_OnAttachedToVisualTree()
        End Sub

        Private Sub UpdateAdaptiveSize(screenWidth As Double)
            If screenWidth >= MinLargeWidth Then
                If Not _isWideScreen.GetValueOrDefault() Then
                    _isWideScreen = True
                    UpdateCardSize(BigCardWidth, BigCardHeight)
                End If
            Else
                Dim cardWidth = Math.Max(screenWidth - CardMargin, CardMargin * 2)
                If Not _isWideScreen.GetValueOrDefault() OrElse Math.Abs(_lastCardWidth - cardWidth) > CardMargin / 2 Then
                    UpdateCardSize(cardWidth, SmallCardHeight)
                End If
                _isWideScreen = False
            End If
        End Sub

        Private Sub UpdateCardSize(cardWidth As Double, cardHeight As Double)
            _lastCardWidth = cardWidth
            Width = cardWidth
            Height = cardHeight
        End Sub

    End Class
End Namespace
