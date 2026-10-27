using OpenSilver.Animations;
using System.Windows.Controls;

namespace OpenSilver.Showcase;

public partial class LoadingControl : UserControl
{
    public Transition Animation { get; }

    public LoadingControl(Transition animation = null)
    {
        InitializeComponent();

        Animation = animation;
        DataContext = this;
    }
}
