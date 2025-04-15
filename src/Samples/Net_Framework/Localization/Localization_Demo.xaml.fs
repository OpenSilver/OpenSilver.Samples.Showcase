namespace OpenSilver.Samples.Showcase

open System
open System.Globalization
open System.Linq
open System.Net.Http
open System.Reflection
open System.Resources
open System.Text.Json
open System.Threading.Tasks
open System.Windows
open System.Windows.Controls
open OpenSilver
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("resources", "translation", "globalization", "internationalization", "culture", "region")>]
type Localization_Demo() as this =
    inherit Localization_DemoXaml()

    let supportedCultures =
        [| CultureInfo("es"); CultureInfo("fr"); CultureInfo("ru") |]

    do
        this.InitializeComponent()
        this.allCulturesCombo.SelectedItem <-
            this.allCulturesCombo.Items
            |> Seq.cast<obj>
            |> Seq.tryFind (fun x -> (x :?> FrameworkElement).Tag.ToString() = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
            |> Option.toObj

    member this.OnCulturesComboBoxSelectionChanged(sender: obj, e: SelectionChangedEventArgs) =
        async {
            let selectedCulture = CultureInfo((this.allCulturesCombo.SelectedItem :?> FrameworkElement).Tag.ToString())
            CultureInfo.CurrentCulture <- selectedCulture
            CultureInfo.CurrentUICulture <- selectedCulture

            //let mutable resultMessage = SampleResourceFile.GreetingMessage

            //if not Interop.IsRunningInTheSimulator && Array.contains selectedCulture supportedCultures then
            //    let! msg = Localization_Demo.GetLocalizedValueInBrowser(selectedCulture.TwoLetterISOLanguageName) |> Async.AwaitTask
            //    resultMessage <- msg

            //this.message.Text <- resultMessage
            this.dateTextBlock.Text <- DateTime.Now.ToString()
        }
        |> Async.StartImmediate

    static member GetLocalizedValueInBrowser(languageCode: string) : Task<string> =
        task {
            //let assemblyName = typeof<SampleResourceFile>.Assembly.GetName().Name
            //try
            //    let mutable resourceAssembly =
            //        AppDomain.CurrentDomain.GetAssemblies()
            //        |> Array.tryFind (fun x ->
            //            let name = x.GetName()
            //            name.Name = $"{assemblyName}.resources" && name.CultureName = languageCode)

            //    if resourceAssembly.IsNone then
            //        let baseAddress = Interop.ExecuteJavaScriptGetResult<string>("window.location.origin + window.location.pathname").TrimEnd('/')
            //        use httpClient = new HttpClient(BaseAddress = Uri($"{baseAddress}/_framework/"))

            //        let! bootJson = httpClient.GetStringAsync("blazor.boot.json")
            //        let document = JsonDocument.Parse(bootJson)

            //        let resources, satelliteResources, cultureSection = ref Unchecked.defaultof<_>, ref Unchecked.defaultof<_>, ref Unchecked.defaultof<_>
            //        if document.RootElement.TryGetProperty("resources", resources) &&
            //           resources.Value.TryGetProperty("satelliteResources", satelliteResources) &&
            //           satelliteResources.Value.TryGetProperty(languageCode, cultureSection) then

            //            for prop in cultureSection.Value.EnumerateObject() do
            //                if prop.Name.StartsWith($"{assemblyName}.resources") then
            //                    let! bytes = httpClient.GetByteArrayAsync($"{languageCode}/{prop.Name}")
            //                    resourceAssembly <- Some (Assembly.Load(bytes))
            //                    break

            //    match resourceAssembly with
            //    | Some ra ->
            //        let resourceManager = ResourceManager($"{assemblyName}.Other.Localization.{nameof SampleResourceFile}.{languageCode}", ra)
            //        return resourceManager.GetString(nameof SampleResourceFile.GreetingMessage)
            //    | None -> return null
            //with ex ->
            //    Console.WriteLine(ex.Message)
            //    return null
        }
