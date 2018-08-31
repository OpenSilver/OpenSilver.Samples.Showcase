using CSHTML5.Extensions.Common;
using kendo_ui_editor.kendo.ui;
using System.Collections.Generic;
using TypeScriptDefinitionsSupport;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Samples.Showcase.Samples.Third_Party.Telerik_KendoUI.Editor
{
    public partial class Editor_Demo : Page
    {
        public Editor_Demo()
        {
            InitializeComponent();

            Loaded += Editor_Demo_Loaded;
        }

        private async void Editor_Demo_Loaded(object sender, RoutedEventArgs e)
        {
            //------------
            // Wait until Editor's underlying JS instance has been loaded
            //------------
            LoadingPleaseWaitMessage.Visibility = Visibility.Visible;
            await Editor.JSInstanceLoaded;
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

        private void ButtonExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            Editor.saveAsPDF();
        }
    }
}
