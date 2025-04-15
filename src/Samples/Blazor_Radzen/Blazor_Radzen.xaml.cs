using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class Blazor_Radzen : UserControl
    {
        public Blazor_Radzen()
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
                    .FirstOrDefault(a => a.GetName().Name == "OpenSilver.Samples.Showcase.Radzen");

                Type type = assemblySample.GetType("OpenSilver.Samples.Showcase.Radzen_Sample");
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
