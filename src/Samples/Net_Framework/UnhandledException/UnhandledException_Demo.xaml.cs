using OpenSilver.Samples.Showcase.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    [SearchKeywords("error handling", "exception", "debugging", "logging")]
    public partial class UnhandledException_Demo : UserControl
    {
        int _exceptionsReceived = 0;
        static UnhandledException_Demo()
        {
            Application.Current.UnhandledException += OnUnhandledException;
        }

        public UnhandledException_Demo()
        {
            this.InitializeComponent();
        }

        private void ButtonThrowException_Click(object sender, RoutedEventArgs e)
        {
            throw new Exception("This exception was thrown outside of a Try/Catch statement and handled using UnhandledException");
        }

        private static void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            string exceptionStackMessages = "Received an unhandled Exception: ";
            Exception ex = e.ExceptionObject;
            string spacing = "  ";
            while (ex != null)
            {
                exceptionStackMessages += Environment.NewLine + spacing + "-" + ex.GetType().Name + ": " + ex.Message;
                spacing += "  ";
                ex = ex.InnerException;
            }
            MessageBox.Show(exceptionStackMessages);
        }
    }
}
