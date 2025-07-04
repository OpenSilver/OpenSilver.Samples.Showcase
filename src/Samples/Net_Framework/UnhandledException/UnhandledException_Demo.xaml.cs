using OpenSilver.Samples.Showcase.Search;
using System;
using System.Windows;
using System.Windows.Controls;

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
            InitializeComponent();
        }

        private void ButtonThrowException_Click(object sender, RoutedEventArgs e)
        {
            throw new Exception("This exception was thrown outside of a Try/Catch statement and handled using UnhandledException");
        }

        private static void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            string exceptionStackMessages = "";
            Exception ex = e.ExceptionObject;
            string spacing = "  ";

            while (ex != null)
            {
                exceptionStackMessages += Environment.NewLine + spacing + "-" + ex.GetType().Name + ": " + ex.Message;
                spacing += "  ";
                ex = ex.InnerException;
            }

            MessageBox.Show(exceptionStackMessages, "Received an unhandled Exception");
        }
    }
}
