Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("toastui", "editor", "markdown", "wysiwyg", "richtext", "js", "javascript", "interop")>
    Partial Public Class ToastUiEditor_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            toastUiEditor.Content = "# Getting Started with Toast UI Editor

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
            Console.WriteLine(""Hello, Toast UI Editor!"");
        }
    }
}
```"
        End Sub
    End Class

End Namespace
