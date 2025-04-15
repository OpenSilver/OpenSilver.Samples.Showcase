using OpenSilver.Samples.Showcase.Search;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase;

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

        var currentCulture = CultureInfo.CurrentUICulture;
        CultureInfo[] cultures = [new CultureInfo("en-US"), currentCulture, .. _supportedCultures, .. CultureInfo.GetCultures(CultureTypes.NeutralCultures)];

        allCulturesCombo.ItemsSource = cultures.Distinct();
        allCulturesCombo.SelectedItem = currentCulture;
    }

    private async void OnCulturesComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedCulture = allCulturesCombo.SelectedItem as CultureInfo;

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
                var baseAddress = new Uri(Interop.ExecuteJavaScriptGetResult<string>("window.location.origin + window.location.pathname"));
                using var httpClient = new HttpClient { BaseAddress = baseAddress };
                var response = await httpClient.GetAsync($"_framework/{languageCode}/{assemblyName}.resources.dll");
                var bytes = await response.Content.ReadAsByteArrayAsync();
                resourceAssembly = Assembly.Load(bytes);
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
