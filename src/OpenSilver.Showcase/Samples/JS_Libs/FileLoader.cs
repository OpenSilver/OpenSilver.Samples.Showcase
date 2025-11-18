using System;
using System.Threading.Tasks;

namespace OpenSilver.Showcase;

internal static class FileLoader
{
    public static async Task<bool> TryLoadJavaScriptFile(string url)
    {
        try
        {
            await Interop.LoadJavaScriptFile(url);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    public static async Task<bool> TryLoadCssFile(string url)
    {
        try
        {
            await Interop.LoadCssFile(url);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}
