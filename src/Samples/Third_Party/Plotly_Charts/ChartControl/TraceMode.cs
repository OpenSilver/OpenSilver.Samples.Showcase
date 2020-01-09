using System;
using System.Collections.Generic;

namespace CSHTML5.Extensions.Plotly
{
    [Flags]
    public enum TraceMode : short
    {
        None = 0,
        Lines = 1,
        Markers = 2,
        Text = 4,
    }

    internal static class TraceModeExtensionMethods
    {
        public static string ToJavaScriptString(this TraceMode traceMode)
        {
            if (traceMode == 0)
            {
                return "none";
            }
            else
            {
                List<string> result = new List<string>();
                foreach (TraceMode value in Enum.GetValues(typeof(TraceMode)))
                {
                    if ((traceMode & value) != 0)
                        result.Add(value.ToString());
                }
                return String.Join("+", result).ToLower();
            }
        }
    }
}
