namespace Newtonsoft.Json

#nowarn "0044" // Suppress warning FS0044 (about deprecated JSIL.Meta.JSReplacement)

module InteropHelper =

#if !BRIDGE
    [<JSIL.Meta.JSReplacement("$value")>]
#else
    [<Bridge.Template("({value}.v != undefined ? {value}.v : {value})")>]
#endif
    let Unbox (value : obj) =
        value
