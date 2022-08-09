using CSHTML5;
using CSHTML5.Native.Html.Controls;
using System;

namespace TestNumericTextBox
{
    public class NumericTextBox : HtmlPresenter
    {
        private int _value = 0;

        public NumericTextBox()
        {
            this.Html = @"<input type=""number"" pattern=""[0-9]*"" style=""width:100%;height:100%"">";

            this.Loaded += NumericTextBox_Loaded;
        }

        public int Value
        {
            get
            {
                if (this.DomElement != null) //Note: the DOM element is null if the control has not been added to the visual tree yet.
                {
                    int valueInt;
                    string valueString = OpenSilver.Interop.ExecuteJavaScript("$0.value", this.DomElement).ToString();
                    if (Int32.TryParse(valueString, out valueInt))
                    {
                        _value = valueInt;
                    }
                }
                return _value;
            }
            set
            {
                _value = value;

                if (this.DomElement != null) //Note: the DOM element is null if the control has not been added to the visual tree yet.
                    OpenSilver.Interop.ExecuteJavaScript("$0.value = $1", this.DomElement, _value);
            }
        }

#if SLMIGRATION
        void NumericTextBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
#else
        void NumericTextBox_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
#endif
        {
            // Here, the control has been added to the visual tree, so the DOM element exists. We set the initial value:
            OpenSilver.Interop.ExecuteJavaScript("$0.value = $1", this.DomElement, _value);
        }
    }
}