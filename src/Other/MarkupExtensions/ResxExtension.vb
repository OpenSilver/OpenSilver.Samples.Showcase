Imports System.Windows.Markup

Namespace OpenSilver.Samples.Showcase

    <ContentProperty("Key")>
    Public Class ResxExtension
        Inherits MarkupExtension

        Public Property Key As String

        Public Overrides Function ProvideValue(serviceProvider As IServiceProvider) As Object
            Return SampleResourceFile.ResourceManager.GetString(Key)
        End Function
    End Class

End Namespace
