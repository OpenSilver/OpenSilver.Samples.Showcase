using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class Blazor_DevExpress : UserControl
    {
        public Blazor_DevExpress()
        {
            this.InitializeComponent();

            LoadContent();
        }

        private void LoadContent()
        {
            try
            {
                Content = new DevExpress_Sample();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new TargetInvocationException(ex);
            }
        }
    }
}
