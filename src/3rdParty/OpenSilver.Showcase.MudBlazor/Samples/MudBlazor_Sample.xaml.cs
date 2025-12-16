using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using System.ComponentModel;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    public partial class MudBlazor_Sample : UserControl
    {
        public MudBlazor_Sample()
        {
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
}
