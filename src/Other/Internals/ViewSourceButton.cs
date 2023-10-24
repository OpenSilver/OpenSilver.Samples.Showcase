using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public sealed class ViewSourceButton : Button
    {
        public ViewSourceButton()
        {
            Style = Application.Current.Resources["ButtonViewSource_Style"] as Style;
        }

        public List<ViewSourceButtonInfo> Sources { get; } = new List<ViewSourceButtonInfo>();

        protected override void OnClick()
        {
            base.OnClick();
            ViewSourceButtonHelper.ViewSource(Sources);
        }
    }
}
