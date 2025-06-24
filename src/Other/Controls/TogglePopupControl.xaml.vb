Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Markup

Namespace OpenSilver.Samples.Showcase

    <ContentProperty("PopupContent")>
    Partial Public Class TogglePopupControl
        Inherits UserControl

        ' IsPopupOpen Property
        Public Property IsPopupOpen As Boolean
            Get
                Return CBool(GetValue(IsPopupOpenProperty))
            End Get
            Set(value As Boolean)
                SetValue(IsPopupOpenProperty, value)
            End Set
        End Property

        Public Shared ReadOnly IsPopupOpenProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(IsPopupOpen), GetType(Boolean), GetType(TogglePopupControl), New PropertyMetadata(False))

        ' PopupBorderStyle Property
        Public Property PopupBorderStyle As Style
            Get
                Return CType(GetValue(PopupBorderStyleProperty), Style)
            End Get
            Set(value As Style)
                SetValue(PopupBorderStyleProperty, value)
            End Set
        End Property

        Public Shared ReadOnly PopupBorderStyleProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(PopupBorderStyle), GetType(Style), GetType(TogglePopupControl))

        ' PopupContent Property
        Public Property PopupContent As Object
            Get
                Return GetValue(PopupContentProperty)
            End Get
            Set(value As Object)
                SetValue(PopupContentProperty, value)
            End Set
        End Property

        Public Shared ReadOnly PopupContentProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(PopupContent), GetType(Object), GetType(TogglePopupControl))

        ' ToggleButtonContent Property
        Public Property ToggleButtonContent As Object
            Get
                Return GetValue(ToggleButtonContentProperty)
            End Get
            Set(value As Object)
                SetValue(ToggleButtonContentProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ToggleButtonContentProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(ToggleButtonContent), GetType(Object), GetType(TogglePopupControl))

        ' ToggleButtonStyle Property
        Public Property ToggleButtonStyle As Style
            Get
                Return CType(GetValue(ToggleButtonStyleProperty), Style)
            End Get
            Set(value As Style)
                SetValue(ToggleButtonStyleProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ToggleButtonStyleProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(ToggleButtonStyle), GetType(Style), GetType(TogglePopupControl))

        Public Sub New()
            InitializeComponent()
        End Sub

    End Class

End Namespace
