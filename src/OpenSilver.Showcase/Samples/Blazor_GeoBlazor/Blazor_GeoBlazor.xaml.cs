using System;
using System.Reflection;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    public partial class Blazor_GeoBlazor : UserControl
    {
        public Blazor_GeoBlazor()
        {
            this.InitializeComponent();

#if FULLBLAZOR
            LoadContent(); 
#endif
        }

#if FULLBLAZOR
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
