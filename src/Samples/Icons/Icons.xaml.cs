using OpenSilver.Samples.Showcase.Search;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    public partial class Icons : Page
    {
        bool _emojiHaveBeenInitialized;
        bool _materialIconsHaveBeenInitialized;

        public Icons()
        {
            InitializeComponent();

            MaterialIconsTextBlock.Loaded += MaterialIconsTextBlock_Loaded;
            EmojiCharactersTextBlock.Loaded += EmojiCharactersTextBlock_Loaded;
        }

        private void MaterialIconsTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_materialIconsHaveBeenInitialized)
            {
                // Load all emoji
                MaterialIconsTextBlock.Text = GetAllMaterialIcons();

                // Subscribe to the CharacterClick event
                CharacterClickService.Attach(MaterialIconsTextBlock, OnCharacterClick_MaterialIcons);

                _materialIconsHaveBeenInitialized = true;
            }
        }

        private void EmojiCharactersTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_emojiHaveBeenInitialized)
            {
                // Load all emoji
                EmojiCharactersTextBlock.Text = GetAllEmoji();

                // Subscribe to the CharacterClick event
                CharacterClickService.Attach(EmojiCharactersTextBlock, OnCharacterClick_EmojiCharacters);

                _emojiHaveBeenInitialized = true;
            }
        }

        private void OnCharacterClick_MaterialIcons(object sender, CharacterClickEventArgs e)
        {
            //MessageBox.Show($"You clicked: {e.Character}");

            // Build the numeric entities for the exact code-points of the grapheme.
            string xmlRefs = GetXmlNumericEntities(e.Character);
            string snippet = $"<TextBlock Text=\"{xmlRefs}\" FontFamily=\"{{StaticResource MaterialIconsRegular_FontFamily}}\"/>";

            // Also show the pretty “U+…” list for reference.
            string pretty = string.Join(" ",
                            EnumerateCodePoints(e.Character)
                                .Select(cp => $"U+{cp:X}"));

            // Show the snippet
            var popup = new CodeSnippetChildWindow(snippet, isMaterialIcons: true);
            popup.Show();
        }

        private void OnCharacterClick_EmojiCharacters(object sender, CharacterClickEventArgs e)
        {
            //MessageBox.Show($"You clicked: {e.Character}");

            // Build the numeric entities for the exact code-points of the grapheme.
            string xmlRefs = GetXmlNumericEntities(e.Character); // e.g. "&#x1F600;"
            string snippet = $"<TextBlock Text=\"{xmlRefs}\" FontFamily=\"emoji, Segoe UI Emoji, Apple Color Emoji, Noto Color Emoji, Segoe UI Symbol, sans-serif\"/>";

            // Also show the pretty “U+…” list for reference.
            string pretty = string.Join(" ",
                            EnumerateCodePoints(e.Character)
                                .Select(cp => $"U+{cp:X}"));

            // Show the snippet
            var popup = new CodeSnippetChildWindow(snippet);
            popup.Show();
        }

        private static string CodepointsToEmoji(string codepointSequence)
        {
            var codepoints = codepointSequence.Split(' ');

            return string.Concat(codepoints.Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(cp => char.ConvertFromUtf32(Convert.ToInt32(cp.Trim(), 16))));
        }

        private static string GetAllEmoji()
        {
            var uri = new Uri(
                "/OpenSilver.Samples.Showcase;component/Samples/Icons/emoji-codes.txt",
                UriKind.Relative);

            var fileContent = RetrieveFileContent(uri);
            return string.Join("", fileContent.Split('\n').Select(CodepointsToEmoji));
        }


        /* Returns "&#x1F44D;&#x1F3FD;" for 👍🏽                                */
        private static string GetXmlNumericEntities(string grapheme)
        {
            var sb = new StringBuilder(grapheme.Length * 8); // worst case

            foreach (int cp in EnumerateCodePoints(grapheme))
                sb.Append("&#x").Append(cp.ToString("X")).Append(';');

            return sb.ToString();
        }

        /* Enumerate UTF-32 scalar values from any .NET string (Std 2.0-safe) */
        private static IEnumerable<int> EnumerateCodePoints(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                int codePoint = char.ConvertToUtf32(s, i);
                yield return codePoint;

                if (char.IsHighSurrogate(s[i]))      // don’t read the low surrogate twice
                    i++;
            }
        }

        private static string RetrieveFileContent(Uri uri)
        {
            var resourceStream = Application.GetResourceStream(uri).Result;
            using var currentReader = new StreamReader(resourceStream.Stream);

            string result = currentReader.ReadToEnd();
            return result;
        }

        private static string GetAllMaterialIcons()
        {
            // Load the code-points list embedded in the assembly
            var uri = new Uri(
                "/OpenSilver.Samples.Showcase;component/Samples/Icons/MaterialIcons-Regular.codepoints.txt",
                UriKind.Relative);

            string fileContent = RetrieveFileContent(uri);

            // Rough upper-bound capacity (one UTF-16 code unit per icon in the BMP,
            // two if it ever ventures into supplementary planes)
            var sb = new StringBuilder(fileContent.Length / 6 * 2);

            using var reader = new StringReader(fileContent);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line) || line[0] == '#')
                    continue;                       // skip blanks / comments

                int lastSpace = line.LastIndexOf(' ');
                if (lastSpace < 0 || lastSpace == line.Length - 1)
                    continue;                       // malformed – ignore

                string hex = line.Substring(lastSpace + 1).Trim();

                if (int.TryParse(hex, NumberStyles.HexNumber,
                                 CultureInfo.InvariantCulture, out int codePoint))
                {
                    sb.Append(char.ConvertFromUtf32(codePoint));
                }
            }

            return sb.ToString();
        }

    }
}
