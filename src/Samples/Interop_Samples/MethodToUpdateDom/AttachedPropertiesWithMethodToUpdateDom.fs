namespace OpenSilver.Samples.Showcase

open System.Windows

type AttachedPropertiesWithMethodToUpdateDom() =
    static let ariaLabelProperty =
        DependencyProperty.RegisterAttached(
            "AriaLabel",
            typeof<string>,
            typeof<AttachedPropertiesWithMethodToUpdateDom>,
            PropertyMetadata(defaultValue = null, MethodToUpdateDom = AttachedPropertiesWithMethodToUpdateDom.AriaLabel_MethodToUpdateDom)
        )

    static member GetAriaLabel(obj : DependencyObject) =
        obj.GetValue(ariaLabelProperty) :?> string

    static member SetAriaLabel(obj : DependencyObject, value : string) =
        obj.SetValue(ariaLabelProperty, value)

    /// <summary>
    /// This method is called when the DOM tree is rendered.
    /// </summary>
    /// <param name="d">The dependency object to which the property is attached</param>
    /// <param name="newValue">The new value of the attached property</param>
    static member AriaLabel_MethodToUpdateDom (d : DependencyObject) (newValue : obj) =
        match d with
        | :? FrameworkElement as fe ->
            // Convert the value to string:
            let value = if newValue <> null then newValue.ToString() else ""

            // Get a reference to the <div> DOM element used to render the UI element:
            let div = OpenSilver.Interop.GetDiv(fe)

            // Set the "Aria Label" attribute on the <div> via a JavaScript interop call:
            OpenSilver.Interop.ExecuteJavaScript("$0.setAttribute('aria-label', $1)", div, value) |> ignore

            // Note: for documentation related to the commands above, please refer to https://opensilver.net/links/how-to-call-javascript.aspx
        | _ -> ()
