using System;
using System.Windows.Browser;

namespace OpenSilver.Showcase;

public static class BlazorHelper
{
    private static string fullAppBaseUri;
    public static string FullAppBaseUri
    {
        get => fullAppBaseUri ??= $"{HtmlPage.Document.DocumentUri.GetLeftPart(UriPartial.Authority)}/full/#/";
        set => fullAppBaseUri = value;
    }
}
