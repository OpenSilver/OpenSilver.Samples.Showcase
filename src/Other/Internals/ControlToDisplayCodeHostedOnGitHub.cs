﻿using System.Windows.Browser;
using CSHTML5.Internal;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public class ControlToDisplayCodeHostedOnGitHub : ContentControl
    {
        string _filePathOnGitHub;
        string _displayedHtmlString;

        public ControlToDisplayCodeHostedOnGitHub()
        {
            this.Loaded += OnLoaded;

            VerticalContentAlignment = VerticalAlignment.Stretch;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
        }

        public ControlToDisplayCodeHostedOnGitHub(string absolutePath)
            : this()
        {
            FilePathOnGitHub = absolutePath;
        }

        string GetHtmlString(string filePath)
        {
            var embedJs =
                INTERNAL_UriHelper.ConvertToHtml5Path("ms-appx:/Other/embed.js");
            return string.Format(
                "<script src=\"{0}?target={1}&style=github&showBorder=on&showLineNumbers=on&showCopy=on\"></script>",
                embedJs, HttpUtility.UrlEncode(filePath));
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
            this.Content = webView;
            _displayedHtmlString = htmlString;
        }

        public string FilePathOnGitHub
        {
            get
            {
                return _filePathOnGitHub;
            }
            set
            {
                _filePathOnGitHub = value;

                if (this.IsLoaded)
                {
                    string htmlString = GetHtmlString(FilePathOnGitHub);
                    if (htmlString != _displayedHtmlString)
                    {
                        DisplayHtmlString(htmlString);
                    }
                }
            }
        }

    }
}
