using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public class WrapPanelWithProgressiveLoading : WrapPanel
    {
        public WrapPanelWithProgressiveLoading()
        {
            ProgressiveRenderingChunkSize = 3;
        }
    }
}
