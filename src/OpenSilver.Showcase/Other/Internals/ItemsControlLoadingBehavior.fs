namespace OpenSilver.Showcase

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Controls.Primitives

type ItemsControlLoadingBehavior() =

    static let statusChangedHandlerProperty =
        DependencyProperty.RegisterAttached(
            "StatusChangedHandler",
            typeof<bool>,
            typeof<ItemsControlLoadingBehavior>,
            null
        )

    static let loadingPopupProperty =
        DependencyProperty.RegisterAttached(
            "LoadingPopup",
            typeof<Popup>,
            typeof<ItemsControlLoadingBehavior>,
            null
        )

    static let rec onStatusChanged (itemsControl: ItemsControl) =
        match itemsControl.ItemContainerGenerator.Status with
        | GeneratorStatus.GeneratingContainers ->
            let popup = Popup()
            popup.Child <- new LoadingControl()
            popup.Placement <- PlacementMode.Absolute
            popup.IsHitTestVisible <- false
            popup.IsOpen <- false

            let host = Application.Current.RootVisual :?> FrameworkElement
            popup.Width <- host.ActualWidth
            popup.Height <- host.ActualHeight

            itemsControl.SetValue(loadingPopupProperty, popup)
            popup.IsOpen <- true
        | _ ->
            match itemsControl.GetValue(loadingPopupProperty) with
            | :? Popup as popup ->
                popup.IsOpen <- false
                itemsControl.ClearValue(loadingPopupProperty)
            | _ -> ()

    static let ShowLoadingOnGeneratingProperty =
        DependencyProperty.RegisterAttached(
            "ShowLoadingOnGenerating",
            typeof<bool>,
            typeof<ItemsControlLoadingBehavior>,
            PropertyMetadata(false, PropertyChangedCallback(fun d e ->
                match d with
                | :? ItemsControl as ic ->
                    let enabled = e.NewValue :?> bool
                    if enabled then
                        let handler = EventHandler(fun _ _ -> onStatusChanged ic)
                        ic.SetValue(statusChangedHandlerProperty, handler)
                        ic.ItemContainerGenerator.StatusChanged.AddHandler(handler)
                    else
                        match ic.GetValue(statusChangedHandlerProperty) with
                        | :? EventHandler as handler ->
                            ic.ItemContainerGenerator.StatusChanged.RemoveHandler(handler)
                            ic.ClearValue(statusChangedHandlerProperty)
                        | _ -> ()

                        match ic.GetValue(loadingPopupProperty) with
                        | :? Popup as popup ->
                            popup.IsOpen <- false
                            ic.ClearValue(loadingPopupProperty)
                        | _ -> ()
                | _ -> ()
            ))
        )

    static member SetShowLoadingOnGenerating (element: DependencyObject, value: bool) =
        element.SetValue(ShowLoadingOnGeneratingProperty, value)

    static member GetShowLoadingOnGenerating (element: DependencyObject) =
        element.GetValue(ShowLoadingOnGeneratingProperty) :?> bool
