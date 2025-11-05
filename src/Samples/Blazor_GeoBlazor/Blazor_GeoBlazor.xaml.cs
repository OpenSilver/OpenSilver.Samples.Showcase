using System;
using System.Reflection;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class Blazor_GeoBlazor : UserControl
    {
        public Blazor_GeoBlazor()
        {
            this.InitializeComponent();

#if WITHBLAZOR
            LoadContent(); 
#endif
        }

#if WITHBLAZOR
        private void LoadContent()
        {
            try
            {
                //Content = new GeoBlazor_Sample();
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
