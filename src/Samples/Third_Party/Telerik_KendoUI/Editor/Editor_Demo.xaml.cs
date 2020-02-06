#if !OPENSILVER
using CSHTML5.Wrappers.KendoUI.Common;
using kendo_ui_editor.kendo.ui; 
#endif
using System.Collections.Generic;
using TypeScriptDefinitionsSupport;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#endif

namespace CSHTML5.Samples.Showcase.Samples.Third_Party.Telerik_KendoUI.Editor
{
    public partial class Editor_Demo : Page
    {
        public Editor_Demo()
        {
            InitializeComponent();

#if !OPENSILVER
            //Note: Below is an example of setting the location for the required scripts and css for the KendoUI Editor control. See the tutorial at http://cshtml5.com for more information.
            kendo_ui_editor.kendo.ui.Editor.Configuration.LocationOfKendoAllJS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Telerik_KendoUI/scripts/kendo.all.min.js";
            kendo_ui_editor.kendo.ui.Editor.Configuration.LocationOfKendoCommonMaterialCSS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Telerik_KendoUI/styles/kendo.common-material.min.css";
            kendo_ui_editor.kendo.ui.Editor.Configuration.LocationOfKendoMaterialCSS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Telerik_KendoUI/styles/kendo.material.min.css";
            kendo_ui_editor.kendo.ui.Editor.Configuration.LocationOfKendoMaterialMobileCSS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Telerik_KendoUI/styles/kendo.material.mobile.min.css";
            kendo_ui_editor.kendo.ui.Editor.Configuration.LocationOfKendoRTLCSS = "ms-appx:///CSHTML5.Samples.Showcase/Third_Party_Resources/Telerik_KendoUI/styles/kendo.rtl.min.css";

            Loaded += Editor_Demo_Loaded;
#endif
        }

#if !OPENSILVER
        private async void Editor_Demo_Loaded(object sender, RoutedEventArgs e)
        {
            //------------
            // Wait until Editor's underlying JS instance has been loaded
            //------------
            LoadingPleaseWaitMessage.Visibility = Visibility.Visible;
            bool result = await Editor.JSInstanceLoaded;
            if (result)
            {
                LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed;
                EditorDemo.Visibility = Visibility.Visible;

                Editor.options.tools.Add(new EditorTool() { name = "insertFile" });
                Editor.setOptions(Editor.options);

                string someHtml = @"
<h1>I'm BIG.</h1>
<b>I'm BOLD.</b><br>
<i>I'm ITALIC.</i><br>
<u>I'm UNDERLINED.</u>";

                Interop.ExecuteJavaScript("$0.innerHTML = $1", Editor.body.UnderlyingJSInstance, someHtml);
            }
            else
            {
                EditorDemo.Visibility = Visibility.Visible;
                LoadingPleaseWaitMessage.Visibility = Visibility.Collapsed;
                ButtonExportToPDF.Visibility = Visibility.Collapsed;
            }
            
        }
#endif

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Editor_Demo.xaml",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Third_Party/Telerik_KendoUI/Editor/Editor_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "Editor_Demo.xaml.cs",
                    FilePathOnGitHub = "github/cshtml5/CSHTML5.Samples.Showcase/blob/master/CSHTML5.Samples.Showcase/Samples/Third_Party/Telerik_KendoUI/Editor/Editor_Demo.xaml.cs"
                }
            });
        }

#if !OPENSILVER
        private void ButtonExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            Editor.saveAsPDF();
        } 
#endif
    }
}
