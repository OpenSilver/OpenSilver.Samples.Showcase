using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace OpenSilver.Samples.Showcase
{
    public partial class Blazor_Radzen : UserControl
    {

        public Blazor_Radzen()
        {
            this.InitializeComponent();

            this.MyBusyIndicator.IsBusy = true;

            Task.Run(() => LoadContentAssembly());
        }

        private async void LoadContentAssembly()
        {
            try
            {
                await LazyLoadingHelper.AssemblyLoader.LoadAssembliesAsync(
                    new string[] { 
                        "Radzen.Blazor.wasm", 
                        "OpenSilver.Samples.Showcase.Radzen.wasm" 
                    });

                Assembly assemblySample = LazyLoadingHelper.GetLoadedAssembly("OpenSilver.Samples.Showcase.Radzen");
                Type type = assemblySample.GetType("OpenSilver.Samples.Showcase.Radzen_Sample");
                object content = Activator.CreateInstance(type);
                this.Content = (UIElement)content;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new TargetInvocationException(ex);
            }
            finally
            {
                this.MyBusyIndicator.IsBusy = false;
            }
        }
    }
}
