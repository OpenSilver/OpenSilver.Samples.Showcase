using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    public partial class Blazor_Syncfusion : UserControl
    {
        public Blazor_Syncfusion()
        {
            this.InitializeComponent();

            LoadContent();
        }

        private async void LoadContent()
        {
            try
            {
                // Load required js and css files:
                var baseUri = Interop.ExecuteJavaScript("document.baseURI").ToString();
                var url = (baseUri.EndsWith("/") ? baseUri : baseUri + "/") +
                          "_content/Syncfusion.Blazor.Core/scripts/syncfusion-blazor.min.js";
                await Interop.LoadJavaScriptFile(url);
                url = (baseUri.EndsWith("/") ? baseUri : baseUri + "/") +
                          "_content/Syncfusion.Blazor.Themes/bootstrap5.css";
                await Interop.LoadCssFile(url);

                //Load Syncfusion dlls
                var nav = ServiceLocator.Get<ILazyFeatureNavigator>();
                await nav.EnsureLoadedFromPathAsync(LazyLoadingConstants.SYNCFUSION_NAME);
                //await nav.NavigateToAsync(LazyLoadingConstants.SYNCFUSION_NAME);

                //Add the page's content
                Content = new Syncfusion_Sample();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new TargetInvocationException(ex);
            }
        }
    }
}
