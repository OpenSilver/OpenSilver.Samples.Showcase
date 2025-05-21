Imports System.Windows
Imports CSHTML5.Native.Html.Controls

Namespace OpenSilver.Samples.Showcase

    Public Class ToastUiEditor
        Inherits HtmlPresenter

        Private Const CdnUrl As String = "https://uicdn.toast.com/editor/latest"
        Private _domElement As Object

#Region "Content"
        Private _contentInEditor As String = String.Empty

        Public Property Content() As String
            Get
                Return DirectCast(GetValue(ContentProperty), String)
            End Get
            Set(value As String)
                SetValue(ContentProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ContentProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(Content), GetType(String), GetType(ToastUiEditor), New PropertyMetadata(String.Empty, AddressOf OnContentChanged))

        Private Shared Sub OnContentChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim editor = DirectCast(d, ToastUiEditor)
            Dim newContent = DirectCast(e.NewValue, String)

            If editor._domElement IsNot Nothing AndAlso newContent <> editor._contentInEditor Then
                editor._contentInEditor = newContent
                Interop.ExecuteJavaScriptVoidAsync(
                    "if($0.editor) { $0.editor.setMarkdown($1); }",
                    editor._domElement,
                    newContent
                )
            End If
        End Sub
#End Region

        Public Sub New()
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Private Async Sub OnLoaded(sender As Object, e As RoutedEventArgs)
            RemoveHandler Loaded, AddressOf OnLoaded

            If Not Await FileLoader.TryLoadCssFile($"{CdnUrl}/toastui-editor.min.css") OrElse _
               Not Await FileLoader.TryLoadJavaScriptFile($"{CdnUrl}/toastui-editor-all.min.js") Then
                Return
            End If

            _domElement = Interop.GetDiv(Me)

            Interop.ExecuteJavaScriptVoidAsync(
$"$0.editor = new toastui.Editor({{
    el: $0.firstChild,
    initialValue: $1,
    previewStyle: 'vertical',
    initialEditType: 'wysiwyg',
    theme: 'dark'
}});

$0.editor.on('change', () => {{
    const value = $0.editor.getMarkdown();
    $2(value);
}});", _domElement, Content, DirectCast(AddressOf OnContentChangedInEditor, Action(Of String)))
        End Sub

        Private Sub OnContentChangedInEditor(newContent As String)
            _contentInEditor = newContent
            Content = newContent
        End Sub
    End Class

End Namespace
