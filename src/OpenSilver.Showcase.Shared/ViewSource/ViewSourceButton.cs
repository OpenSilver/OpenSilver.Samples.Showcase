using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    public sealed class ViewSourceButton : Button
    {
        public static readonly RoutedEvent ViewSourceEvent =
            EventManager.RegisterRoutedEvent(
                "ViewSource",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ViewSourceButton));

        public event RoutedEventHandler ViewSource
        {
            add { AddHandler(ViewSourceEvent, value); }
            remove { RemoveHandler(ViewSourceEvent, value); }
        }

        public ViewSourceButton()
        {
            Style = Application.Current.Resources["ButtonViewSource_Style"] as Style;
        }

        public List<ViewSourceButtonInfo> Sources { get; } = new List<ViewSourceButtonInfo>();

        protected override void OnClick()
        {
            base.OnClick();

            if (Sources is null || Sources.Count == 0)
            {
                return;
            }

            RaiseEvent(new RoutedEventArgs(ViewSourceEvent, this));
        }
    }
}
