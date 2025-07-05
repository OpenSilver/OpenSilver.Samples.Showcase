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
                Content = new Radzen_Sample();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new TargetInvocationException(ex);
            }
        }
    }
}
