using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class Blazor_MudBlazor : UserControl
    {
        public Blazor_MudBlazor()
        {
            this.InitializeComponent();

            LoadContent();
        }

        private void LoadContent()
        {
            try
            {
                Assembly assemblySample = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .FirstOrDefault(a => a.GetName().Name == "OpenSilver.Samples.Showcase.MudBlazor");
                Type type = assemblySample.GetType("OpenSilver.Samples.Showcase.MudBlazor_Sample");
                object content = Activator.CreateInstance(type);
                Content = (UIElement)content;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new TargetInvocationException(ex);
            }
        }
    }
}
