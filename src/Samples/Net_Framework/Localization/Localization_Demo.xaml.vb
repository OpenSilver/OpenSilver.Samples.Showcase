Imports System.Globalization
Imports System.Net.Http
Imports System.Reflection
Imports System.Resources
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

            Dim currentCulture As CultureInfo = CultureInfo.CurrentUICulture
            Dim cultures As CultureInfo() = (New CultureInfo() {New CultureInfo("en-US"), currentCulture}).Concat(_supportedCultures).Concat(CultureInfo.GetCultures(CultureTypes.NeutralCultures)).Distinct().ToArray()

            allCulturesCombo.ItemsSource = cultures
            allCulturesCombo.SelectedItem = currentCulture
        End Sub

        Private Async Sub OnCulturesComboBoxSelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim selectedCulture As CultureInfo = TryCast(allCulturesCombo.SelectedItem, CultureInfo)

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
                Dim resourceAssembly = AppDomain.CurrentDomain.GetAssemblies().
                    FirstOrDefault(Function(x)
                                       Dim name = x.GetName()
                                       Return name.Name = $"{assemblyName}.resources" AndAlso name.CultureName = languageCode
                                   End Function)

                If resourceAssembly Is Nothing Then
                    Dim baseAddress = New Uri(Interop.ExecuteJavaScriptGetResult(Of String)("window.location.origin + window.location.pathname"))
                    Using httpClient As New HttpClient With {.BaseAddress = baseAddress}
                        Dim response = Await httpClient.GetAsync($"_framework/{languageCode}/{assemblyName}.resources.dll")
                        Dim bytes = Await response.Content.ReadAsByteArrayAsync()
                        resourceAssembly = Assembly.Load(bytes)
                    End Using
                End If

                Dim resourceManager = New ResourceManager($"{NameOf(SampleResourceFile)}.{languageCode}", resourceAssembly)
                Return resourceManager.GetString(NameOf(SampleResourceFile.GreetingMessage))
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try

            Return Nothing
        End Function
    End Class

End Namespace
