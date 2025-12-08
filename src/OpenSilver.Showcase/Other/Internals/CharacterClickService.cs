using OpenSilver;
using System;
using System.Runtime.CompilerServices;   // ConditionalWeakTable
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Showcase
{
    public delegate void CharacterClickHandler(object sender, CharacterClickEventArgs e);

    public sealed class CharacterClickEventArgs : EventArgs
    {
        public string Character { get; }
        internal CharacterClickEventArgs(string c) => Character = c;
    }

    public static class CharacterClickService
    {
        /*  Stores (TextBlock → managed callback) without preventing GC  */
        private static readonly ConditionalWeakTable<TextBlock, Action<string>> _map =
            new ConditionalWeakTable<TextBlock, Action<string>>();

        /// <summary>
        /// Starts (or restarts) the character-click behaviour on the given TextBlock.
        /// Call this once; the service will keep itself alive across unload/load cycles.
        /// </summary>
        public static void Attach(TextBlock tb, CharacterClickHandler handler)
        {
            if (tb is null || handler is null) return;

            Detach(tb);          // idempotent – clears any earlier wiring

            void Managed(string ch) => handler(tb, new CharacterClickEventArgs(ch));
            _map.Add(tb, Managed);

            if (tb.IsLoaded)
            {
                _ = Enable(tb, Managed);
            }
            else
            {
                async void OnLoaded(object? s, RoutedEventArgs e)
                {
                    tb.Loaded -= OnLoaded;
                    await Enable(tb, Managed);
                }
                tb.Loaded += OnLoaded;
            }
        }

        /// <summary>Completely stops the behaviour and frees the managed callback.</summary>
        public static void Detach(TextBlock tb)
        {
            Disable(tb);
            _map.Remove(tb);   // allow GC of the delegate
        }

        /* ---------- internal helpers -------------------------------- */

        /// <summary>
        /// Removes the JS listener and clears the flag so that <see cref="Enable"/>
        /// can safely run again later.
        /// </summary>
        private static void Disable(TextBlock tb)
        {
            if (!_map.TryGetValue(tb, out var managed)) return;

            var div = Interop.GetDiv(tb);
            Interop.ExecuteJavaScriptVoid(@"
(function(host, cb){
    const box = host.firstChild;
    if (box) {
        box.removeEventListener('click', cb);
        box.__charClickEnabled = false;   // make it re-enableable
    }
})( $0, $1 );
", div, managed);
        }

        /// <summary>
        /// Injects (or re-injects) the JS that splits the text into graphemes and
        /// wires the click handler.  It also installs handlers so that the service
        /// survives future unload/load cycles.
        /// </summary>
        private static async Task Enable(TextBlock tb, Action<string> managedCallback)
        {
            await Task.Delay(10); // DOM-ready work-around

            var div = Interop.GetDiv(tb);
            Interop.ExecuteJavaScriptVoid(@"
(function(host, cb){
    const box = host.firstChild;
    if (!box || box.__charClickEnabled) return;   // already wired
    box.__charClickEnabled = true;

    const raw = box.textContent;
    const parts = (window.Intl && Intl.Segmenter)
        ? [...new Intl.Segmenter('und',{granularity:'grapheme'}).segment(raw)].map(s=>s.segment)
        : Array.from(raw);

    const frag = document.createDocumentFragment();
    for (const g of parts) {
        const sp = document.createElement('span');
        sp.className = 'grapheme';
        sp.textContent = g;
        sp.style.display = 'inline-block';
        sp.style.padding = '0 .05em';
        frag.appendChild(sp);
    }
    box.replaceChildren(frag);

    box.addEventListener('click', e => {
        const sp = e.target.closest('.grapheme');
        if (sp) { cb(sp.textContent); }
    });
})( $0, $1 );
", div, managedCallback);

            /*  Keep the service alive across Unloaded → Loaded cycles  */
            RoutedEventHandler? unload = null;
            unload = (s, e) =>
            {
                tb.Unloaded -= unload;
                Disable(tb);      // remove listener, clear flag

                /*  Re-enable on the very next Load  */
                async void reload(object? ss, RoutedEventArgs ee)
                {
                    tb.Loaded -= reload;
                    if (_map.TryGetValue(tb, out var cb))
                        await Enable(tb, cb);
                }
                tb.Loaded += reload;
            };
            tb.Unloaded += unload;

            tb.InvalidateMeasure();
            tb.InvalidateArrange();
        }
    }
}
