using OpenSilver.Samples.Showcase.Search;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("input", "counter", "control", "buttonspinner")]
public partial class UpDownBase_Demo : ContentControl
{
    public UpDownBase_Demo()
    {
        InitializeComponent();
    }
}

internal class IntUpDown : UpDownBase<int>
{
    ///<summary>
    /// Called by OnSpin when the spin direction is SpinDirection.Increase.
    ///</summary>
    protected override void OnIncrement()
    {
        Value = (Value + 1) % 10;
    }

    ///<summary>
    /// Called by OnSpin when the spin direction is SpinDirection.Increase.
    ///</summary>
    protected override void OnDecrement()
    {
        Value = (Value - 1) % 10;
    }

    ///<summary>
    /// Called by ApplyValue to parse user input.
    ///</summary>
    ///<param name="text">User input.</param>
    ///<returns>Value parsed from user input.</returns>
    protected override int ParseValue(string text)
    {
        return int.Parse(text, CultureInfo.CurrentCulture);
    }

    ///<summary>
    /// Called to render Value for Text template part to display.
    ///</summary>
    ///<returns>Formatted Value.</returns>
    protected override string FormatValue()
    {
        return Value.ToString(CultureInfo.CurrentCulture);
    }
}

internal class DateTimeUpDown : UpDownBase<DateTime>
{
    ///<summary>
    /// Internal constructor.
    ///</summary>
    //internal DateTimeUpDown()
    //{
    //    Value = DateTime.Now;
    //}

    ///<summary>
    /// Called by OnSpin when the spin direction is SpinDirection.Increase.
    ///</summary>
    protected override void OnIncrement()
    {
        try
        {
            Value = Value.AddHours(1);
        }
        catch (ArgumentOutOfRangeException)
        {
            Value = DateTime.MinValue;
        }
    }

    ///<summary>
    /// Called by OnSpin when the spin direction is SpinDirection.Increase.
    ///</summary>
    protected override void OnDecrement()
    {
        try
        {
            Value = Value.AddHours(-1);
        }
        catch (ArgumentOutOfRangeException)
        {
            Value = DateTime.MaxValue;
        }
    }

    ///<summary>
    /// Called by ApplyValue to parse user input.
    ///</summary>
    ///<param name="text">User input.</param>
    ///<returns>Value parsed from user input.</returns>
    protected override DateTime ParseValue(string text)
    {
        return DateTime.Parse(text, CultureInfo.CurrentCulture);
    }

    ///<summary>
    /// Called to render Value for Text template part to display.
    ///</summary>
    ///<returns>Formatted Value.</returns>
    protected override string FormatValue()
    {
        return Value.ToShortTimeString();
    }
}

internal class StringUpDown : UpDownBase<string>
{
    ///<summary>
    /// Internal constructor.
    ///</summary>
    internal StringUpDown()
    {
        Value = "0";
    }

    ///<summary>
    /// Called by OnSpin when the spin direction is SpinDirection.Increase.
    ///</summary>
    protected override void OnIncrement()
    {
        string value = string.IsNullOrEmpty(Value) ? "0" : Value;
        if (value.Length >= 10)
        {
            value = "0";
        }
        else
        {
            value = value + value.Length.ToString(CultureInfo.CurrentCulture);
        }
        Value = value;
    }

    ///<summary>
    /// Called by OnSpin when the spin direction is SpinDirection.Increase.
    ///</summary>
    protected override void OnDecrement()
    {
        string value = string.IsNullOrEmpty(Value) ? "0" : Value;
        if (value.Length <= 1)
        {
            value = "0123456789";
        }
        else
        {
            value = value.Substring(0, value.Length - 1);
        }
        Value = value;
    }

    ///<summary>
    /// Called by ApplyValue to parse user input.
    ///</summary>
    ///<param name="text">User input.</param>
    ///<returns>Value parsed from user input.</returns>
    protected override string ParseValue(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            text = "0";
        }

        if (text.Length > 10)
        {
            text = text.Substring(0, 10);
        }

        return text;
    }

    ///<summary>
    /// Called to render Value for Text template part to display.
    ///</summary>
    ///<returns>Formatted Value.</returns>
    protected override string FormatValue()
    {
        return string.IsNullOrEmpty(Value) ? "0" : Value;
    }
}
