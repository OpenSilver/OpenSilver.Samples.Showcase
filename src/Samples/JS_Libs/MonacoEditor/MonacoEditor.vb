Imports System.Windows
Imports CSHTML5.Native.Html.Controls

Namespace OpenSilver.Samples.Showcase

    Public Class MonacoEditor
        Inherits HtmlPresenter

        Private Const CdnUrl As String = "https://unpkg.com/monaco-editor@latest/min/vs"
        Private Shared _isJsLibLoaded As Boolean
        Private _domElement As Object

#Region "Code"
        Public Property Code() As String
            Get
                Return DirectCast(GetValue(CodeProperty), String)
            End Get
            Set(value As String)
                SetValue(CodeProperty, value)
            End Set
        End Property

        Public Shared ReadOnly CodeProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(Code), GetType(String), GetType(MonacoEditor), New PropertyMetadata(String.Empty, AddressOf OnCodeChanged))

        Private Shared Sub OnCodeChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim editor = DirectCast(d, MonacoEditor)
            If editor._domElement IsNot Nothing Then
                Interop.ExecuteJavaScriptVoidAsync(
                    "if($0.editor) { $0.editor.setValue($1); }",
                    editor._domElement,
                    e.NewValue
                )
            End If
        End Sub
#End Region

#Region "Language"
        Public Shadows Property Language() As String
            Get
                Return DirectCast(GetValue(LanguageProperty), String)
            End Get
            Set(value As String)
                SetValue(LanguageProperty, value)
            End Set
        End Property

        Public Shared Shadows ReadOnly LanguageProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(Language), GetType(String), GetType(MonacoEditor), New PropertyMetadata("plaintext", AddressOf OnLanguageChanged))

        Private Shared Sub OnLanguageChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim editor = DirectCast(d, MonacoEditor)
            If editor._domElement IsNot Nothing Then
                Interop.ExecuteJavaScriptVoidAsync(
                    "if($0.editor) { monaco.editor.setModelLanguage($0.editor.getModel(), $1); }",
                    editor._domElement,
                    e.NewValue
                )
            End If
        End Sub
#End Region

        Public Sub New()
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Private Async Sub OnLoaded(sender As Object, e As RoutedEventArgs)
            RemoveHandler Loaded, AddressOf OnLoaded

            Await LoadJSLibrary()
            _domElement = Interop.GetDiv(Me)

            Html = $"<div style='width:100%;height:{Height}px;display:block'></div>"

            Interop.ExecuteJavaScriptAsync(
$"require.config({{
    paths: {{ 'vs': '{CdnUrl}' }}
}});                
require(['vs/editor/editor.main'], function() {{
        $0.editor = monaco.editor.create($0.firstChild.firstChild, {{
            value: $1,
            language: $2,
            theme: 'vs-dark',
            minimap: {{
                enabled: false
            }},
            automaticLayout: true
        }});

        $0.editor.onDidChangeModelContent((e) => {{
            const value = $0.editor.getValue();
            $3(value);
        }});
    }});", _domElement, Code, Language, DirectCast(AddressOf OnCodeChangedInEditor, Action(Of String)))
        End Sub

        Private Sub OnCodeChangedInEditor(newCode As String)
            Code = newCode
        End Sub

        Private Shared Async Function LoadJSLibrary() As Task
            If Not _isJsLibLoaded Then
                Await Interop.LoadJavaScriptFile($"{CdnUrl}/loader.js")

                _isJsLibLoaded = True
            End If
        End Function
    End Class

End Namespace
