using OpenSilver.Animations;
using System.Windows.Controls;

namespace OpenSilver.Showcase;

public partial class LoadingControl : UserControl
{
    public IAnimationType Animation { get; }

    public LoadingControl(IAnimationType animation = null)
    {
        InitializeComponent();

        Animation = animation;
        DataContext = this;
    }
}
