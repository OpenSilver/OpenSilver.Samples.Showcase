
#If SLMIGRATION
Imports System.Windows
#Else
using Windows.UI.Xaml;
#End If

Namespace Global.OpenSilver.Samples.VBShowcase
    Public Module SampleAttachedProperties
        Public Function GetAriaLabel(ByVal obj As DependencyObject) As String
            Return CStr(obj.GetValue(AriaLabelProperty))
        End Function

        Public Sub SetAriaLabel(ByVal obj As DependencyObject, ByVal value As String)
            obj.SetValue(AriaLabelProperty, value)
        End Sub

        Public ReadOnly AriaLabelProperty As DependencyProperty = DependencyProperty.RegisterAttached(name:="AriaLabel", propertyType:=GetType(String), ownerType:=GetType(SampleAttachedProperties), defaultMetadata:=New PropertyMetadata(defaultValue:=Nothing) With {
.MethodToUpdateDom = AddressOf AriaLabel_MethodToUpdateDom
})

        ''' <summary>
        ''' This method is called when the DOM tree is rendered.
        ''' </summary>
        ''' <paramname="d">The dependency object to which the property is attached</param>
        ''' <paramname="newValue">The new value of the attached property</param>
        Public Sub AriaLabel_MethodToUpdateDom(ByVal d As DependencyObject, ByVal newValue As Object)
            If TypeOf d Is FrameworkElement Then
                ' Convert the value to string:
                Dim value As String = (If(newValue IsNot Nothing, newValue.ToString(), ""))

                ' Get a reference to the <div> DOM element used to render the UI element:
                Dim div = Interop.GetDiv(CType(d, FrameworkElement))

                ' Set the "Aria Label" attribute on the <div> via a JavaScript interop call:
                Interop.ExecuteJavaScript("$0.setAttribute('aria-label', $1)", div, value)

                'Note: for documentation related to the commands above, please refer to https://opensilver.net/links/how-to-call-javascript.aspx
            End If
        End Sub

    End Module
End Namespace
