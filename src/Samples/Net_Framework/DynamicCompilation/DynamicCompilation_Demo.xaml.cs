using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using OpenSilver.Samples.Showcase.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Loader;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

[SearchKeywords("roslyn", "compile", "dynamic", "runtime", "csharp", "code analysis")]
public partial class DynamicCompilation_Demo : UserControl
{
    public DynamicCompilation_Demo()
    {
        InitializeComponent();
        
        // Set default sample code
        CodeTextBox.Text = @"using System;

public class Program
{
    public static string Execute()
    {
        return ""Hello from dynamically compiled C# code! "" + 
               ""The current time is: "" + DateTime.Now.ToString();
    }
}";
    }

    private void OnCompileAndRunClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string code = CodeTextBox.Text;
            
            // Clear previous output
            OutputTextBlock.Text = "";
            OutputTextBlock.Foreground = new System.Windows.Media.SolidColorBrush(
                System.Windows.Media.Colors.DarkGreen);
            
            // Compile the code
            var result = CompileAndExecute(code);
            
            // Display the result
            OutputTextBlock.Text = $"Output: {result}";
        }
        catch (Exception ex)
        {
            OutputTextBlock.Text = $"Error: {ex.Message}";
            OutputTextBlock.Foreground = new System.Windows.Media.SolidColorBrush(
                System.Windows.Media.Colors.Red);
        }
    }

    private string CompileAndExecute(string code)
    {
        // Parse the code
        var syntaxTree = CSharpSyntaxTree.ParseText(code);

        // Get references to required assemblies
        var references = new List<MetadataReference>();
        
        // Resolve all assemblies needed for compilation (similar to XAML.io approach)
        var assembliesToLoad = ResolveAssembliesToLoad();
        
        // Check if we can use file-based references (Simulator)
        bool useFileBased = false;
        var testAssembly = typeof(object).Assembly;
        var testLocation = testAssembly.Location;
        
        if (!string.IsNullOrEmpty(testLocation) && File.Exists(testLocation))
        {
            // File-based approach for Simulator
            foreach (var asm in assembliesToLoad)
            {
                var loc = asm.Location;
                if (!string.IsNullOrEmpty(loc) && File.Exists(loc))
                {
                    references.Add(MetadataReference.CreateFromFile(loc));
                    useFileBased = true;
                }
            }
        }
        
        // If file-based didn't work, use HTTP download approach (WebAssembly)
        if (!useFileBased)
        {
            var httpClient = new HttpClient();
            
            foreach (var asm in assembliesToLoad)
            {
                var assemblyFileName = asm.GetName().Name + ".dll";
                
                try
                {
                    // Download assembly from _framework folder (as done in XAML.io)
                    var assemblyBytes = httpClient.GetByteArrayAsync($"_framework/{assemblyFileName}").Result;
                    
                    // Create metadata reference from bytes
                    using var memoryStream = new MemoryStream(assemblyBytes);
                    references.Add(MetadataReference.CreateFromStream(memoryStream));
                    
                    Console.WriteLine($"Loaded assembly: {assemblyFileName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Could not load assembly {assemblyFileName}: {ex.Message}");
                }
            }
        }
        
        if (references.Count == 0)
        {
            throw new Exception("Unable to load any metadata references for compilation. Please check the browser console for details.");
        }
        
        Console.WriteLine($"Total metadata references loaded: {references.Count}");

        // Create compilation
        var compilation = CSharpCompilation.Create(
            assemblyName: "DynamicAssembly",
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        // Compile to memory stream
        using var ms = new MemoryStream();
        var emitResult = compilation.Emit(ms);

        if (!emitResult.Success)
        {
            // Get compilation errors
            var failures = emitResult.Diagnostics
                .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
            
            var errors = string.Join("\n", failures.Select(f => 
                $"{f.Id}: {f.GetMessage()}"));
            
            throw new Exception($"Compilation failed:\n{errors}");
        }

        // Load the compiled assembly
        ms.Seek(0, SeekOrigin.Begin);
        var assembly = AssemblyLoadContext.Default.LoadFromStream(ms);

        // Find and execute the method
        var type = assembly.GetType("Program");
        if (type == null)
            throw new Exception("Could not find 'Program' class in compiled code");

        var method = type.GetMethod("Execute", BindingFlags.Public | BindingFlags.Static);
        if (method == null)
            throw new Exception("Could not find 'public static Execute()' method in Program class");

        // Invoke the method
        var result = method.Invoke(null, null);
        
        return result?.ToString() ?? "(null)";
    }

    private static List<Assembly> ResolveAssembliesToLoad()
    {
        // Resolve assemblies recursively (similar to XAML.io approach)
        var assembliesInCurrentDomain = AppDomain.CurrentDomain.GetAssemblies();
        
        // Start with core assemblies needed for basic C# compilation
        var startingAssemblies = new[]
        {
            typeof(object).Assembly.GetName().Name,           // System.Private.CoreLib
            typeof(Console).Assembly.GetName().Name,          // System.Console
            typeof(Enumerable).Assembly.GetName().Name,       // System.Linq
        };
        
        var references = new List<Assembly>();
        var visited = new HashSet<Assembly>();
        var queue = new Queue<Assembly>(
            assembliesInCurrentDomain.Where(a => startingAssemblies.Contains(a.GetName().Name)));
        
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!visited.Add(current))
            {
                continue;
            }
            
            // Skip dynamic assemblies
            if (current.IsDynamic)
            {
                continue;
            }
            
            references.Add(current);
            
            // Add all referenced assemblies
            foreach (var assemblyName in current.GetReferencedAssemblies())
            {
                var referencedAsm = assembliesInCurrentDomain.FirstOrDefault(a => a.GetName().Name == assemblyName.Name);
                if (referencedAsm != null)
                {
                    queue.Enqueue(referencedAsm);
                }
            }
        }
        
        return references;
    }
}


