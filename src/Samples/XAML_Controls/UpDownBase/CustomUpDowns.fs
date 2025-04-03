namespace OpenSilver.Samples.Showcase

open System
open System.Globalization
open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search


/// <summary>
/// Called by OnSpin when the spin direction is SpinDirection.Increase.
/// </summary>
type internal IntUpDown() =
    inherit UpDownBase<int>()

    /// <summary>
    /// Called by OnSpin when the spin direction is SpinDirection.Increase.
    /// </summary>
    override this.OnIncrement() =
        this.Value <- (this.Value + 1) % 10

    /// <summary>
    /// Called by OnSpin when the spin direction is SpinDirection.Increase.
    /// </summary>
    override this.OnDecrement() =
        this.Value <- (this.Value - 1) % 10

    /// <summary>
    /// Called by ApplyValue to parse user input.
    /// </summary>
    /// <param name="text">User input.</param>
    /// <returns>Value parsed from user input.</returns>
    override this.ParseValue(text: string) =
        Int32.Parse(text, CultureInfo.CurrentCulture)

    /// <summary>
    /// Called to render Value for Text template part to display.
    /// </summary>
    /// <returns>Formatted Value.</returns>
    override this.FormatValue() =
        this.Value.ToString(CultureInfo.CurrentCulture)

/// <summary>
/// UpDown control for DateTime values.
/// </summary>
type internal DateTimeUpDown() =
    inherit UpDownBase<DateTime>()

    /// <summary>
    /// Called by OnSpin when the spin direction is SpinDirection.Increase.
    /// </summary>
    override this.OnIncrement() =
        try
            this.Value <- this.Value.AddHours(1.0)
        with :? ArgumentOutOfRangeException ->
            this.Value <- DateTime.MinValue

    /// <summary>
    /// Called by OnSpin when the spin direction is SpinDirection.Increase.
    /// </summary>
    override this.OnDecrement() =
        try
            this.Value <- this.Value.AddHours(-1.0)
        with :? ArgumentOutOfRangeException ->
            this.Value <- DateTime.MaxValue

    /// <summary>
    /// Called by ApplyValue to parse user input.
    /// </summary>
    /// <param name="text">User input.</param>
    /// <returns>Value parsed from user input.</returns>
    override this.ParseValue(text: string) =
        DateTime.Parse(text, CultureInfo.CurrentCulture)

    /// <summary>
    /// Called to render Value for Text template part to display.
    /// </summary>
    /// <returns>Formatted Value.</returns>
    override this.FormatValue() =
        this.Value.ToShortTimeString()

/// <summary>
/// UpDown control for string values.
/// </summary>
type internal StringUpDown() as this =
    inherit UpDownBase<string>()

    /// <summary>
    /// Internal constructor.
    /// </summary>
    do this.Value <- "0"

    /// <summary>
    /// Called by OnSpin when the spin direction is SpinDirection.Increase.
    /// </summary>
    override this.OnIncrement() =
        let value = if String.IsNullOrEmpty(this.Value) then "0" else this.Value
        this.Value <-
            if value.Length >= 10 then "0"
            else value + value.Length.ToString(CultureInfo.CurrentCulture)

    /// <summary>
    /// Called by OnSpin when the spin direction is SpinDirection.Increase.
    /// </summary>
    override this.OnDecrement() =
        let value = if String.IsNullOrEmpty(this.Value) then "0" else this.Value
        this.Value <-
            if value.Length <= 1 then "0123456789"
            else value.Substring(0, value.Length - 1)

    /// <summary>
    /// Called by ApplyValue to parse user input.
    /// </summary>
    /// <param name="text">User input.</param>
    /// <returns>Value parsed from user input.</returns>
    override this.ParseValue(text: string) =
        let mutable text = if String.IsNullOrEmpty(text) then "0" else text
        if text.Length > 10 then
            text <- text.Substring(0, 10)
        text

    /// <summary>
    /// Called to render Value for Text template part to display.
    /// </summary>
    /// <returns>Formatted Value.</returns>
    override this.FormatValue() =
        if String.IsNullOrEmpty(this.Value) then "0" else this.Value
