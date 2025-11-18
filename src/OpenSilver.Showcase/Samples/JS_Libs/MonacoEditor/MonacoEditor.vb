Imports System.Windows
Imports CSHTML5.Native.Html.Controls

Namespace OpenSilver.Showcase

    Public Class MonacoEditor
        Inherits HtmlPresenter

        Private Const CdnUrl As String = "https://unpkg.com/monaco-editor@latest"
        Private Const IframeId As String = "monacoEditorIframe"
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
                Interop.ExecuteJavaScriptVoid($"document.getElementById('{IframeId}').contentWindow.setEditorContent(`{e.NewValue}`)")
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
                Interop.ExecuteJavaScriptVoid($"document.getElementById('{IframeId}').contentWindow.setEditorLanguage('{e.NewValue}')")
            End If
        End Sub
#End Region

        Public Sub New()
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Private Sub OnLoaded(sender As Object, e As RoutedEventArgs)
            RemoveHandler Loaded, AddressOf OnLoaded

            _domElement = Interop.GetDiv(Me)

            Interop.ExecuteJavaScriptVoidAsync(
$"
                (function () {{
                    const iframe = document.createElement('iframe');
                    iframe.setAttribute('id','{IframeId}');
                    iframe.style='border:none;width:100%;height:100%;overflow:hidden;display:block';

                    const currentDiv = $0;
                    currentDiv.appendChild(iframe);

                    const iframeDoc = iframe.contentDocument || iframe.contentWindow.document;
                    iframeDoc.open();
                    const div = `
                        <html>
                            <style>
                                body, html {{
                                    margin: 0;
                                    padding: 0;
                                    width: 100%;
                                    height: 100%;
                                    overflow:hidden;
                                }}
                                #monacoContainer {{
                                    width: 100%;
                                    height: 100%;
                                    margin: 0;
                                    padding: 0;
                                }}
                            </style>
                            <body>
                                <div id=""monacoContainer""></div>
                            </body>
                        </html>
                    `;
                    iframeDoc.write(div);

                    const monacoLoaderScript = iframeDoc.createElement('script');
                    monacoLoaderScript.src = '{CdnUrl}/min/vs/loader.js';
                    monacoLoaderScript.onload = function()
                    {{
                        var configScript = iframeDoc.createElement('script');
                        configScript.type = 'text/javascript';
                        configScript.text = `
                            require.config({{ paths: {{ 'vs': '{CdnUrl}/min/vs' }} }});
                            require(['vs/editor/editor.main'], function() {{
                                let element=document.getElementById('monacoContainer');
                                element.editor = monaco.editor.create(element, {{
                                        value: \`{Code}\`,
                                        language: $1,
                                        theme: 'vs-dark',
                                        automaticLayout: true,
                                        scrolling: {{ vertical:'auto' }}
                                }});
                                element.style.height = '100%';
                                element.editor.onDidChangeModelContent((event) => {{
                                    const fullContent = element.editor.getValue();
                                    window.updateContentInParent(fullContent);
                                }});
                            }});
                        `;
                        iframeDoc.body.appendChild(configScript);
                    }};
                    iframeDoc.body.appendChild(monacoLoaderScript);

                    const scriptCallback = iframeDoc.createElement('script');
                    scriptCallback.text = `
                            window.setEditorContent = function(message) {{
                                let element=document.getElementById('monacoContainer');
                                element && element.editor && element.editor.getModel().getValue() != message && element.editor.getModel().setValue(message)
                            }};
                            window.setEditorLanguage = function(language) {{
                                let editor = document.getElementById('monacoContainer')?.editor;
                                if (editor) {{
                                    monaco.editor.setModelLanguage(editor.getModel(), language);
                                }}
                            }};
                    `;

                    iframeDoc.body.appendChild(scriptCallback);
                    iframeDoc.close();
                    const cw = document.getElementById('{IframeId}').contentWindow;
                    cw.updateContentInParent = function(message) {{
                        $2(message);
                    }};
                }})();
", _domElement, Language, DirectCast(AddressOf OnCodeChangedInEditor, Action(Of String)))
        End Sub

        Private Sub OnCodeChangedInEditor(newCode As String)
            Code = newCode
        End Sub
    End Class

End Namespace
