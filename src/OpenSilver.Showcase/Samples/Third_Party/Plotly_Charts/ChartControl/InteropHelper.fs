namespace OpenSilver.Extensions.Plotly

#nowarn "0044" // Suppress warning FS0044 (about deprecated JSIL.Meta.JSReplacement)

module InteropHelper =
    let Unbox (value : obj) =
        value
