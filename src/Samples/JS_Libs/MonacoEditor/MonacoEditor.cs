using CSHTML5.Native.Html.Controls;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace OpenSilver.Samples.Showcase;

public class MonacoEditor : HtmlPresenter
{
    private const string CdnUrl = "https://unpkg.com/monaco-editor@latest/min/vs";
    private static bool _isJsLibLoaded;

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
            Interop.ExecuteJavaScriptVoidAsync(
                "if($0.editor) { $0.editor.setValue($1); }",
                editor._domElement,
                e.NewValue
            );
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
            Interop.ExecuteJavaScriptVoidAsync(
                "if($0.editor) { monaco.editor.setModelLanguage($0.editor.getModel(), $1); }",
                editor._domElement,
                e.NewValue
            );
        }
    }
    #endregion

    public MonacoEditor()
    {
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        await LoadJSLibrary();
        _domElement = Interop.GetDiv(this);

        Html = $"<div style='width:100%;height:{Height}px;display:block'></div>";

        Interop.ExecuteJavaScriptAsync($$"""
            require.config({
                paths: { 'vs': '{{CdnUrl}}' }
            });                
            require(['vs/editor/editor.main'], function() {
                    $0.editor = monaco.editor.create($0.firstChild.firstChild, {
                        value: $1,
                        language: $2,
                        theme: 'vs-dark',
                        minimap: {
                            enabled: false
                        },
                        automaticLayout: true
                    });

                    $0.editor.onDidChangeModelContent((e) => {
                        const value = $0.editor.getValue();
                        $3(value);
                    });
                });
        """, _domElement, Code, Language, (Action<string>)OnCodeChangedInEditor);
    }

    private void OnCodeChangedInEditor(string newCode)
    {
        Code = newCode;
    }

    private static async Task LoadJSLibrary()
    {
        if (!_isJsLibLoaded)
        {
            await Interop.LoadJavaScriptFile($"{CdnUrl}/loader.js");
            _isJsLibLoaded = true;
        }
    }
}
