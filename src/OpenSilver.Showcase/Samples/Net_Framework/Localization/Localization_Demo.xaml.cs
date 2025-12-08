using OpenSilver.Showcase.Search;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Resources;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase;

[SearchKeywords("resources", "translation", "globalization", "internationalization", "culture", "region")]
public partial class Localization_Demo : UserControl
{
    private readonly CultureInfo[] _supportedCultures =
        [
            new CultureInfo("es"),
            new CultureInfo("fr"),
            new CultureInfo("ru"),
        ];

    public Localization_Demo()
    {
        InitializeComponent();

        allCulturesCombo.SelectedItem = allCulturesCombo.Items.FirstOrDefault(x => (x as FrameworkElement).Tag.ToString() == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
    }

    private async void OnCulturesComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedCulture = new CultureInfo(((FrameworkElement)allCulturesCombo.SelectedItem).Tag.ToString());

        CultureInfo.CurrentCulture = selectedCulture;
        CultureInfo.CurrentUICulture = selectedCulture;

        string resultMessage = SampleResourceFile.GreetingMessage; // in Simulator or MAUI Hybrid it works fine

        // in browser we need to load a satellite resource dll manually
        if (!Interop.IsRunningInTheSimulator && _supportedCultures.Contains(selectedCulture))
        {
            resultMessage = await GetLocalizedValueInBrowser(selectedCulture.TwoLetterISOLanguageName);
        }

        message.Text = resultMessage;
        dateTextBlock.Text = DateTime.Now.ToString();
    }

    private static async Task<string> GetLocalizedValueInBrowser(string languageCode)
    {
        var assemblyName = typeof(SampleResourceFile).Assembly.GetName().Name;

        try
        {
            Assembly resourceAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x =>
                {
                    var name = x.GetName();
                    return name.Name == $"{assemblyName}.resources" && name.CultureName == languageCode;
                });

            if (resourceAssembly == null)
            {
                var baseAddress = Interop.ExecuteJavaScriptGetResult<string>("window.location.origin + window.location.pathname").TrimEnd('/');
                using var httpClient = new HttpClient { BaseAddress = new Uri($"{baseAddress}/_framework/") };

                // find dll name with hash
                var bootJson = await httpClient.GetStringAsync("blazor.boot.json");
                if (JsonDocument.Parse(bootJson).RootElement.TryGetProperty("resources", out var resources) &&
                    resources.TryGetProperty("satelliteResources", out var satelliteResources) &&
                    satelliteResources.TryGetProperty(languageCode, out var cultureSection))
                {
                    foreach (var property in cultureSection.EnumerateObject())
                    {
                        if (property.Name.StartsWith($"{assemblyName}.resources"))
                        {
                            var bytes = await httpClient.GetByteArrayAsync($"{languageCode}/{property.Name}");
                            resourceAssembly = Assembly.Load(bytes);
                            break;
                        }
                    }
                }
            }

            var resourceManager = new ResourceManager($"{assemblyName}.Other.Localization.{nameof(SampleResourceFile)}.{languageCode}", resourceAssembly);
            return resourceManager.GetString(nameof(SampleResourceFile.GreetingMessage));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null;
    }
}
