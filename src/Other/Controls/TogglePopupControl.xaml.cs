using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace OpenSilver.Samples.Showcase;

[ContentProperty(nameof(PopupContent))]
public partial class TogglePopupControl : UserControl
{
    #region IsPopupOpen
    public bool IsPopupOpen
    {
        get => (bool)GetValue(IsPopupOpenProperty);
        set => SetValue(IsPopupOpenProperty, value);
    }

    public static readonly DependencyProperty IsPopupOpenProperty =
        DependencyProperty.Register(nameof(IsPopupOpen), typeof(bool), typeof(TogglePopupControl), new PropertyMetadata(false));
    #endregion

    #region PopupBorderStyle
    public Style PopupBorderStyle
    {
        get => (Style)GetValue(PopupBorderStyleProperty);
        set => SetValue(PopupBorderStyleProperty, value);
    }

    public static readonly DependencyProperty PopupBorderStyleProperty =
        DependencyProperty.Register(nameof(PopupBorderStyle), typeof(Style), typeof(TogglePopupControl));
    #endregion

    #region PopupContent
    public object PopupContent
    {
        get => GetValue(PopupContentProperty);
        set => SetValue(PopupContentProperty, value);
    }

    public static readonly DependencyProperty PopupContentProperty =
        DependencyProperty.Register(nameof(PopupContent), typeof(object), typeof(TogglePopupControl));
    #endregion

    #region ToggleButtonContent
    public object ToggleButtonContent
    {
        get => GetValue(ToggleButtonContentProperty);
        set => SetValue(ToggleButtonContentProperty, value);
    }

    public static readonly DependencyProperty ToggleButtonContentProperty =
        DependencyProperty.Register(nameof(ToggleButtonContent), typeof(object), typeof(TogglePopupControl));
    #endregion

    #region ToggleButtonStyle
    public Style ToggleButtonStyle
    {
        get => (Style)GetValue(ToggleButtonStyleProperty);
        set => SetValue(ToggleButtonStyleProperty, value);
    }

    public static readonly DependencyProperty ToggleButtonStyleProperty =
        DependencyProperty.Register(nameof(ToggleButtonStyle), typeof(Style), typeof(TogglePopupControl));
    #endregion

    public TogglePopupControl()
    {
        InitializeComponent();
    }
}
