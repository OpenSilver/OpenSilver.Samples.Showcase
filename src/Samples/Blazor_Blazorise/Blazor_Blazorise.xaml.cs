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
    public partial class Blazor_Blazorise : UserControl
    {
        public Blazor_Blazorise()
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
                Content = new Blazorise_Sample();
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
