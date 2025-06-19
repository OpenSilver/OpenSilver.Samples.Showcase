using OpenSilver.Animations;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

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
