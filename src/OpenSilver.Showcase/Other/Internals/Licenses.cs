using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OpenSilver.Showcase.Browser")]
[assembly: InternalsVisibleTo("OpenSilver.Showcase.MauiHybrid")]
[assembly: InternalsVisibleTo("Showcase")]

namespace OpenSilver.Showcase.Other.Internals;

/*
LICENSE CONFIGURATION (OPTIONAL)

This project supports loading third-party license keys from an embedded
text file instead of hard-coding them in source code.

How it works:
- If a file named "licenses.txt" exists at:
      Other/Internals/licenses.txt
  it will be embedded into the assembly at build time.
- The file is NOT committed to source control (it should be .gitignored).
- At runtime, licenses are read from the embedded resource.

How to add licenses locally:
1. Create the file:
      OpenSilver.Showcase/Other/Internals/licenses.txt

2. Add your license keys using KEY=VALUE format:

    # licenses.txt
    SYNCFUSION_LICENSE=
    GEOBLAZOR_LICENSE=Your GeoBlazor License key
    ARCGIS_API_KEY=Your ArcGIS Api key

3. Build the project.

Notes:
- Lines starting with '#' are treated as comments.
- Empty lines are ignored.
- If the file is missing, the application will still build and run,
  but licenses will be null.
- Embedded licenses are included in the built binaries; do NOT use this
  mechanism for sensitive production secrets.

*/
internal static class Licenses
{
    private static readonly Lazy<Dictionary<string, string>> _values = new(() =>
    {
        var asm = Assembly.GetExecutingAssembly();
        using var stream = asm.GetManifestResourceStream("OpenSilver.Showcase.Other.Internals.licenses.txt");
        if (stream is null)
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        using var reader = new StreamReader(stream);
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        var line = "";
        while ((line = reader.ReadLine()) != null)
        {
            line = line.Trim();
            if (line.Length == 0 || line.StartsWith("#"))
            {
                continue;
            }

            var idx = line.IndexOf('=');
            if (idx <= 0)
            {
                continue;
            }

            var key = line[..idx].Trim();
            var value = line[(idx + 1)..].Trim();
            dict[key] = value;
        }

        return dict;
    });

    private static string Get(string key)
        => _values.Value.TryGetValue(key, out var v) ? v : null;

    internal static string SYNCFUSION_LICENSE
    {
        get
        {
            return Get("SYNCFUSION_LICENSE");
        }
    }

    //to get your GeoBlazor and ArcGIS license keys, follow the steps at this address: https://docs.geoblazor.com/pages/gettingStarted
    internal const string GEOBLAZOR_LICENSE = "Your GeoBlazor License key";
    internal const string ARCGIS_API_KEY = "Your ArcGIS Api key";
}
