using OpenSilver.Samples.Showcase.Search;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OpenSilver.Samples.Showcase
{
    public partial class Icons : Page
    {
        public Icons()
        {
            InitializeComponent();

            // Load all emoji
            IconsTextBlock.Text = GetAllEmoji();

            // Subscribe to the CharacterClick event
            CharacterClickService.Attach(IconsTextBlock, OnCharacterClick);
        }

        private void OnCharacterClick(object sender, CharacterClickEventArgs e)
        {
            //MessageBox.Show($"You clicked: {e.Character}");

            // Build the numeric entities for the exact code-points of the grapheme.
            string xmlRefs = GetXmlNumericEntities(e.Character); // e.g. "&#x1F600;"
            string snippet = $"<TextBlock Text=\"{xmlRefs}\"/>";

            // Also show the pretty “U+…” list for reference.
            string pretty = string.Join(" ",
                            EnumerateCodePoints(e.Character)
                                .Select(cp => $"U+{cp:X}"));

            // Show the snippet
            var popup = new CodeSnippetChildWindow(snippet);
            popup.Show();
        }

        private static string GetAllEmoji()
        {
            // Blocks that actually contain pictographic emoji.
            // We intentionally leave out ranges such as 1F700-1F77F (mostly alchemical
            // symbols) because almost none of them render on Windows.
            (int Start, int End)[] emojiBlocks =
            {
                (0x1F600, 0x1F64F),  // Emoticons 😀😅🤔
                (0x2700 , 0x27BF),   // Dingbats ✂✉✔
                (0x1F300, 0x1F5FF),  // Misc Symbols & Pictographs
                (0x2600 , 0x26FF),   // Misc Symbols ☀✈☂
                (0x1F680, 0x1F6FF),  // Transport & Map 🚀✈🛳
                (0x1F900, 0x1F9FF),  // Supplemental Symbols & Pictographs 🤯🥺🦄
                //(0x1FA70, 0x1FAFF),  // Symbols & Pictographs Ext-B 🪄🫠
            };

            var sb = new StringBuilder(3500);

            foreach (var (start, end) in emojiBlocks)
            {
                for (int cp = start; cp <= end; cp++)
                {
                    // Skip surrogate-half values (D800–DFFF) — they are not valid
                    // standalone Unicode scalar values.
                    if (cp >= 0xD800 && cp <= 0xDFFF)
                        continue;

                    // ---- BMP characters (<= 0xFFFF) -----------------------------
                    if (cp <= 0xFFFF)
                    {
                        char ch = (char)cp;

                        // Filter: keep only "OtherSymbol" (that’s where pictographic
                        // symbols/emoji live); this cuts nearly all empties.
                        if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.OtherSymbol)
                            continue;

                        sb.Append(ch);
                    }
                    // ---- Supplementary Plane characters (> 0xFFFF) --------------
                    else
                    {
                        // We can’t query UnicodeCategory without Rune, so we simply
                        // add the code point; undefined scalars in these emoji
                        // blocks are extremely rare.
                        sb.Append(char.ConvertFromUtf32(cp));
                    }
                }
            }

            return sb.ToString();
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
    }
}
