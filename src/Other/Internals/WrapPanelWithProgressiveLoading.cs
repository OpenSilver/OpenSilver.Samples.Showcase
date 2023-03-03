using System;
using System.Collections.Generic;
using System.Text;

#if SLMIGRATION
using System.Windows.Controls;
#else
using Windows.UI.Xaml.Controls;
#endif

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
