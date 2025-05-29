using CSHTML5.Internal;
using OpenSilver.Themes.Modern;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public class ControlToDisplayCodeHostedOnGitHub : ContentControl
    {
        string _filePathOnGitHub;
        string _displayedHtmlString;

        public ControlToDisplayCodeHostedOnGitHub()
        {
            Loaded += OnLoaded;

            VerticalContentAlignment = VerticalAlignment.Stretch;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
        }

        public ControlToDisplayCodeHostedOnGitHub(string absolutePath) : this()
        {
            FilePathOnGitHub = absolutePath;
        }

        string GetHtmlString(string filePath)
        {
            var embedJs = INTERNAL_UriHelper.ConvertToHtml5Path("ms-appx:/Other/embed.js");
            var themeMode = Application.Current.Theme is ModernTheme theme && theme.CurrentPalette == ModernTheme.Palettes.Dark ? "dark" : "github";

            return $"<script src=\"{embedJs}?target={HttpUtility.UrlEncode(filePath)}&style={themeMode}&showBorder=on&showLineNumbers=on&showCopy=on\"></script>";
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_filePathOnGitHub))
            {
                string htmlString = GetHtmlString(_filePathOnGitHub);
                DisplayHtmlString(htmlString);
            }
        }

        void DisplayHtmlString(string htmlString)
        {
            var webView = new WebBrowser();
            webView.NavigateToString(htmlString);
            Content = webView;
            _displayedHtmlString = htmlString;
        }

        public string FilePathOnGitHub
        {
            get => _filePathOnGitHub;
            set
            {
                _filePathOnGitHub = value;

                if (IsLoaded)
                {
                    string htmlString = GetHtmlString(FilePathOnGitHub);
                    if (htmlString != _displayedHtmlString)
                    {
                        DisplayHtmlString(htmlString);
                    }
                }
            }
        }

        public void Refresh()
        {
            if (!string.IsNullOrEmpty(_filePathOnGitHub) && IsLoaded)
            {
                string htmlString = GetHtmlString(_filePathOnGitHub);
                DisplayHtmlString(htmlString);
            }
        }
    }
}
