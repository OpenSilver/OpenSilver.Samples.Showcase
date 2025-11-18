#if WITHBLAZOR
using DevExpress.Blazor.RichEdit; 
#endif
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace OpenSilver.Showcase
{
    public partial class Blazor_DevExpress : UserControl
    {
        public Blazor_DevExpress()
        {
            this.InitializeComponent();

#if WITHBLAZOR
            LoadContent(); 
#endif
        }

#if WITHBLAZOR
        private async void LoadContent()
        {
            try
            {
                //Load DevExpress dlls
                var nav = ServiceLocator.Get<ILazyFeatureNavigator>();
                await nav.EnsureLoadedFromPathAsync(LazyLoadingConstants.DEVEXPRESS_NAME);

                //// Load required js and css files:
                var baseUri = Interop.ExecuteJavaScript("document.baseURI").ToString();
                baseUri = baseUri.EndsWith("/") ? baseUri : baseUri + "/";
                Interop.LoadCssFilesAsync(
                    new string[]
                    {
                        baseUri + "_content/DevExpress.Blazor.Themes/blazing-berry.bs5.min.css",
                        baseUri + "_content/DevExpress.Blazor.RichEdit/dx-blazor-richedit.css"
                    },
                    () =>
                    {
                        //Add the page's content
                        Content = new DevExpress_Sample();
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new TargetInvocationException(ex);
            }
        } 
#endif
    }
}
