namespace OpenSilver.Samples.Showcase

open System
open System.Threading.Tasks
open System.Windows
open System.Windows.Media
open OpenSilver
open CSHTML5.Native.Html.Controls

type MonacoEditor() as this =
    inherit HtmlPresenter()

    static let cdnUrl = "https://unpkg.com/monaco-editor@latest"
    static let iframeId = "monacoEditorIframe"

    let mutable domElement: obj = null
    
    static let codeProperty =
        DependencyProperty.Register(
            "Code",
            typeof<string>,
            typeof<MonacoEditor>,
            PropertyMetadata(String.Empty)
        )
        
    static let languageProperty =
        DependencyProperty.Register(
            "Language",
            typeof<string>,
            typeof<MonacoEditor>,
            PropertyMetadata("plaintext")
        )

    member this.Code
        with get() = this.GetValue(codeProperty) :?> string
        and set(value: string) = this.SetValue(codeProperty, value)
        
    member this.Language
        with get() = this.GetValue(languageProperty) :?> string
        and set(value: string) = this.SetValue(languageProperty, value)
        
    member private this.OnCodeChangedInEditor(newCode: string) =
        this.Code <- newCode

    member private this.domElement
        with get() = domElement
        and set(value) = domElement <- value
    
    //member private this.OnLoaded(sender: obj, e: RoutedEventArgs) =
    //    this.Loaded.RemoveHandler(RoutedEventHandler(this.OnLoaded))
        
    //    domElement <- Interop.GetDiv(this)
        
        //Interop.ExecuteJavaScriptVoidAsync(
        //    $"""
        //        (function () {{
        //            const iframe = document.createElement('iframe');
        //            iframe.setAttribute('id','{iframeId}');
        //            iframe.style='border:none;width:100%;height:100%;overflow:hidden;display:block';

        //            const currentDiv = $0;
        //            currentDiv.appendChild(iframe);

        //            const iframeDoc = iframe.contentDocument || iframe.contentWindow.document;
        //            iframeDoc.open();
        //            const div = `
        //                <html>
        //                    <style>
        //                        body, html {{
        //                            margin: 0;
        //                            padding: 0;
        //                            width: 100%;
        //                            height: 100%;
        //                            overflow:hidden;
        //                        }}
        //                        #monacoContainer {{
        //                            width: 100%;
        //                            height: 100%;
        //                            margin: 0;
        //                            padding: 0;
        //                        }}
        //                    </style>
        //                    <body>
        //                        <div id="monacoContainer"></div>
        //                    </body>
        //                </html>
        //            `;
        //            iframeDoc.write(div);

        //            const monacoLoaderScript = iframeDoc.createElement('script');
        //            monacoLoaderScript.src = '{cdnUrl}/min/vs/loader.js';
        //            monacoLoaderScript.onload = function()
        //            {{
        //                var configScript = iframeDoc.createElement('script');
        //                configScript.type = 'text/javascript';
        //                configScript.text = `
        //                    require.config({{ paths: {{ 'vs': '{cdnUrl}/min/vs' }} }});
        //                    require(['vs/editor/editor.main'], function() {{
        //                        let element=document.getElementById('monacoContainer');
        //                        element.editor = monaco.editor.create(element, {{
        //                                value: `{this.Code}`,
        //                                language: $1,
        //                                theme: 'vs-dark',
        //                                automaticLayout: true,
        //                                scrolling: {{ vertical:'auto' }}
        //                        }});
        //                        element.style.height = '100%';
        //                        element.editor.onDidChangeModelContent((event) => {{
        //                            const fullContent = element.editor.getValue();
        //                            window.updateContentInParent(fullContent);
        //                        }});
        //                    }});
        //                `;
        //                iframeDoc.body.appendChild(configScript);
        //            }};
        //            iframeDoc.body.appendChild(monacoLoaderScript);

        //            const scriptCallback = iframeDoc.createElement('script');
        //            scriptCallback.text = `
        //                    window.setEditorContent = function(message) {{
        //                        let element=document.getElementById('monacoContainer');
        //                        element && element.editor && element.editor.getModel().getValue() != message && element.editor.getModel().setValue(message)
        //                    }};
        //                    window.setEditorLanguage = function(language) {{
        //                        let editor = document.getElementById('monacoContainer')?.editor;
        //                        if (editor) {{
        //                            monaco.editor.setModelLanguage(editor.getModel(), language);
        //                        }}
        //                    }};
        //            `;

        //            iframeDoc.body.appendChild(scriptCallback);
        //            iframeDoc.close();
        //            const cw = document.getElementById('{iframeId}').contentWindow;
        //            cw.updateContentInParent = function(message) {{
        //                $2(message);
        //            }};
        //        }})();
        //    """, domElement, this.Language, Action<string>(this.OnCodeChangedInEditor)) |> ignore            
    
    //do
    //    this.Loaded.AddHandler(RoutedEventHandler(this.OnLoaded))

    static member CodeProperty = codeProperty
    static member LanguageProperty = languageProperty
