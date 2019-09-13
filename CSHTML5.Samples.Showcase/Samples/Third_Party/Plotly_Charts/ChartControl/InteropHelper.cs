using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHTML5.Extensions.Plotly
{
    public static class InteropHelper
    {
        [Bridge.Template("({value}.v != undefined ? {value}.v : {value})")]
        public static object Unbox(object value) //move this to a helper class "InteropHelper.Unbox(..)" with the same Namespace as plotly.
        {
            return value;
        }
    }
}
