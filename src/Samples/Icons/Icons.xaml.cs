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
            // Wide-platform (≤ Emoji 12.0) emoji code points — inclusive
            (int Start, int End)[] emojiBlocks =
            {
                // Keycap bases
                (0x0023, 0x0023),   // #
                (0x002A, 0x002A),   // *
                (0x0030, 0x0039),   // 0-9

                // Two specials required to complete keycap sequences
                (0x20E3, 0x20E3),   // COMBINING ENCLOSING KEYCAP
                (0xFE0F, 0xFE0F),   // VARIATION SELECTOR-16 (emoji style)

                // “Legacy” text emoji points and blocks
                (0x00A9, 0x00A9), (0x00AE, 0x00AE),
                (0x203C, 0x203C), (0x2049, 0x2049),
                (0x2122, 0x2122), (0x2139, 0x2139),
                (0x2194, 0x2199), (0x21A9, 0x21AA),
                (0x231A, 0x231B), (0x2328, 0x2328),
                (0x2388, 0x2388), (0x23CF, 0x23CF),
                (0x23E9, 0x23F3), (0x23F8, 0x23FA),
                (0x24C2, 0x24C2),
                (0x25AA, 0x25AB), (0x25B6, 0x25B6),
                (0x25C0, 0x25C0), (0x25FB, 0x25FE),
                (0x2600, 0x26FF),  // Misc Symbols
                (0x2700, 0x27BF),  // Dingbats
                (0x27C0, 0x27EF),  // extra arrows/shapes
                (0x2900, 0x297F),  // Supplemental Arrows-B
                (0x2B00, 0x2BFF),  // Misc Symbols & Arrows

                // Main multicolour emoji planes recognised by Windows 10
                (0x1F000, 0x1F02F), // Mahjong tiles
                (0x1F0A0, 0x1F0FF), // Playing cards
                (0x1F100, 0x1F1FF), // Enclosed alphanums (squared letters/digits, flags)
                (0x1F300, 0x1F5FF), // Misc Symbols & Pictographs
                (0x1F600, 0x1F64F), // Emoticons
                (0x1F680, 0x1F6FF), // Transport & Map
                (0x1F700, 0x1F77F), // Alchemical symbols (monochrome but harmless)
                (0x1F780, 0x1F7FF), // Geometric Shapes Ext. (🟥, 🟩, …)
                (0x1F800, 0x1F8FF), // Supplemental Arrows-C
                (0x1F900, 0x1F9FF), // Supplemental Symbols & Pictographs
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
