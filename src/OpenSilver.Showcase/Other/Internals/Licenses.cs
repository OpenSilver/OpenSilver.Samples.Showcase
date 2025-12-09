using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OpenSilver.Showcase.Browser")]
[assembly: InternalsVisibleTo("OpenSilver.Showcase.MauiHybrid")]
[assembly: InternalsVisibleTo("Showcase")]

namespace OpenSilver.Showcase.Other.Internals;

internal static class Licenses
{
    internal const string SYNCFUSION_LICENSE = "Your Syncfusion License key";

    //to get your GeoBlazor and ArcGIS license keys, follow the steps at this address: https://docs.geoblazor.com/pages/gettingStarted
    internal const string GEOBLAZOR_LICENSE = "Your GeoBlazor License key";
    internal const string ARCGIS_API_KEY = "Your ArcGIS Api key";
}
