using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using OpenSilver.Blazor;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace OpenSilver.Showcase;

public partial class MudBlazor_Sample : UserControl
{
    private static Popup _mudPopoverProviderPopup;

    public MudBlazor_Sample()
    {
        // MudPopoverProvider must be rendered only once
        _mudPopoverProviderPopup ??= new Popup
        {
            Child = new RazorComponent { ComponentType = typeof(MudPopoverProvider) },
            IsOpen = true,
        };

        InitializeComponent();
        DataContext = MudBlazorThemeService.Instance;
    }
}

public class MudBlazorThemeService : INotifyPropertyChanged
{
    public static MudBlazorThemeService Instance { get; } = new();

    private bool isDarkMode;
    public bool IsDarkMode
    {
        get => isDarkMode;
        set
        {
            if (value != isDarkMode)
            {
                isDarkMode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsDarkMode)));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}

public static class Initializer
{
    public static void AddMudBlazorSamples(this IServiceCollection services)
    {
        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
        });
    }
}
