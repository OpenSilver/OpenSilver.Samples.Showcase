using CSHTML5.Native.Html.Controls;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace OpenSilver.Samples.Showcase;

public class ToastUiEditor : HtmlPresenter
{
    private const string CdnUrl = "https://uicdn.toast.com/editor/latest";
    private static bool _isJsLibLoaded;

    private object _domElement;

    #region Content
    private string _contentInEditor = string.Empty;

    public string Content
    {
        get => (string)GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(string), typeof(ToastUiEditor), new PropertyMetadata(string.Empty, OnContentChanged));

    private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var editor = (ToastUiEditor)d;
        var newContent = (string)e.NewValue;

        if (editor._domElement != null && newContent != editor._contentInEditor)
        {
            editor._contentInEditor = newContent;
            Interop.ExecuteJavaScriptVoidAsync(
                "if($0.editor) { $0.editor.setMarkdown($1); }",
                editor._domElement,
                newContent
            );
        }
    }
    #endregion

    public ToastUiEditor()
    {
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        await LoadJSLibrary();
        _domElement = Interop.GetDiv(this);

        Interop.ExecuteJavaScriptAsync($$"""
            $0.editor = new toastui.Editor({
                el: $0.firstChild,
                initialValue: $1,
                previewStyle: 'vertical',
                initialEditType: 'wysiwyg',
                theme: 'dark'
            });
            
            $0.editor.on('change', () => {
                const value = $0.editor.getMarkdown();
                $2(value);
            });
        """, _domElement, Content, (Action<string>)OnContentChangedInEditor);
    }

    private void OnContentChangedInEditor(string newContent)
    {
        _contentInEditor = newContent;
        Content = newContent;
    }

    private static async Task LoadJSLibrary()
    {
        if (!_isJsLibLoaded)
        {
            await Interop.LoadCssFile($"{CdnUrl}/toastui-editor.min.css");
            await Interop.LoadJavaScriptFile($"{CdnUrl}/toastui-editor-all.min.js");
            _isJsLibLoaded = true;
        }
    }
}
