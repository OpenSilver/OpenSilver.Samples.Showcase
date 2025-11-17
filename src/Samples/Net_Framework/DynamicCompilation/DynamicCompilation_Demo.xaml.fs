namespace OpenSilver.Samples.Showcase

open System
open System.IO
open System.Linq
open System.Reflection
open System.Runtime.Loader
open System.Windows
open System.Windows.Controls
open Microsoft.CodeAnalysis
open Microsoft.CodeAnalysis.CSharp

type DynamicCompilation_Demo() as this =
    inherit DynamicCompilation_DemoXaml()

    do
        this.InitializeComponent()
        
        // Set default sample code
        this.CodeTextBox.Text <- 
            "using System;\n\n" +
            "public class Program\n" +
            "{\n" +
            "    public static string Execute()\n" +
            "    {\n" +
            "        return \"Hello from dynamically compiled C# code! \" + \n" +
            "               \"The current time is: \" + DateTime.Now.ToString();\n" +
            "    }\n" +
            "}"
    
    member private this.CompileAndExecute(code: string) : string =
        // Parse the code
        let syntaxTree = CSharpSyntaxTree.ParseText(code)

        // Get references to required assemblies
        let references = ResizeArray<MetadataReference>()
        
        // Resolve all assemblies needed for compilation (similar to XAML.io approach)
        let assembliesToLoad = DynamicCompilation_Demo.ResolveAssembliesToLoad()
        
        // Check if we can use file-based references (Simulator)
        let mutable useFileBased = false
        let testAssembly = typeof<obj>.Assembly
        let testLocation = testAssembly.Location
        
        if not (String.IsNullOrEmpty(testLocation)) && File.Exists(testLocation) then
            // File-based approach for Simulator
            for asm in assembliesToLoad do
                let loc = asm.Location
                if not (String.IsNullOrEmpty(loc)) && File.Exists(loc) then
                    references.Add(MetadataReference.CreateFromFile(loc))
                    useFileBased <- true
        
        // If file-based didn't work, use HTTP download approach (WebAssembly)
        if not useFileBased then
            let httpClient = new Net.Http.HttpClient()
            
            for asm in assembliesToLoad do
                let assemblyFileName = asm.GetName().Name + ".dll"
                
                try
                    // Download assembly from _framework folder (as done in XAML.io)
                    let assemblyBytes = httpClient.GetByteArrayAsync(sprintf "_framework/%s" assemblyFileName).Result
                    
                    // Create metadata reference from bytes
                    use memoryStream = new MemoryStream(assemblyBytes)
                    references.Add(MetadataReference.CreateFromStream(memoryStream))
                    
                    Console.WriteLine(sprintf "Loaded assembly: %s" assemblyFileName)
                with ex ->
                    Console.WriteLine(sprintf "Warning: Could not load assembly %s: %s" assemblyFileName ex.Message)
        
        if references.Count = 0 then
            raise (Exception("Unable to load any metadata references for compilation. Please check the browser console for details."))
        
        Console.WriteLine(sprintf "Total metadata references loaded: %d" references.Count)

        // Create compilation
        let compilation = 
            CSharpCompilation.Create(
                assemblyName = "DynamicAssembly",
                syntaxTrees = [| syntaxTree |],
                references = references,
                options = CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))

        // Compile to memory stream
        use ms = new MemoryStream()
        let emitResult = compilation.Emit(ms)

        if not emitResult.Success then
            // Get compilation errors
            let failures = 
                emitResult.Diagnostics
                |> Seq.filter (fun diagnostic -> diagnostic.Severity = DiagnosticSeverity.Error)
            
            let errors = 
                failures 
                |> Seq.map (fun f -> sprintf "%s: %s" f.Id (f.GetMessage()))
                |> String.concat "\n"
            
            raise (Exception(sprintf "Compilation failed:\n%s" errors))

        // Load the compiled assembly
        ms.Seek(0L, SeekOrigin.Begin) |> ignore
        let assembly = AssemblyLoadContext.Default.LoadFromStream(ms)

        // Find and execute the method
        let programType = assembly.GetType("Program")
        if isNull programType then
            raise (Exception("Could not find 'Program' class in compiled code"))

        let method = programType.GetMethod("Execute", BindingFlags.Public ||| BindingFlags.Static)
        if isNull method then
            raise (Exception("Could not find 'public static Execute()' method in Program class"))

        // Invoke the method
        let result = method.Invoke(null, null)
        
        if isNull result then "(null)" else result.ToString()

    static member private ResolveAssembliesToLoad() : ResizeArray<Assembly> =
        // Resolve assemblies recursively (similar to XAML.io approach)
        let assembliesInCurrentDomain = AppDomain.CurrentDomain.GetAssemblies()
        
        // Start with core assemblies needed for basic C# compilation
        let startingAssemblies =
            [|
                typeof<obj>.Assembly.GetName().Name           // System.Private.CoreLib
                typeof<Console>.Assembly.GetName().Name       // System.Console
                typeof<Enumerable>.Assembly.GetName().Name    // System.Linq
            |]
        
        let references = ResizeArray<Assembly>()
        let visited = System.Collections.Generic.HashSet<Assembly>()
        let queue = System.Collections.Generic.Queue<Assembly>(
            assembliesInCurrentDomain |> Array.filter (fun a -> startingAssemblies |> Array.contains (a.GetName().Name)))
        
        while queue.Count > 0 do
            let current = queue.Dequeue()
            if visited.Add(current) then
                // Skip dynamic assemblies
                if not current.IsDynamic then
                    references.Add(current)
                    
                    // Add all referenced assemblies
                    for assemblyName in current.GetReferencedAssemblies() do
                        let referencedAsm = assembliesInCurrentDomain |> Array.tryFind (fun a -> a.GetName().Name = assemblyName.Name)
                        match referencedAsm with
                        | Some asm -> queue.Enqueue(asm)
                        | None -> ()
        
        references

    member private this.OnCompileAndRunClick(sender: obj, e: RoutedEventArgs) =
        try
            let code = this.CodeTextBox.Text
            
            // Clear previous output
            this.OutputTextBlock.Text <- ""
            this.OutputTextBlock.Foreground <- 
                System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkGreen)
            
            // Compile the code
            let result = this.CompileAndExecute(code)
            
            // Display the result
            this.OutputTextBlock.Text <- sprintf "Output: %s" result
        with ex ->
            this.OutputTextBlock.Text <- sprintf "Error: %s" ex.Message
            this.OutputTextBlock.Foreground <- 
                System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red)

