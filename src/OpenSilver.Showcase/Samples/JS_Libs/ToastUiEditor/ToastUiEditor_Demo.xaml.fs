namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("toastui", "editor", "markdown", "wysiwyg", "richtext", "js", "javascript", "interop")>]
type ToastUiEditor_Demo() as this =
    inherit ToastUiEditor_DemoXaml()

    do
        this.InitializeComponent()
        
        // Set initial content
        this.toastUiEditor.Content <- """
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
"""
