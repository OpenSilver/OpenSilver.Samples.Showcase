using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHTML5.Extensions.Plotly
{
    public static class InteropHelper
    {
#if !BRIDGE
        [JSIL.Meta.JSReplacement("$value")]
#else
        [Bridge.Template("({value}.v != undefined ? {value}.v : {value})")]
#endif
        public static object Unbox(object value) //move this to a helper class "InteropHelper.Unbox(..)" with the same Namespace as plotly.
        {
            return value;
        }
    }
}
