Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives

Namespace OpenSilver.Showcase
    Public NotInheritable Class ItemsControlLoadingBehavior
        Private Sub New()
        End Sub

        Public Shared ReadOnly ShowLoadingOnGeneratingProperty As DependencyProperty =
            DependencyProperty.RegisterAttached(
                "ShowLoadingOnGenerating",
                GetType(Boolean),
                GetType(ItemsControlLoadingBehavior),
                New PropertyMetadata(False, AddressOf OnShowLoadingOnGeneratingChanged)
            )

        Public Shared Sub SetShowLoadingOnGenerating(element As DependencyObject, value As Boolean)
            element.SetValue(ShowLoadingOnGeneratingProperty, value)
        End Sub

        Public Shared Function GetShowLoadingOnGenerating(element As DependencyObject) As Boolean
            Return CBool(element.GetValue(ShowLoadingOnGeneratingProperty))
        End Function

        Private Shared ReadOnly StatusChangedHandlerProperty As DependencyProperty =
            DependencyProperty.RegisterAttached(
                "StatusChangedHandler",
                GetType(EventHandler),
                GetType(ItemsControlLoadingBehavior),
                Nothing
            )

        Private Shared ReadOnly LoadingPopupProperty As DependencyProperty =
            DependencyProperty.RegisterAttached(
                "LoadingPopup",
                GetType(Popup),
                GetType(ItemsControlLoadingBehavior),
                Nothing
            )

        Private Shared Sub OnShowLoadingOnGeneratingChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim itemsControl = TryCast(d, ItemsControl)
            If itemsControl Is Nothing Then Return

            Dim enabled As Boolean = CBool(e.NewValue)
            If enabled Then
                Dim handler As EventHandler = Sub(s, args) OnStatusChanged(itemsControl)
                itemsControl.SetValue(StatusChangedHandlerProperty, handler)
                AddHandler itemsControl.ItemContainerGenerator.StatusChanged, handler
            Else
                Dim handler = TryCast(itemsControl.GetValue(StatusChangedHandlerProperty), EventHandler)
                If handler IsNot Nothing Then
                    RemoveHandler itemsControl.ItemContainerGenerator.StatusChanged, handler
                    itemsControl.ClearValue(StatusChangedHandlerProperty)
                End If

                Dim existing = TryCast(itemsControl.GetValue(LoadingPopupProperty), Popup)
                If existing IsNot Nothing Then
                    existing.IsOpen = False
                    itemsControl.ClearValue(LoadingPopupProperty)
                End If
            End If
        End Sub

        Private Shared Sub OnStatusChanged(itemsControl As ItemsControl)
            Dim status = itemsControl.ItemContainerGenerator.Status

            If status = GeneratorStatus.GeneratingContainers Then
                Dim popup As New Popup With {
                    .Child = New LoadingControl(),
                    .Placement = PlacementMode.Absolute,
                    .IsHitTestVisible = False,
                    .IsOpen = False
                }

                ' Overlay the whole window
                Dim host = TryCast(Application.Current.RootVisual, FrameworkElement)
                popup.Width = host.ActualWidth
                popup.Height = host.ActualHeight

                itemsControl.SetValue(LoadingPopupProperty, popup)
                popup.IsOpen = True
            Else
                Dim popup = TryCast(itemsControl.GetValue(LoadingPopupProperty), Popup)
                If popup IsNot Nothing Then
                    popup.IsOpen = False
                    itemsControl.ClearValue(LoadingPopupProperty)
                End If
            End If
        End Sub
    End Class
End Namespace
