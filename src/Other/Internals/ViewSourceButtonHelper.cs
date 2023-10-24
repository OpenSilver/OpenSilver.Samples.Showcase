using System.Collections.Generic;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    internal static class ViewSourceButtonHelper
    {
        public static void ViewSource(List<ViewSourceButtonInfo> sourcePaths) => ViewSourceImpl(sourcePaths);

        public static void ViewSource(params ViewSourceButtonInfo[] sourcePaths) => ViewSourceImpl(sourcePaths);

        private static void ViewSourceImpl(ICollection<ViewSourceButtonInfo> sourcePaths)
        {
            if (sourcePaths is null || sourcePaths.Count == 0)
            {
                return;
            }

            var tabControl = new TabControl();

            foreach (ViewSourceButtonInfo viewSourceButtonInfo in sourcePaths)
            {
                tabControl.Items.Add(new TabItem()
                {
                    Header = viewSourceButtonInfo.GetHeader(),
                    Content = new ControlToDisplayCodeHostedOnGitHub(viewSourceButtonInfo.GetAbsoluteUrl()),
                });
            }

            ((TabItem)tabControl.Items[0]).IsSelected = true;

            MainPage.Current.ViewSourceCode(tabControl);
        }
    }
}