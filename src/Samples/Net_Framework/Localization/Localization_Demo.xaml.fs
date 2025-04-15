namespace OpenSilver.Samples.Showcase

open OpenSilver;
open OpenSilver.Samples.Showcase.Search
open System
open System.Globalization
open System.Linq
open System.Net.Http
open System.Reflection
open System.Resources
open System.Threading.Tasks
open System.Windows.Controls

[<SearchKeywords("resources", "translation", "globalization", "internationalization", "culture", "region")>]
type Localization_Demo() as this =
    inherit Localization_DemoXaml()

    let supportedCultures =
        [| CultureInfo("es"); CultureInfo("fr"); CultureInfo("ru") |]

    do
        this.InitializeComponent()
        let currentCulture = CultureInfo.CurrentUICulture
        let cultures =
            [| CultureInfo("en-US"); currentCulture |]
            |> Array.append supportedCultures
            |> Array.append (CultureInfo.GetCultures(CultureTypes.NeutralCultures))
            |> Array.distinct

        this.allCulturesCombo.ItemsSource <- cultures
        this.allCulturesCombo.SelectedItem <- currentCulture

    member private this.OnCulturesComboBoxSelectionChanged(sender: obj, e: SelectionChangedEventArgs) =
        async {
            let selectedCulture = this.allCulturesCombo.SelectedItem :?> CultureInfo
            CultureInfo.CurrentCulture <- selectedCulture
            CultureInfo.CurrentUICulture <- selectedCulture

            //let mutable resultMessage = SampleResourceFile.GreetingMessage

            //if not Interop.IsRunningInTheSimulator && supportedCultures |> Array.contains selectedCulture then
            //    let! msg = Localization_Demo.GetLocalizedValueInBrowser(selectedCulture.TwoLetterISOLanguageName) |> Async.AwaitTask
            //    resultMessage <- msg

            //this.message.Text <- resultMessage
            //this.dateTextBlock.Text <- DateTime.Now.ToString()
        }
        |> Async.StartImmediate

    //static member private GetLocalizedValueInBrowser(languageCode: string) : Task<string> =
    //    task {
    //        let assemblyName = typeof<SampleResourceFile>.Assembly.GetName().Name

    //        try
    //            let resourceAssembly =
    //                AppDomain.CurrentDomain.GetAssemblies()
    //                |> Array.tryFind (fun x ->
    //                    let name = x.GetName()
    //                    name.Name = $"{assemblyName}.resources" && name.CultureName = languageCode
    //                )

    //            let! loadedAssembly =
    //                match resourceAssembly with
    //                | Some a -> Task.FromResult(a)
    //                | None ->
    //                    let baseAddress = Uri(Interop.ExecuteJavaScriptGetResult<string>("window.location.origin + window.location.pathname"))
    //                    use httpClient = new HttpClient(BaseAddress = baseAddress)
    //                    task {
    //                        let! response = httpClient.GetAsync($"_framework/{languageCode}/{assemblyName}.resources.dll")
    //                        let! bytes = response.Content.ReadAsByteArrayAsync()
    //                        return Assembly.Load(bytes)
    //                    }

    //            let resourceManager =
    //                ResourceManager($"{assemblyName}.Other.Localization.{nameof SampleResourceFile}.{languageCode}", loadedAssembly)

    //            return resourceManager.GetString(nameof SampleResourceFile.GreetingMessage)
    //        with ex ->
    //            Console.WriteLine(ex.Message)
    //            return null
    //    }
