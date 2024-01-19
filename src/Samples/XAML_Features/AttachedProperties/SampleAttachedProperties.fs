namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Linq
open System.Text
open System.Threading.Tasks
#if SLMIGRATION
open System.Windows
#else
open Windows.UI.Xaml
#endif

type SampleAttachedProperties() =
    static member GetAriaLabel (obj : DependencyObject) =
        obj.GetValue(SampleAttachedProperties.AriaLabelProperty) :?> string

    static member SetAriaLabel(obj : DependencyObject, value : string) =
        obj.SetValue(SampleAttachedProperties.AriaLabelProperty, value)

    static member AriaLabel_MethodToUpdateDom (d : DependencyObject) (newValue : obj) =
        if d :? FrameworkElement then
            let value = if newValue <> null then newValue.ToString() else ""

            // Get a reference to the <div> DOM element used to render the UI element:
            let div = OpenSilver.Interop.GetDiv(d :?> FrameworkElement)

            // Set the "Aria Label" attribute on the <div> via a JavaScript interop call:
            OpenSilver.Interop.ExecuteJavaScript("$0.setAttribute('aria-label', $1)", div, value) |> ignore
            // Note: For documentation related to the commands above, please refer to https://opensilver.net/links/how-to-call-javascript.aspx

    static member AriaLabelProperty =
        DependencyProperty.RegisterAttached("AriaLabel", typeof<string>, typeof<SampleAttachedProperties>,
            PropertyMetadata(defaultValue = null, MethodToUpdateDom = SampleAttachedProperties.AriaLabel_MethodToUpdateDom))
