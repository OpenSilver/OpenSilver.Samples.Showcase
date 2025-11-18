using OpenSilver.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Showcase;

[SearchKeywords("graphics", "shadow", "effect", "blur", "UI")]
public partial class DropShadowEffect_Demo : UserControl
{
    public DropShadowEffect_Demo()
    {
        InitializeComponent();

        ColorPicker.SetBinding(
            HtmlColorPicker.ColorProperty,
            new System.Windows.Data.Binding("Color")
            {
                ElementName = nameof(RectangleShadow),
                Mode = System.Windows.Data.BindingMode.TwoWay,
            });
    }
}
