using CSHTML5.Internal;
using CSHTML5.Native.Html.Controls;
using OpenSilver;
using System.Windows;

namespace TestNumericTextBox;

public class NumericTextBox : HtmlPresenter
{
    private int _value = 0;
    private object _domElement;

    public NumericTextBox()
    {
        Html = "<input type='number' pattern='[0-9]*' style='width:100%;height:100%'>";

        Loaded += NumericTextBox_Loaded;
    }

    public int Value
    {
        get
        {
            if (_domElement != null) //Note: the DOM element is null if the control has not been added to the visual tree yet.
            {
                var id = (_domElement as INTERNAL_HtmlDomElementReference).UniqueIdentifier;
                string valueString = Interop.ExecuteJavaScriptGetResult<string>($"{id}.firstChild.firstChild.value");
                if (int.TryParse(valueString, out int valueInt))
                {
                    _value = valueInt;
                }
            }
            return _value;
        }
        set
        {
            _value = value;

            if (_domElement != null) //Note: the DOM element is null if the control has not been added to the visual tree yet.
                UpdateValue();
        }
    }

    void NumericTextBox_Loaded(object sender, RoutedEventArgs e)
    {
        _domElement = Interop.GetDiv(this);

        // Here, the control has been added to the visual tree, so the DOM element exists. We set the initial value:
        UpdateValue();
    }

    private void UpdateValue()
    {
        Interop.ExecuteJavaScriptVoidAsync("$0.firstChild.firstChild.value = $1", _domElement, _value);
    }
}