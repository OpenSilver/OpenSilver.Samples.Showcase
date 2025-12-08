using CSHTML5.Native.Html.Controls;
using System;
using System.Windows;

namespace OpenSilver.Samples.Showcase;

public class MonacoEditor : HtmlPresenter
{
    private const string CdnUrl = "https://unpkg.com/monaco-editor@latest";
    private const string IframeId = "monacoEditorIframe";

    private object _domElement;

    #region Code
    public string Code
    {
        get => (string)GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }

    public static readonly DependencyProperty CodeProperty =
        DependencyProperty.Register(nameof(Code), typeof(string), typeof(MonacoEditor), new PropertyMetadata(string.Empty, OnCodeChanged));

    private static void OnCodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var editor = (MonacoEditor)d;
        if (editor._domElement != null)
        {
            Interop.ExecuteJavaScriptVoid($"document.getElementById('{IframeId}').contentWindow.setEditorContent(`{e.NewValue}`)");
        }
    }
    #endregion

    #region Language
    public new string Language
    {
        get => (string)GetValue(LanguageProperty);
        set => SetValue(LanguageProperty, value);
    }

    public static new readonly DependencyProperty LanguageProperty =
        DependencyProperty.Register(nameof(Language), typeof(string), typeof(MonacoEditor), new PropertyMetadata("plaintext", OnLanguageChanged));

    private static void OnLanguageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var editor = (MonacoEditor)d;
        if (editor._domElement != null)
        {
            Interop.ExecuteJavaScriptVoid($"document.getElementById('{IframeId}').contentWindow.setEditorLanguage('{e.NewValue}')");
        }
    }
    #endregion

    public MonacoEditor()
    {
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        _domElement = Interop.GetDiv(this);

        Interop.ExecuteJavaScriptVoidAsync($$"""
                (function () {
                    const iframe = document.createElement('iframe');
                    iframe.setAttribute('id','{{IframeId}}');
                    iframe.style='border:none;width:100%;height:100%;overflow:hidden;display:block';

                    const currentDiv = $0;
                    currentDiv.appendChild(iframe);

                    const iframeDoc = iframe.contentDocument || iframe.contentWindow.document;
                    iframeDoc.open();
                    const div = `
                        <html>
                            <style>
                                body, html {
                                    margin: 0;
                                    padding: 0;
                                    width: 100%;
                                    height: 100%;
                                    overflow:hidden;
                                }
                                #monacoContainer {
                                    width: 100%;
                                    height: 100%;
                                    margin: 0;
                                    padding: 0;
                                }
                            </style>
                            <body>
                                <div id="monacoContainer"></div>
                            </body>
                        </html>
                    `;
                    iframeDoc.write(div);

                    const monacoLoaderScript = iframeDoc.createElement('script');
                    monacoLoaderScript.src = '{{CdnUrl}}/min/vs/loader.js';
                    monacoLoaderScript.onload = function()
                    {
                        var configScript = iframeDoc.createElement('script');
                        configScript.type = 'text/javascript';
                        configScript.text = `
                            require.config({ paths: { 'vs': '{{CdnUrl}}/min/vs' } });
                            require(['vs/editor/editor.main'], function() {
                                let element=document.getElementById('monacoContainer');
                                element.editor = monaco.editor.create(element, {
                                        value: \`{{Code}}\`,
                                        language: $1,
                                        theme: 'vs-dark',
                                        automaticLayout: true,
                                        scrolling: { vertical:'auto' }
                                });
                                element.style.height = '100%';
                                element.editor.onDidChangeModelContent((event) => {
                                    const fullContent = element.editor.getValue();
                                    window.updateContentInParent(fullContent);
                                });
                            });
                        `;
                        iframeDoc.body.appendChild(configScript);
                    };
                    iframeDoc.body.appendChild(monacoLoaderScript);

                    const scriptCallback = iframeDoc.createElement('script');
                    scriptCallback.text = `
                            window.setEditorContent = function(message) {
                                let element=document.getElementById('monacoContainer');
                                element && element.editor && element.editor.getModel().getValue() != message && element.editor.getModel().setValue(message)
                            };
                            window.setEditorLanguage = function(language) {
                                let editor = document.getElementById('monacoContainer')?.editor;
                                if (editor) {
                                    monaco.editor.setModelLanguage(editor.getModel(), language);
                                }
                            };
                    `;

                    iframeDoc.body.appendChild(scriptCallback);
                    iframeDoc.close();
                    const cw = document.getElementById('{{IframeId}}').contentWindow;
                    cw.updateContentInParent = function(message) {
                        $2(message);
                    };
                })();
""", _domElement, Language, (Action<string>)OnCodeChangedInEditor);
    }

    private void OnCodeChangedInEditor(string newCode)
    {
        Code = newCode;
    }
}
