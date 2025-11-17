Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.CSharp
Imports System.IO
Imports System.Reflection
Imports System.Runtime.Loader
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class DynamicCompilation_Demo
        Inherits UserControl
        
        Public Sub New()
            InitializeComponent()
            
            ' Set default sample code
            CodeTextBox.Text = "using System;" & vbCrLf & vbCrLf & _
                "public class Program" & vbCrLf & _
                "{" & vbCrLf & _
                "    public static string Execute()" & vbCrLf & _
                "    {" & vbCrLf & _
                "        return ""Hello from dynamically compiled C# code! "" + " & vbCrLf & _
                "               ""The current time is: "" + DateTime.Now.ToString();" & vbCrLf & _
                "    }" & vbCrLf & _
                "}"
        End Sub

        Private Sub OnCompileAndRunClick(sender As Object, e As RoutedEventArgs)
            Try
                Dim code As String = CodeTextBox.Text
                
                ' Clear previous output
                OutputTextBlock.Text = ""
                OutputTextBlock.Foreground = New System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Colors.DarkGreen)
                
                ' Compile the code
                Dim result As String = CompileAndExecute(code)
                
                ' Display the result
                OutputTextBlock.Text = $"Output: {result}"
            Catch ex As Exception
                OutputTextBlock.Text = $"Error: {ex.Message}"
                OutputTextBlock.Foreground = New System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Colors.Red)
            End Try
        End Sub

        Private Function CompileAndExecute(code As String) As String
            ' Parse the code
            Dim syntaxTree = CSharpSyntaxTree.ParseText(code)

            ' Get references to required assemblies
            Dim references As New List(Of MetadataReference)
            
            ' Resolve all assemblies needed for compilation (similar to XAML.io approach)
            Dim assembliesToLoad = ResolveAssembliesToLoad()
            
            ' Check if we can use file-based references (Simulator)
            Dim useFileBased As Boolean = False
            Dim testAssembly = GetType(Object).Assembly
            Dim testLocation = testAssembly.Location
            
            If Not String.IsNullOrEmpty(testLocation) AndAlso File.Exists(testLocation) Then
                ' File-based approach for Simulator
                For Each asm In assembliesToLoad
                    Dim loc = asm.Location
                    If Not String.IsNullOrEmpty(loc) AndAlso File.Exists(loc) Then
                        references.Add(MetadataReference.CreateFromFile(loc))
                        useFileBased = True
                    End If
                Next
            End If
            
            ' If file-based didn't work, use HTTP download approach (WebAssembly)
            If Not useFileBased Then
                Dim httpClient As New Net.Http.HttpClient()
                
                For Each asm In assembliesToLoad
                    Dim assemblyFileName = asm.GetName().Name & ".dll"
                    
                    Try
                        ' Download assembly from _framework folder (as done in XAML.io)
                        Dim assemblyBytes = httpClient.GetByteArrayAsync($"_framework/{assemblyFileName}").Result
                        
                        ' Create metadata reference from bytes
                        Using memoryStream As New MemoryStream(assemblyBytes)
                            references.Add(MetadataReference.CreateFromStream(memoryStream))
                        End Using
                        
                        Console.WriteLine($"Loaded assembly: {assemblyFileName}")
                    Catch ex As Exception
                        Console.WriteLine($"Warning: Could not load assembly {assemblyFileName}: {ex.Message}")
                    End Try
                Next
            End If
            
            If references.Count = 0 Then
                Throw New Exception("Unable to load any metadata references for compilation. Please check the browser console for details.")
            End If
            
            Console.WriteLine($"Total metadata references loaded: {references.Count}")

            ' Create compilation
            Dim compilation = CSharpCompilation.Create(
                assemblyName:="DynamicAssembly",
                syntaxTrees:={syntaxTree},
                references:=references,
                options:=New CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))

            ' Compile to memory stream
            Using ms As New MemoryStream()
                Dim emitResult = compilation.Emit(ms)

                If Not emitResult.Success Then
                    ' Get compilation errors
                    Dim failures = emitResult.Diagnostics.
                        Where(Function(diagnostic) diagnostic.Severity = DiagnosticSeverity.Error)
                    
                    Dim errors = String.Join(vbLf, failures.Select(Function(f) $"{f.Id}: {f.GetMessage()}"))
                    
                    Throw New Exception($"Compilation failed:{vbLf}{errors}")
                End If

                ' Load the compiled assembly
                ms.Seek(0, SeekOrigin.Begin)
                Dim assembly = AssemblyLoadContext.Default.LoadFromStream(ms)

                ' Find and execute the method
                Dim type = assembly.GetType("Program")
                If type Is Nothing Then
                    Throw New Exception("Could not find 'Program' class in compiled code")
                End If

                Dim method = type.GetMethod("Execute", BindingFlags.Public Or BindingFlags.Static)
                If method Is Nothing Then
                    Throw New Exception("Could not find 'public static Execute()' method in Program class")
                End If

                ' Invoke the method
                Dim result = method.Invoke(Nothing, Nothing)
                
                Return If(result?.ToString(), "(null)")
            End Using
        End Function

        Private Shared Function ResolveAssembliesToLoad() As List(Of Assembly)
            ' Resolve assemblies recursively (similar to XAML.io approach)
            Dim assembliesInCurrentDomain = AppDomain.CurrentDomain.GetAssemblies()
            
            ' Start with core assemblies needed for basic C# compilation
            Dim startingAssemblies = {
                GetType(Object).Assembly.GetName().Name,           ' System.Private.CoreLib
                GetType(Console).Assembly.GetName().Name,          ' System.Console
                GetType(Enumerable).Assembly.GetName().Name        ' System.Linq
            }
            
            Dim references As New List(Of Assembly)
            Dim visited As New HashSet(Of Assembly)
            Dim queue As New Queue(Of Assembly)(
                assembliesInCurrentDomain.Where(Function(a) startingAssemblies.Contains(a.GetName().Name)))
            
            While queue.Count > 0
                Dim current = queue.Dequeue()
                If Not visited.Add(current) Then
                    Continue While
                End If
                
                ' Skip dynamic assemblies
                If current.IsDynamic Then
                    Continue While
                End If
                
                references.Add(current)
                
                ' Add all referenced assemblies
                For Each assemblyName In current.GetReferencedAssemblies()
                    Dim referencedAsm = assembliesInCurrentDomain.FirstOrDefault(Function(a) a.GetName().Name = assemblyName.Name)
                    If referencedAsm IsNot Nothing Then
                        queue.Enqueue(referencedAsm)
                    End If
                Next
            End While
            
            Return references
        End Function
    End Class
End Namespace

