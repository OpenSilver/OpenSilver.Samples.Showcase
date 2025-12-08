Imports System.Globalization
Imports System.Net.Http
Imports System.Reflection
Imports System.Resources
Imports System.Text.Json
Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("resources", "translation", "globalization", "internationalization", "culture", "region")>
    Partial Public Class Localization_Demo
        Inherits UserControl

        Private ReadOnly _supportedCultures As CultureInfo() = {
            New CultureInfo("es"),
            New CultureInfo("fr"),
            New CultureInfo("ru")
        }

        Public Sub New()
            InitializeComponent()
            allCulturesCombo.SelectedItem = allCulturesCombo.Items.Cast(Of Object)().FirstOrDefault(Function(x) TryCast(x, FrameworkElement).Tag.ToString() = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
        End Sub

        Private Async Sub OnCulturesComboBoxSelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim selectedCulture = New CultureInfo(TryCast(allCulturesCombo.SelectedItem, FrameworkElement).Tag.ToString())

            CultureInfo.CurrentCulture = selectedCulture
            CultureInfo.CurrentUICulture = selectedCulture

            Dim resultMessage As String = SampleResourceFile.GreetingMessage

            If Not Interop.IsRunningInTheSimulator AndAlso _supportedCultures.Contains(selectedCulture) Then
                resultMessage = Await GetLocalizedValueInBrowser(selectedCulture.TwoLetterISOLanguageName)
            End If

            message.Text = resultMessage
            dateTextBlock.Text = DateTime.Now.ToString()
        End Sub

        Private Shared Async Function GetLocalizedValueInBrowser(languageCode As String) As Task(Of String)
            Dim assemblyName = GetType(SampleResourceFile).Assembly.GetName().Name

            Try
                Dim resourceAssembly As Assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(Function(x)
                                                                                                              Dim name = x.GetName()
                                                                                                              Return name.Name = $"{assemblyName}.resources" AndAlso name.CultureName = languageCode
                                                                                                          End Function)

                If resourceAssembly Is Nothing Then
                    Dim baseAddress = Interop.ExecuteJavaScriptGetResult(Of String)("window.location.origin + window.location.pathname").TrimEnd("/"c)
                    Using httpClient As New HttpClient With {.BaseAddress = New Uri($"{baseAddress}/_framework/")}

                        Dim bootJson = Await httpClient.GetStringAsync("blazor.boot.json")
                        Dim document = JsonDocument.Parse(bootJson)

                        Dim resources, satelliteResources, cultureSection As JsonElement

                        If document.RootElement.TryGetProperty("resources", resources) AndAlso
                            resources.TryGetProperty("satelliteResources", satelliteResources) AndAlso
                            satelliteResources.TryGetProperty(languageCode, cultureSection) Then

                            For Each prop In cultureSection.EnumerateObject()
                                If prop.Name.StartsWith($"{assemblyName}.resources") Then
                                    Dim bytes = Await httpClient.GetByteArrayAsync($"{languageCode}/{prop.Name}")
                                    resourceAssembly = Assembly.Load(bytes)
                                    Exit For
                                End If
                            Next
                        End If
                    End Using
                End If

                Dim resourceManager = New ResourceManager($"{assemblyName}.Other.Localization.{NameOf(SampleResourceFile)}.{languageCode}", resourceAssembly)
                Return resourceManager.GetString(NameOf(SampleResourceFile.GreetingMessage))
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try

            Return Nothing
        End Function
    End Class

End Namespace
