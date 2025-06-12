using OpenSilver;
using System;
using System.Runtime.CompilerServices;   // ConditionalWeakTable
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public delegate void CharacterClickHandler(object sender, CharacterClickEventArgs e);

    public sealed class CharacterClickEventArgs : EventArgs
    {
        public string Character { get; }
        internal CharacterClickEventArgs(string c) => Character = c;
    }

    public static class CharacterClickService
    {
        /* Stores (TextBlock → JS callback) without preventing GC */
        private static readonly ConditionalWeakTable<TextBlock, Action<string>> _map = new ConditionalWeakTable<TextBlock, Action<string>>();

        public static async void Attach(TextBlock tb, CharacterClickHandler handler)
        {
            if (tb is null || handler is null) return;

            Detach(tb);                                  // idempotent

            void Managed(string ch) => handler(tb, new CharacterClickEventArgs(ch));
            _map.Add(tb, Managed);

            if (tb.IsLoaded) await Enable(tb, Managed);
            else tb.Loaded += LoadedEnable;

            async void LoadedEnable(object s, RoutedEventArgs e)
            { tb.Loaded -= LoadedEnable; await Enable(tb, Managed); }
        }

        public static void Detach(TextBlock tb)
        {
            if (!_map.TryGetValue(tb, out var managed)) return;
            _map.Remove(tb);

            var div = Interop.GetDiv(tb);
            Interop.ExecuteJavaScriptVoid(@"
(function(host, cb){
    const box = host.firstChild;
    if(box) box.removeEventListener('click', cb);
})( $0, $1 );
", div, managed);
        }

        /* -------- internal: JS once per TextBlock ------------------- */
        private static async Task Enable(TextBlock tb, Action<string> managedCallback)
        {
            await Task.Delay(10); // workaround to the TextBlock content not being available if the attach happens before the content of the TextBlock has been loaded into the DOM.

            var div = Interop.GetDiv(tb);

            Interop.ExecuteJavaScriptVoid(@"
(function(host, cb){
    const box = host.firstChild;
    if(!box || box.__charClickEnabled) return;
    box.__charClickEnabled = true;

    const raw = box.textContent;
    const parts = (window.Intl && Intl.Segmenter)
        ? [...new Intl.Segmenter('und',{granularity:'grapheme'}).segment(raw)].map(s=>s.segment)
        : Array.from(raw);

    const frag = document.createDocumentFragment();
    for(const g of parts){
        const sp=document.createElement('span');
        sp.className='grapheme';
        sp.textContent=g;
        sp.style.display='inline-block';
        sp.style.padding='0 .05em';
        frag.appendChild(sp);
    }
    box.replaceChildren(frag);

    box.addEventListener('click', e=>{
        const sp=e.target.closest('.grapheme');
        if(sp){ cb(sp.textContent); }
    });
})( $0, $1 );
", div, managedCallback);

            tb.Unloaded += AutoDetach;
            void AutoDetach(object s, RoutedEventArgs e)
            { tb.Unloaded -= AutoDetach; Detach(tb); }
        }
    }
}
