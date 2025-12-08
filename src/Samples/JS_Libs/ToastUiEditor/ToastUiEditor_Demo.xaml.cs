using OpenSilver.Samples.Showcase.Search;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("toastui", "editor", "markdown", "wysiwyg", "richtext", "js", "javascript", "interop")]
public partial class ToastUiEditor_Demo : UserControl
{
    public ToastUiEditor_Demo()
    {
        InitializeComponent();

        toastUiEditor.Content = """
            # Getting Started with Toast UI Editor

            ## Features

            - **Markdown support**: You can write documents in markdown syntax
            - **WYSIWYG editor**: You can use the WYSIWYG editor to create documents
            - **Syntax highlighting**: Code blocks are syntax highlighted

            ### Code Example
            ```csharp
            using System;

            namespace HelloWorld
            {
                class Program
                {
                    static void Main(string[] args)
                    {
                        Console.WriteLine("Hello, Toast UI Editor!");
                    }
                }
            }
            ```
            """;
    }
}
