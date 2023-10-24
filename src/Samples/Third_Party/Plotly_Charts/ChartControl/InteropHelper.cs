namespace OpenSilver.Extensions.Plotly
{
    public static class InteropHelper
    {
#if !BRIDGE
        [JSIL.Meta.JSReplacement("$value")]
#else
        [Bridge.Template("({value}.v != undefined ? {value}.v : {value})")]
#endif
        public static object Unbox(object value)
        {
            return value;
        }
    }
}
